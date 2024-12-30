namespace MagicCarRepair.Application.Interfaces;

public interface IEmailService
{
    Task SendPasswordResetEmailAsync(string email, string resetToken, string userName);
} 