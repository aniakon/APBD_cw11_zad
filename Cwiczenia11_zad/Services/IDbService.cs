using Cwiczenia11_zad.DTOs;
using Cwiczenia11_zad.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia11_zad.Services;

public interface IDbService
{
    Task AddPrescriptionAsync(PrescriptionDTO prescription);
    Task<PatientInfoDTO> GetPatientInfoAsync(int patientId);
}