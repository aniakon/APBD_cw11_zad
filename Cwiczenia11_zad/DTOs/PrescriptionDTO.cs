using Cwiczenia11_zad.Models;

namespace Cwiczenia11_zad.DTOs;

public class PrescriptionDTO
{
    public PatientDTO Patient { get; set; }
    public int IdDoctor { get; set; }
    public List<MedicamentDTO> Medicaments { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}

public class PatientDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
}


public class MedicamentDTO
{
    public int IdMedicament { get; set; }
    public int Dose { get; set; }
    public string Details { get; set; }
}