using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using test_backend.Data;
using test_backend.Data.DTOs;
using test_backend.Models;

namespace test_backend.Services;
public class AuthService : IAuthService
{
    private readonly TestBackendContext _context;
    private readonly string? _key;


    public AuthService(TestBackendContext context, IConfiguration config)
    {
        _context = context;
        _key = config.GetValue<string>("Jwt:Key");
    }


    public ReadUserDTO? GetUserById(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return null;
        }
        return new ReadUserDTO
        {
            Id = user.Id,
            Email = user.Email,
        };
    }
    public User RegisterUser(CreateUserDTO dto)
    {
        if (_context.Users.Any(u => u.Email == dto.Email))
        {
            throw new ArgumentException("E-mail já registrado.");
        }

        var user = new User
        {
            Email = dto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        return user;
    }

    public User? ValidateUser(string email, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return null;
        }

        return user;
    }
    public string GenerateJwtToken(User user)
    {

        if (user == null || string.IsNullOrEmpty(user.Email))
        {
            throw new ArgumentNullException(nameof(user.Email), "User email is null or empty.");
        }
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Email)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    public User GenerateResetToken(ForgotPasswordDTO dto)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);
        if (user == null)
        {
            throw new ArgumentException("User not found");
        }

        user.ResetToken = Guid.NewGuid().ToString();

        _context.SaveChanges();

        return user;
    }

    public bool ResetPassword(ResetPasswordDTO dto)
    {
        var user = _context.Users.FirstOrDefault(u => u.ResetToken == dto.ResetToken);
        if (user == null)
        {
            return false;
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
        user.ResetToken = null;
        _context.SaveChanges();

        return true;
    }
}