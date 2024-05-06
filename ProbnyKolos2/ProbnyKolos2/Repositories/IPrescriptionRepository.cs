using ProbnyKolos2.DTOs;

namespace ProbnyKolos2.Repositories;

public interface IPrescriptionRepository
{
    Task<List<PrescriptionInfoWithDoctor>> getPrescriptionDoctor(String lastName);
    Task<List<PrescriptionInfoWithDoctor>> getPrescription();
    Task<Prescription> AddPrescription(AddPrescription prescription);
}