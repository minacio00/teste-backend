using System.ComponentModel.DataAnnotations;

namespace test_backend.Data.DTOs;

public class ForgotPasswordDTO
{
    [Required]
    [EmailAddress(ErrorMessage ="O campo email é obrigatório")]
    public string? Email { get; set; }
}
