using Microsoft.AspNetCore.Mvc;
using ProbnyKolos2.DTOs;
using ProbnyKolos2.Repositories;

namespace ProbnyKolos2.Controllers;


[ApiController]
[Route("api/prescriptions")]
public class PrescriptionController:ControllerBase
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public PrescriptionController(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> getPrescription(String? doctorLastName = null)
    {
        List<PrescriptionInfoWithDoctor> list = null;
        if (doctorLastName is null)
        {
         list =  await  _prescriptionRepository.getPrescription();
        }

        if (doctorLastName is not null)
        {
            list = await _prescriptionRepository.getPrescriptionDoctor(doctorLastName);
           
        }
        
        
        return Ok(list);
    }
    [HttpPut]
    public async Task<IActionResult> addNewPrescription(AddPrescription prescription)
    {
        if (prescription.Date > prescription.DueDate)
        {
            return BadRequest("DueDate have to be older than Date");
        }

        var addedPrescription = await _prescriptionRepository.AddPrescription(prescription);
        // return Created(addedPrescription);
        return Ok(addedPrescription);

    }
    
}

    