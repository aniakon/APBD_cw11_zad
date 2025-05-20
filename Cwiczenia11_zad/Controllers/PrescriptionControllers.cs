using Cwiczenia11_zad.DTOs;
using Cwiczenia11_zad.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia11_zad.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionController : ControllerBase
{
    private readonly IDbService _dbService;

    public PrescriptionController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddPrescription(PrescriptionDTO prescription)
    {
        try
        {
            await _dbService.AddPrescriptionAsync(prescription);
            return Created("", null);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("patient/{idPatient}")]
    public async Task<IActionResult> GetPatientInfo(int idPatient)
    {
        try
        {
            var pat = await _dbService.GetPatientInfoAsync(idPatient);
            return Ok(pat);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}