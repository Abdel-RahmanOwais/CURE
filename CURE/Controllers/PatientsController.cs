using CURE.Application.DTOs.Patients;
using CURE.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [Authorize(Roles = "Admin, Nurse")]
    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        return Ok(await _patientService.GetAllAsync(pageNumber, pageSize));
    }

    [Authorize(Roles = "Admin, Nurse, Patient")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        return Ok(await _patientService.GetByIdAsync(id));
    }

    [Authorize(Roles = "Admin, Nurse")]
    [HttpPost]
    public async Task<IActionResult> Create(CreatePatientDto dto)
    {
        var id = await _patientService.CreateAsync(dto);

        return Ok(id);
    }

    [Authorize(Roles = "Admin, Nurse")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, UpdatePatientDto dto)
    {
        await _patientService.UpdateAsync(id, dto);

        return NoContent();
    }

    [Authorize(Roles = "Admin, Nurse")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _patientService.DeleteAsync(id);

        return NoContent();
    }
}