using System;
using System.Collections.Generic;

namespace WebApplication1.Entities;

public class Patient
{
    public int IdPatient { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly Birthdate { get; set; }

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
