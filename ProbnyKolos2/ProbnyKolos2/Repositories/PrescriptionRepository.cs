using Microsoft.Data.SqlClient;
using ProbnyKolos2.DTOs;

namespace ProbnyKolos2.Repositories;

public class PrescriptionRepository: IPrescriptionRepository
{
    private readonly IConfiguration _configuration;

    public PrescriptionRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task< List<PrescriptionInfoWithDoctor>> getPrescriptionDoctor(String lastName )
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;

        command.CommandText = "select IdPrescription, Date, DueDate, Patient.LastName as patientLastName, Doctor.LastName as doctorLastName   from Prescription " +
                              "join Patient on Patient.IdPatient =Prescription.IdPatient " +
                              "join Doctor on Doctor.IdDoctor = Prescription.IdDoctor " +
                              " where Doctor.LastName = @lastName order by Date desc";
        command.Parameters.AddWithValue("@lastName", lastName);
        await connection.OpenAsync();
        List < PrescriptionInfoWithDoctor> listOfPrescription = null;
        var reader = await command.ExecuteReaderAsync();

        var prescriptionIdOrdinary = reader.GetOrdinal("IdPrescription");
        var dateIdOrdinary = reader.GetOrdinal("Date");
        var dueDateIdOrdinary = reader.GetOrdinal("DueDate");
        var patientLastNameOrdinary = reader.GetOrdinal("patientLastName");
        var doctorLastNameOrdinary = reader.GetOrdinal("doctorLastName");
        
        
        
        
        while (await reader.ReadAsync())
        {
            if (listOfPrescription is null)
            {
                listOfPrescription = new List<PrescriptionInfoWithDoctor>()
                {
                    new PrescriptionInfoWithDoctor()
                    {
                        IdPrescription = reader.GetInt32(prescriptionIdOrdinary),
                        Date = reader.GetDateTime(dateIdOrdinary),
                        DueDate = reader.GetDateTime(dueDateIdOrdinary),
                        PatientLastName = reader.GetString(patientLastNameOrdinary),
                        DoctorLastName = reader.GetString(doctorLastNameOrdinary)
                    }
                };
            }
            else
            {
                listOfPrescription.Add(new PrescriptionInfoWithDoctor()
                {
                    IdPrescription = reader.GetInt32(prescriptionIdOrdinary),
                    Date = reader.GetDateTime(dateIdOrdinary),
                    DueDate = reader.GetDateTime(dueDateIdOrdinary),
                    PatientLastName = reader.GetString(patientLastNameOrdinary),
                    DoctorLastName = reader.GetString(doctorLastNameOrdinary)
                });
            }
        }

        if (listOfPrescription is null) throw new Exception("List is empty");
        {
            
        }
        return listOfPrescription;
    }

    public async Task<List<PrescriptionInfoWithDoctor>> getPrescription()
    {
         using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;

        command.CommandText = "select IdPrescription, Date, DueDate, Patient.LastName as patientLastName, Doctor.LastName as doctorLastName   from Prescription " +
                              "join Patient on Patient.IdPatient =Prescription.IdPatient " +
                              "join Doctor on Doctor.IdDoctor = Prescription.IdDoctor " +
                              " order by Date desc";
        
        await connection.OpenAsync();
        List < PrescriptionInfoWithDoctor> listOfPrescription = null;
        var reader = await command.ExecuteReaderAsync();

        var prescriptionIdOrdinary = reader.GetOrdinal("IdPrescription");
        var dateIdOrdinary = reader.GetOrdinal("Date");
        var dueDateIdOrdinary = reader.GetOrdinal("DueDate");
        var patientLastNameOrdinary = reader.GetOrdinal("patientLastName");
        var doctorLastNameOrdinary = reader.GetOrdinal("doctorLastName");
        
        
        
        
        while (await reader.ReadAsync())
        {
            if (listOfPrescription is null)
            {
                listOfPrescription = new List<PrescriptionInfoWithDoctor>()
                {
                    new PrescriptionInfoWithDoctor()
                    {
                        IdPrescription = reader.GetInt32(prescriptionIdOrdinary),
                        Date = reader.GetDateTime(dateIdOrdinary),
                        DueDate = reader.GetDateTime(dueDateIdOrdinary),
                        PatientLastName = reader.GetString(patientLastNameOrdinary),
                        DoctorLastName = reader.GetString(doctorLastNameOrdinary)
                    }
                };
            }
            else
            {
                listOfPrescription.Add(new PrescriptionInfoWithDoctor()
                {
                    IdPrescription = reader.GetInt32(prescriptionIdOrdinary),
                    Date = reader.GetDateTime(dateIdOrdinary),
                    DueDate = reader.GetDateTime(dueDateIdOrdinary),
                    PatientLastName = reader.GetString(patientLastNameOrdinary),
                    DoctorLastName = reader.GetString(doctorLastNameOrdinary)
                });
            }
        }

        if (listOfPrescription is null) throw new Exception("List is empty");
        {
            
        }
        return listOfPrescription;
    }

    public async Task<Prescription> AddPrescription(AddPrescription prescription)
    {
       await using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
      await  using SqlCommand command = new SqlCommand();
        command.Connection = connection;

        command.CommandText = "insert into Prescription values(@Date, @DueDate, @IdPatient, @IdDoctor);Select @@IDENTITY as ID";
        command.Parameters.AddWithValue("@Date", prescription.Date);
        command.Parameters.AddWithValue("@DueDate", prescription.DueDate);
        command.Parameters.AddWithValue("@IdPatient", prescription.IdPatient);
        command.Parameters.AddWithValue("@IdDoctor", prescription.IdDoctor);
        await connection.OpenAsync();
        
        var reader = Convert.ToInt32( await command.ExecuteScalarAsync());

        Prescription prescriptionAdded = new Prescription()
        {
            IdPrescription = reader,
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            IdPatient = prescription.IdPatient,
            IdDoctor = prescription.IdDoctor
        };

        return prescriptionAdded;
    }
}