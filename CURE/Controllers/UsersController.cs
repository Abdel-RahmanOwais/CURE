using CURE.Application.DTOs.Users;
using CURE.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CURE.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController
    (
        IUserService userService
    )
    {
        _userService = userService;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult>
    GetAll(int pageNumber = 1, int pageSize = 10)
    {
        var users = await _userService.GetAllAsync(pageNumber, pageSize);

        return Ok(users);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult>
    GetById(long id)
    {
        var user = await _userService.GetByIdAsync(id);

        return Ok(user);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult>
    Create(CreateUserDto dto)
    {
        var id = await _userService.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById),
            new
            {
                id
            },
                id
            );
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult>
    Update
    (
        long id,
        UpdateUserDto dto
    )
    {
        await _userService.UpdateAsync(id, dto);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult>
    Delete(long id)
    {
        await _userService.SoftDelete(id);

        return NoContent();
    }
}