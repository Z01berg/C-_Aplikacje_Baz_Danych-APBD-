using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public WarehouseController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<IActionResult> AddProductToWarehouse([FromBody] WarehouseRequest request)
    {
        if (request == null || request.Amount <= 0)
        {
            return BadRequest("Invalid request data.");
        }

        try
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    // Sprawdzanie, czy produkt istnieje
                    var productQuery = "SELECT COUNT(*) FROM Product WHERE IdProduct = @IdProduct";
                    using (var command = new SqlCommand(productQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@IdProduct", request.IdProduct);
                        var productExists = (int)await command.ExecuteScalarAsync() > 0;
                        if (!productExists)
                        {
                            return NotFound("Product not found.");
                        }
                    }

                    // Sprawdzanie, czy magazyn istnieje
                    var warehouseQuery = "SELECT COUNT(*) FROM Warehouse WHERE IdWarehouse = @IdWarehouse";
                    using (var command = new SqlCommand(warehouseQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@IdWarehouse", request.IdWarehouse);
                        var warehouseExists = (int)await command.ExecuteScalarAsync() > 0;
                        if (!warehouseExists)
                        {
                            return NotFound("Warehouse not found.");
                        }
                    }

                    // Sprawdzanie, czy istnieje zamówienie do zrealizowania
                    int idOrder;
                    decimal price;
                    var orderQuery = @"SELECT TOP 1 o.IdOrder, p.Price 
                                           FROM [Order] o 
                                           JOIN Product p ON o.IdProduct = p.IdProduct 
                                           LEFT JOIN Product_Warehouse pw ON o.IdOrder = pw.IdOrder 
                                           WHERE o.IdProduct = @IdProduct 
                                           AND o.Amount = @Amount 
                                           AND pw.IdProductWarehouse IS NULL 
                                           AND o.CreatedAt < @CreatedAt";
                    using (var command = new SqlCommand(orderQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@IdProduct", request.IdProduct);
                        command.Parameters.AddWithValue("@Amount", request.Amount);
                        command.Parameters.AddWithValue("@CreatedAt", request.CreatedAt);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (!await reader.ReadAsync())
                            {
                                return NotFound("No matching order found.");
                            }

                            idOrder = reader.GetInt32(0);
                            price = reader.GetDecimal(1);
                        }
                    }

                    // Aktualizacja kolumny FulfilledAt w tabeli Order
                    var updateOrderQuery = "UPDATE [Order] SET FulfilledAt = @CreatedAt WHERE IdOrder = @IdOrder";
                    using (var command = new SqlCommand(updateOrderQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                        command.Parameters.AddWithValue("@IdOrder", idOrder);
                        await command.ExecuteNonQueryAsync();
                    }

                    // Wstawianie rekordu do Product_Warehouse
                    var insertQuery =
                        @"INSERT INTO Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) 
                                            VALUES (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt); 
                                            SELECT SCOPE_IDENTITY();";
                    using (var command = new SqlCommand(insertQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@IdWarehouse", request.IdWarehouse);
                        command.Parameters.AddWithValue("@IdProduct", request.IdProduct);
                        command.Parameters.AddWithValue("@IdOrder", idOrder);
                        command.Parameters.AddWithValue("@Amount", request.Amount);
                        command.Parameters.AddWithValue("@Price", price * request.Amount);
                        command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                        var newId = (decimal)await command.ExecuteScalarAsync();
                        transaction.Commit();
                        return Ok(new { IdProductWarehouse = newId });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("stored-procedure")]
    public async Task<IActionResult> AddProductToWarehouseUsingSP([FromBody] WarehouseRequest request)
    {
        if (request == null || request.Amount <= 0)
        {
            return BadRequest("Invalid request data.");
        }

        try
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("AddProductToWarehouse", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdProduct", request.IdProduct);
                    command.Parameters.AddWithValue("@IdWarehouse", request.IdWarehouse);
                    command.Parameters.AddWithValue("@Amount", request.Amount);
                    command.Parameters.AddWithValue("@CreatedAt", request.CreatedAt);

                    try
                    {
                        var newId = (decimal)await command.ExecuteScalarAsync();
                        return Ok(new { IdProductWarehouse = newId });
                    }
                    catch (SqlException ex)
                    {
                        return StatusCode(500, $"Stored Procedure error: {ex.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}