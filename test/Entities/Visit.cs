using System;
using System.Collections.Generic;

namespace WebApplication1.Entities;

public class Visit
{
    public int IdVisit { get; set; }

    public DateTime Date { get; set; }

    public int IdPatient { get; set; }

    public int IdDoctor { get; set; }

    public int Price { get; set; }

    public virtual Doctor IdDoctorNavigation { get; set; } = null!;

    public virtual Patient IdPatientNavigation { get; set; } = null!;
}
