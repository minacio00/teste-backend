using test_backend.Data.DTOs;
using test_backend.Models;

namespace test_backend.Services;
public interface IAuthService
{
    ReadUserDTO? GetUserById(int id);
    User GenerateResetToken(ForgotPasswordDTO dto);
    bool ResetPassword(ResetPasswordDTO dto);
    User RegisterUser(CreateUserDTO dto);
    User? ValidateUser(string email, string password);
    string GenerateJwtToken(User user);
}
