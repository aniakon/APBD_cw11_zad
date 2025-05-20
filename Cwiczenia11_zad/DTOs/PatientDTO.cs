namespace Cwiczenia11_zad.DTOs;

public class PatientInfoDTO
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public List<PrescriptionInfoDTO> Prescriptions { get; set; }
}

public class PrescriptionInfoDTO
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<MedicamentInfoDTO> Medicaments { get; set; }
    public DoctorInfoDTO Doctor { get; set; }
}

public class MedicamentInfoDTO
{
    public int IdMedicament { get; set; }
    public string Name { get; set; }
    public int Dose { get; set; }
    public string Description { get; set; }
}

public class DoctorInfoDTO
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
}