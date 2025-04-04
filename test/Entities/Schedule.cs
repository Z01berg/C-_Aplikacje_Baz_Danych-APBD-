﻿using System;
using System.Collections.Generic;

namespace WebApplication1.Entities;

public class Schedule
{
    public int IdSchedule { get; set; }

    public int IdDoctor { get; set; }

    public DateTime DateFrom { get; set; }

    public DateTime DateTo { get; set; }

    public virtual Doctor IdDoctorNavigation { get; set; } = null!;
}
