using CURE.Application.DTOs.Nurses;
using CURE.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CURE.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NursesController : ControllerBase
{
    private readonly INurseService _nurseService;

    public NursesController(
        INurseService nurseService)
    {
        _nurseService = nurseService;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        return Ok(
            await _nurseService.GetAllAsync(pageNumber, pageSize));
    }

    [Authorize(Roles = "Admin, Nurse")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        return Ok(await _nurseService.GetByIdAsync(id));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateNurseDto dto)
    {
        var id = await _nurseService.CreateAsync(dto);

        return Ok(id);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, UpdateNurseDto dto)
    {
        await _nurseService.UpdateAsync(id, dto);

        return NoContent();
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _nurseService.DeleteAsync(id);

        return NoContent();
    }
}