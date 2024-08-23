using System.ComponentModel.DataAnnotations;

namespace test_backend.Data.DTOs;
public class PostUserDTO
{
    [Required(ErrorMessage = "E-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido. Insira um endereço de e-mail no formato correto.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Senha é obrigatória.")]
    [StringLength(32, MinimumLength = 8, ErrorMessage = "Senha inválida. Verifique se a senha tem pelo menos 8 caracteres, com letras maiúsculas, minúsculas, números e caracteres especiais.")]
    public string? Password { get; set; }
}