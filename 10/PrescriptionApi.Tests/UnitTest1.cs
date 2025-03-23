
/*
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class PrescriptionsControllerTests
{
    [Fact]
    public async Task AddPrescription_ReturnsBadRequest_WhenMedicamentNotFound()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using (var context = new ApplicationDbContext(options))
        {
            var controller = new PrescriptionsController(context);

            var request = new PrescriptionRequest
            {
                Patient = new PatientRequest { IdPatient = 1, FirstName = "John", LastName = "Doe", BirthDate = DateTime.Now },
                Medicaments = new List<MedicamentRequest>
                {
                    new MedicamentRequest { IdMedicament = 999, Dose = 10, Details = "Take daily" } // Non-existent Medicament ID
                },
                Date = DateTime.Now,
                DueDate = DateTime.Now.AddDays(10),
                DoctorId = 1
            };

            var result = await controller.AddPrescription(request);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }

}
*/