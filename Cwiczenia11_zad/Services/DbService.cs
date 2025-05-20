using Cwiczenia11_zad.Data;
using Cwiczenia11_zad.DTOs;
using Cwiczenia11_zad.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Cwiczenia11_zad.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task AddPrescriptionAsync(PrescriptionDTO prescription)
    {
        // sprawdzam czy jest <= 10 leków
        if (prescription.Medicaments.Count > 10)
        {
            throw new Exception("Ponad dziesięc leków na recepcie.");
        }

        // sprawdzam poprawność dat
        if (prescription.DueDate < prescription.Date)
        {
            throw new Exception("Due date jest wcześniej niz date.");
        }
        
        // sprawdzam czy każdy lek istnieje
        foreach (var medicament in prescription.Medicaments)
        {
            var ex = await _context.Medicaments.AnyAsync(m => m.IdMedicament == medicament.IdMedicament);
            if (!ex)
            {
                throw new Exception("Nie ma danego medicament w bazie.");
            }
        }
        
        // sprawdzam czy istnieje pacjent, jeśli nie to dodaję
        var res = await _context.Patients.AnyAsync(p => p.IdPatient == prescription.Patient.IdPatient);
        if (!res)
        {
            _context.Patients.Add(new Patient()
            {
                FirstName = prescription.Patient.FirstName,
                LastName = prescription.Patient.LastName,
                Birthdate = prescription.Patient.Birthdate,
            });
            await _context.SaveChangesAsync();
        }
        
        // dodaje Prescription
        var newPrescription = new Prescription()
        {
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            IdPatient = prescription.Patient.IdPatient,
            IdDoctor = prescription.IdDoctor,
        };
        
        await _context.Prescriptions.AddAsync(newPrescription);
        await _context.SaveChangesAsync();

        // dodaje PrescriptionMedicament dla każdego Medicament i nowo dodanego Prescriotion
        foreach (var medicament in prescription.Medicaments)
        {
            _context.PrescriptionMedicaments.Add(new PrescriptionMedicament()
            {
                IdMedicament = medicament.IdMedicament,
                IdPrescription = newPrescription.IdPrescription,
                Dose = medicament.Dose,
                Details = medicament.Details
            });
        }
        
        await _context.SaveChangesAsync();
    }

    public async Task<PatientInfoDTO> GetPatientInfoAsync(int patientId)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.IdPatient == patientId);
        if (patient == null)
        {
            throw new Exception("Nie ma danego pacjent w bazie.");
        }

        var result = new PatientInfoDTO()
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthdate = patient.Birthdate,
            Prescriptions = new List<PrescriptionInfoDTO>()
        };
        
        var prescriptions = await _context.Prescriptions.Where(p => p.IdPatient == patient.IdPatient)
            .Include(p => p.Doctor).ToListAsync();
        foreach (var prescription in prescriptions)
        {
            var newPrescription = new PrescriptionInfoDTO()
            {
                IdPrescription = prescription.IdPrescription,
                Date = prescription.Date,
                DueDate = prescription.DueDate,
                Medicaments = new List<MedicamentInfoDTO>(),
                Doctor = new DoctorInfoDTO()
                {
                    IdDoctor = prescription.Doctor.IdDoctor,
                    FirstName = prescription.Doctor.FirstName,
                }
            };
            var meds = _context.PrescriptionMedicaments.Where(e => e.IdPrescription == prescription.IdPrescription)
                .Select(m => new MedicamentInfoDTO()
                {
                    IdMedicament = m.IdMedicament,
                    Name = m.Medicament.Name,
                    Dose = m.Dose,
                    Description = m.Medicament.Description,
                });
            foreach (var med in meds)
            {
                newPrescription.Medicaments.Add(med);
            }
            result.Prescriptions.Add(newPrescription);
        }
        return result;
    }
}