using Microsoft.AspNetCore.Mvc;
using test_backend.Data.DTOs;
using test_backend.Services;

namespace test_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IEmailService _emailService;

    public AuthController(IAuthService authService, IEmailService emailService)
    {
        _authService = authService;
        _emailService = emailService;
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO dto)
    {
        try
        {
            // gera o token para reset de senha
            var user = _authService.GenerateResetToken(dto);
            var resetLink = $"https://hostname-do-frontend/reset-password?token={user.ResetToken}";

            var emailBody = $"<p>Click the link below to reset your password:</p><p><a href='{resetLink}'>Reset Password</a></p>";
            await _emailService.SendEmailAsync(user.Email, "Password Reset Request", emailBody);

            return Ok(new { message = "Email com Link para reset enviado", token = user.ResetToken }); ;
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("reset-password")]
    public IActionResult ResetPassword([FromBody] ResetPasswordDTO dto)
    {
        var success = _authService.ResetPassword(dto);
        if (!success)
        {
            return BadRequest("Token de reset invalido");
        }

        return Ok("Senha alterada com sucesso");
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = _authService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        // Pela simplicidade do teste não foi utilizado autoMapper
        var readUserDto = new ReadUserDTO
        {
            Id = user.Id,
            Email = user.Email,
        };

        return Ok(readUserDto);
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] CreateUserDTO dto)
    {
        try
        {
            var user = _authService.RegisterUser(dto);
            var readUserDto = new ReadUserDTO
            {
                Id = user.Id,
                Email = user.Email,

            };
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, readUserDto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("login")]
    public IActionResult Login([FromBody] PostUserDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = _authService.ValidateUser(dto.Email, dto.Password);
        if (user == null)
        {
            return Unauthorized("Credenciais inválidas.");
        }
        //se o usuário for válido, retorna um jwt access token
        var token = _authService.GenerateJwtToken(user);

        return Ok(new { AccessToken = token });
    }
}