namespace ProbnyKolos2.DTOs;

public class PrescriptionInfoWithDoctor
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public String PatientLastName { get; set; }
    public String DoctorLastName { get; set; }
    
}