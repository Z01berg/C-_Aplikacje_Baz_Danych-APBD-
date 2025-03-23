using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly MyDbContext _context;

        public PatientController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPatient(int id)
        {
            var patient = await _context.Patients
                .Include(p => p.Visits)
                .ThenInclude(v => v.IdDoctorNavigation)
                .FirstOrDefaultAsync(p => p.IdPatient == id);

            if (patient == null)
            {
                return NotFound();
            }

            var totalAmountMoneySpent = patient.Visits.Sum(v => v.Price);
            var numberOfVisits = patient.Visits.Count();

            var result = new
            {
                patient.FirstName,
                patient.LastName,
                patient.Birthdate,
                TotalAmountMoneySpent = $"{totalAmountMoneySpent} zł",
                NumberOfVisits = numberOfVisits,
                Visits = patient.Visits.Select(v => new
                {
                    v.IdVisit,
                    Doctor = $"{v.IdDoctorNavigation.FirstName} {v.IdDoctorNavigation.LastName}",
                    Date = v.Date.ToString("yyyy-MM-dd HH:mm"),
                    Price = $"{v.Price} zł"
                }).ToList()
            };

            return Ok(result);
        }

        [HttpPost("add-visit")]
        public async Task<ActionResult> AddVisit([FromBody] VisitRequest visitRequest)
        {
            if (!await _context.Patients.AnyAsync(p => p.IdPatient == visitRequest.IdPatient))
            {
                return BadRequest("Patient does not exist.");
            }

            if (!await _context.Doctors.AnyAsync(d => d.IdDoctor == visitRequest.IdDoctor))
            {
                return BadRequest("Doctor does not exist.");
            }

            var existingVisit = await _context.Visits
                .Where(v => v.IdPatient == visitRequest.IdPatient && v.Date > DateTime.Now)
                .FirstOrDefaultAsync();

            if (existingVisit != null)
            {
                return BadRequest("Patient already has a scheduled visit.");
            }

            var schedule = await _context.Schedules
                .Where(s => s.IdDoctor == visitRequest.IdDoctor && s.DateFrom <= visitRequest.Date && s.DateTo >= visitRequest.Date)
                .FirstOrDefaultAsync();

            if (schedule == null)
            {
                return BadRequest("Doctor is not available at the selected time.");
            }

            var doctor = await _context.Doctors.FindAsync(visitRequest.IdDoctor);
            var newVisit = new Visit
            {
                IdPatient = visitRequest.IdPatient,
                IdDoctor = visitRequest.IdDoctor,
                Date = visitRequest.Date,
                Price = doctor.PriceForVisit
            };

            _context.Visits.Add(newVisit);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPatient), new { id = newVisit.IdVisit }, newVisit);
        }
    }

    public class VisitRequest
    {
        public int IdPatient { get; set; }
        public int IdDoctor { get; set; }
        public DateTime Date { get; set; }
    }
}
