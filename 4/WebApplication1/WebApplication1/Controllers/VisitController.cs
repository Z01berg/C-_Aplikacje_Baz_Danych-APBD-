using Microsoft.AspNetCore.Mvc;

namespace WebApplication1;

[Route("api/Visits")]
[ApiController]
public class VisitController : ControllerBase
{
    private static readonly List<Visit> _visits = new()
    {
        new Visit {Id = 1, Animal = AnimalController.GetAnimalById(1), DateVisit =  new DateTime(2008, 3, 9, 16, 5, 7, 123), Description = "Famnily is united", Price = 23}
    };

    [HttpGet("animal/{visit:int}")]
    public IActionResult GetVisitsForAnimal(int animalId)
    {
        var visitsForAnimal = _visits.Where(v => v.Animal.Id == animalId).ToList();
        return Ok(visitsForAnimal);
    }
        
        
    [HttpPost]
    public IActionResult CreateVisit(Visit visit)
    {
        var animal = AnimalController.GetAnimalP(visit.Animal.Id);
        if (animal == null)
        {
            return NotFound($"Animal with id {visit.Animal.Id} was not found");
        }
        
        _visits.Add(visit);
        return StatusCode(StatusCodes.Status201Created);
    }
}