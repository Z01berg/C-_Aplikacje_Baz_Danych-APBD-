using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using WebApplication1.Entities;
public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=sql7.freesqldatabase.com;database=sql7713329;uid=sql7713329;pwd=71tFe2myZS", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.5.62-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.IdDoctor).HasName("PRIMARY");

            entity.ToTable("Doctor");

            entity.Property(e => e.IdDoctor)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PriceForVisit).HasColumnType("int(11)");
            entity.Property(e => e.Specialization).HasMaxLength(100);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.IdPatient).HasName("PRIMARY");

            entity.ToTable("Patient");

            entity.Property(e => e.IdPatient)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.IdSchedule).HasName("PRIMARY");

            entity.ToTable("Schedule");

            entity.HasIndex(e => e.IdDoctor, "Schedule_Doctor");

            entity.Property(e => e.IdSchedule)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.IdDoctor).HasColumnType("int(11)");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Schedule_Doctor");
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.HasKey(e => e.IdVisit).HasName("PRIMARY");

            entity.ToTable("Visit");

            entity.HasIndex(e => e.IdDoctor, "Table_6_Doctor");

            entity.HasIndex(e => e.IdPatient, "Table_6_Patient");

            entity.Property(e => e.IdVisit)
                .ValueGeneratedNever()
                .HasColumnType("int(11)");
            entity.Property(e => e.IdDoctor).HasColumnType("int(11)");
            entity.Property(e => e.IdPatient).HasColumnType("int(11)");
            entity.Property(e => e.Price).HasColumnType("int(11)");

            entity.HasOne(d => d.IdDoctorNavigation).WithMany(p => p.Visits)
                .HasForeignKey(d => d.IdDoctor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_6_Doctor");

            entity.HasOne(d => d.IdPatientNavigation).WithMany(p => p.Visits)
                .HasForeignKey(d => d.IdPatient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_6_Patient");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
