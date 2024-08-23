using System.ComponentModel.DataAnnotations;

namespace test_backend.Models;


public class User
{
    [Key]
    public int Id {get; set; }

    [Required(ErrorMessage = "O campo Email é obrigatório")]
    [EmailAddress]
    public string? Email {get; set; }

    public string? Password {get; set;}

    public string? ResetToken {get; set;}
    }