using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AnimalsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<IActionResult> GetAnimals([FromQuery] string orderBy = "name")
    {
        var validColumns = new List<string> { "name", "description", "category", "area" };
        if (!validColumns.Contains(orderBy.ToLower()))
        {
            return BadRequest("Invalid orderBy parameter.");
        }

        var query = $"SELECT * FROM Animal ORDER BY {orderBy}";
        var animals = new List<Animal>();

        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(query, connection))
            {
                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    animals.Add(new Animal
                    {
                        IdAnimal = (int)reader["IdAnimal"],
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Category = reader["Category"].ToString(),
                        Area = reader["Area"].ToString()
                    });
                }
            }
        }

        return Ok(animals);
    }

    [HttpPost]
    public async Task<IActionResult> AddAnimal([FromBody] Animal newAnimal)
    {
        if (newAnimal == null)
        {
            return BadRequest("Invalid animal data.");
        }

        var query =
            "INSERT INTO Animal (Name, Description, Category, Area) VALUES (@Name, @Description, @Category, @Area)";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", newAnimal.Name);
                command.Parameters.AddWithValue("@Description", (object)newAnimal.Description ?? DBNull.Value);
                command.Parameters.AddWithValue("@Category", newAnimal.Category);
                command.Parameters.AddWithValue("@Area", newAnimal.Area);

                await command.ExecuteNonQueryAsync();
            }
        }

        return CreatedAtAction(nameof(GetAnimals), new { id = newAnimal.IdAnimal }, newAnimal);
    }

    [HttpPut("{idAnimal}")]
    public async Task<IActionResult> UpdateAnimal(int idAnimal, [FromBody] Animal updatedAnimal)
    {
        if (updatedAnimal == null || updatedAnimal.IdAnimal != idAnimal)
        {
            return BadRequest("Invalid animal data.");
        }

        var query =
            "UPDATE Animal SET Name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE IdAnimal = @IdAnimal";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", updatedAnimal.Name);
                command.Parameters.AddWithValue("@Description", (object)updatedAnimal.Description ?? DBNull.Value);
                command.Parameters.AddWithValue("@Category", updatedAnimal.Category);
                command.Parameters.AddWithValue("@Area", updatedAnimal.Area);
                command.Parameters.AddWithValue("@IdAnimal", updatedAnimal.IdAnimal);

                var rowsAffected = await command.ExecuteNonQueryAsync();
                if (rowsAffected == 0)
                {
                    return NotFound();
                }
            }
        }

        return NoContent();
    }

    [HttpDelete("{idAnimal}")]
    public async Task<IActionResult> DeleteAnimal(int idAnimal)
    {
        var query = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";

        using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IdAnimal", idAnimal);

                var rowsAffected = await command.ExecuteNonQueryAsync();
                if (rowsAffected == 0)
                {
                    return NotFound();
                }
            }
        }

        return NoContent();
    }
}