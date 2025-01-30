using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskBasket.Data;
using TaskBasket.Models;
using TaskBasket.Services;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly TaskContext _context;
    private readonly JwtService _jwtService;

    public AuthController(TaskContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] LoginDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
            return BadRequest("Username already exists.");

        var user = new User
        {
            Username = dto.Username,
            Email = $"{dto.Username}@example.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("Registration successful.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == dto.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized("Invalid username or password.");

        var token = _jwtService.GenerateToken(user.Id, user.Username);

        return Ok(new { Token = token });
    }
}
