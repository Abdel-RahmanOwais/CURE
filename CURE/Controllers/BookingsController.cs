using CURE.Application.DTOs.Booking;
using CURE.Application.Interfaces.Security;
using CURE.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CURE.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;
    private readonly ICurrentUserService _currentUserService;

    public BookingsController(IBookingService bookingService, ICurrentUserService currentUserService)
    {
        _bookingService = bookingService;
        _currentUserService = currentUserService;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        var result = await _bookingService.GetAllAsync(pageNumber, pageSize);

        return Ok(result);
    }

    [Authorize(Roles = "Admin, Nurse, Patient")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var result = await _bookingService.GetByIdAsync(id);

        return Ok(result);
    }

    [Authorize(Roles = "Admin,Nurse, Patient")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateBookingDto dto)
    {
        var id = await _bookingService.CreateAsync(dto);

        return Ok(id);
    }

    [Authorize(Roles = "Admin, Patient")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(long id, UpdateBookingDto dto)
    {
        await _bookingService.UpdateAsync(id, dto);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _bookingService.DeleteAsync(id);

        return NoContent();
    }

    [Authorize(Roles = "Admin, Nurse")]
    [HttpPut("{id}/status")]
    public async Task<IActionResult> ChangeStatus(long id, ChangeBookingStatusDto dto)
    {
        await _bookingService.ChangeStatusAsync(id, dto.StatusId, _currentUserService.UserId);

        return NoContent();
    }

    [Authorize(Roles = "Admin, Nurse")]
    [HttpGet("{id}/history")]
    public async Task<IActionResult> GetHistory(long id)
    {
        var result = await _bookingService.GetHistoryAsync(id);

        return Ok(result);
    }
}