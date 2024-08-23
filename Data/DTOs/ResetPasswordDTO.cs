using System.ComponentModel.DataAnnotations;

namespace test_backend.Data.DTOs;
public class ResetPasswordDTO
{
    [Required]
    public string? ResetToken { get; set; }


    [Required(ErrorMessage = "Senha é obrigatória.")]
    [StringLength(32, MinimumLength = 8, ErrorMessage = "Senha inválida. Verifique se a senha tem pelo menos 8 caracteres, com letras maiúsculas, minúsculas, números e caracteres especiais.")]
    public string? NewPassword { get; set; }
}
