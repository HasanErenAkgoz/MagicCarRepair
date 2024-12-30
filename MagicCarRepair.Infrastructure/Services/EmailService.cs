using MagicCarRepair.Application.Interfaces;
using MagicCarRepair.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace MagicCarRepair.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort);
        var message = new MailMessage
        {
            From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        message.To.Add(to);

        await client.SendMailAsync(message);
    }

    public async Task SendPasswordResetEmailAsync(string email, string resetToken, string userName)
    {
        var resetLink = $"{_emailSettings.PasswordResetUrl}?token={resetToken}&email={email}";
        var subject = "Şifre Sıfırlama Talebi";
        var body = $"Merhaba {userName},<br/><br/>Şifrenizi sıfırlamak için <a href='{resetLink}'>buraya tıklayın</a>.<br/><br/>Teşekkürler,<br/>MagicCarRepair Ekibi";

        await SendEmailAsync(email, subject, body);
    }
} 