using Cwiczenia11_zad.Models;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenia11_zad.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }


    protected DatabaseContext() { }
    public DatabaseContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>()
        {
            new Doctor() { IdDoctor = 1, FirstName = "Doc1", LastName = "Doe", Email = "jd@gmail.com" },
            new Doctor() { IdDoctor = 2, FirstName = "Doc2", LastName = "Doe", Email = "jd2@gmail.com" },
        });

        modelBuilder.Entity<Patient>().HasData(new List<Patient>()
        {
            new Patient()
                { IdPatient = 1, FirstName = "Pac1", LastName = "Poe", Birthdate = DateTime.Parse("2002-02-02"), },
            new Patient()
                { IdPatient = 2, FirstName = "Pac2", LastName = "Poe", Birthdate = DateTime.Parse("2003-02-02") }
        });

        modelBuilder.Entity<Prescription>().HasData(new List<Prescription>()
        {
            new Prescription()
            {
                IdPrescription = 1, Date = DateTime.Parse("2025-05-05"), DueDate = DateTime.Parse("2026-05-05"),
                IdPatient = 1, IdDoctor = 2
            },
            new Prescription()
            {
                IdPrescription = 2, Date = DateTime.Parse("2025-05-05"), DueDate = DateTime.Parse("2025-05-04"),
                IdPatient = 1, IdDoctor = 1
            }
        });

        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>()
        {
            new Medicament() { IdMedicament = 1, Name = "Med1", Description = "For head", Type = "Pill" },
            new Medicament() { IdMedicament = 2, Name = "Med2", Description = "For stomach", Type = "Pill" },
        });

        modelBuilder.Entity<PrescriptionMedicament>().HasData(new List<PrescriptionMedicament>()
        {
            new PrescriptionMedicament()
            {
                IdMedicament = 1, IdPrescription = 1, Dose = 3, Details = "det1",
            },
            new PrescriptionMedicament()
            {
                IdMedicament = 2, IdPrescription = 1, Dose = 2, Details = "det2",
            },
            new PrescriptionMedicament()
            {
                IdMedicament = 2, IdPrescription = 2, Dose = 1, Details = "det3",
            }
        });
    }
}