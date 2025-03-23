using System.Drawing;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1;

[Route("api/Animals")]
[ApiController]
public class AnimalController : ControllerBase
{
    private static readonly List<Animal> _animals = new()
    {
        new Animal { Id = 1, Name = "Alfa", Category = CategoryE.Wolf, ColorWool = "Gray", Mass = 45.0 },
        new Animal { Id = 2, Name = "Dexter", Category = CategoryE.Dog, ColorWool = "White", Mass = 30.0 },
        new Animal { Id = 3, Name = "Lorelei", Category = CategoryE.Fox, ColorWool = "Chocolate", Mass = 25.0 },
        new Animal { Id = 4, Name = "Bob", Category = CategoryE.Cat, ColorWool = "DimGray", Mass = 25.0 },
        new Animal { Id = 5, Name = "Kiri", Category = CategoryE.Bird, ColorWool = "BlueViolet", Mass = 2.0 },
    };

    [HttpGet]
    public IActionResult GetAnimals()
    {
        return Ok(_animals);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetAnimals(int id)
    {
        var animal = _animals.FirstOrDefault(a => a.Id == id);
        if (animal == null)
        {
            return NotFound($"Animal with id {id} was not found");
        }

        return Ok(animal);
    }

    [HttpPost]
    public IActionResult CreateAnimal(Animal animal)
    {
        _animals.Add(animal);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAnimal(int id, Animal animal)
    {
        var animalToEdit = _animals.FirstOrDefault(a => a.Id == id);

        if (animalToEdit == null)
        {
            return NotFound($"Animal with id {id} was not found");
        }

        _animals.Remove(animalToEdit);
        _animals.Add(animal);
        
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal(int id, Animal animal)
    {
        var animalToDelete = _animals.FirstOrDefault(a => a.Id == id);

        if (animalToDelete == null)
        {
            return NotFound($"Animal with id {id} was not found");
        }

        _animals.Remove(animalToDelete);
        
        return NoContent();
    }
    
    // For VisitController
    public static Animal GetAnimalById(int id)
    {
        return _animals.FirstOrDefault(a => a.Id == id);
    }

    public static Animal GetAnimalP(int id)
    {
        return _animals.FirstOrDefault(a => a.Id == id);
    }

}