using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PrescriptionsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionRequest request)
    {
        if (request.Medicaments.Count > 10)
            return BadRequest("Prescription can include a maximum of 10 medicaments.");

        if (request.DueDate < request.Date)
            return BadRequest("Due date must be greater than or equal to the date.");

        var patient = await _context.Patients.FindAsync(request.Patient.IdPatient);
        if (patient == null)
        {
            patient = new Patient
            {
                IdPatient = request.Patient.IdPatient,
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                BirthDate = request.Patient.BirthDate
            };
            _context.Patients.Add(patient);
        }

        var doctor = await _context.Doctors.FindAsync(request.DoctorId);
        if (doctor == null)
            return BadRequest("Doctor not found.");

        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            Patient = patient,
            Doctor = doctor
        };

        foreach (var medicament in request.Medicaments)
        {
            var med = await _context.Medicaments.FindAsync(medicament.IdMedicament);
            if (med == null)
                return BadRequest($"Medicament with ID {medicament.IdMedicament} not found.");

            prescription.PrescriptionMedicaments.Add(new PrescriptionMedicament
            {
                Medicament = med,
                Dose = medicament.Dose,
                Details = medicament.Details
            });
        }

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return Ok(prescription);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientDetails(int id)
    {
        var patient = await _context.Patients
            .Include(p => p.Prescriptions)
                .ThenInclude(p => p.PrescriptionMedicaments)
                    .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
                .ThenInclude(p => p.Doctor)
            .FirstOrDefaultAsync(p => p.IdPatient == id);

        if (patient == null)
            return NotFound();

        var response = new PatientResponse
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            BirthDate = patient.BirthDate,
            Prescriptions = patient.Prescriptions.Select(p => new PrescriptionResponse
            {
                IdPrescription = p.IdPrescription,
                Date = p.Date,
                DueDate = p.DueDate,
                Doctor = new DoctorResponse
                {
                    IdDoctor = p.Doctor.IdDoctor,
                    FirstName = p.Doctor.FirstName,
                    LastName = p.Doctor.LastName
                },
                Medicaments = p.PrescriptionMedicaments.Select(pm => new MedicamentResponse
                {
                    IdMedicament = pm.Medicament.IdMedicament,
                    Name = pm.Medicament.Name,
                    Description = pm.Medicament.Description,
                    Dose = pm.Dose,
                    Details = pm.Details
                }).ToList()
            }).OrderBy(p => p.DueDate).ToList()
        };

        return Ok(response);
    }
}

// Dodatkowe modele do odpowiedzi
public class PrescriptionRequest
{
    public PatientRequest Patient { get; set; }
    public List<MedicamentRequest> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int DoctorId { get; set; }
}

public class PatientRequest
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
}

public class MedicamentRequest
{
    public int IdMedicament { get; set; }
    public int? Dose { get; set; }
    public string Details { get; set; }
}

public class PatientResponse
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public List<PrescriptionResponse> Prescriptions { get; set; }
}

public class PrescriptionResponse
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public DoctorResponse Doctor { get; set; }
    public List<MedicamentResponse> Medicaments { get; set; }
}

public class DoctorResponse
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class MedicamentResponse
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Dose { get; set; }
    public string Details { get; set; }
}