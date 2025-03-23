using System;
using System.Collections.Generic;

namespace WebApplication1.Entities;

public class Doctor
{
    public int IdDoctor { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Specialization { get; set; }

    public int PriceForVisit { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
