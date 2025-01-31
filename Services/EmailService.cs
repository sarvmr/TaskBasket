using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

public class EmailService
{
    private readonly string _apiKey;
    private readonly string _fromEmail;
    private readonly string _fromName;

    public EmailService(IConfiguration configuration)
    {
        _apiKey = configuration["SendGrid:ApiKey"];
        _fromEmail = configuration["SendGrid:FromEmail"];
        _fromName = configuration["SendGrid:FromName"];
    }

    public async Task SendPasswordResetEmail(string toEmail, string resetToken)
    {
        var client = new SendGridClient(_apiKey);
        var from = new EmailAddress(_fromEmail, _fromName);
        var subject = "Password Reset Request";
        var to = new EmailAddress(toEmail);
        var resetUrl = $"http://localhost:3000/reset-password/{resetToken}";

        var htmlContent = $"<p>Click <a href='{resetUrl}'>here</a> to reset your password.</p>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
        
        var response = await client.SendEmailAsync(msg);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to send email: {response.StatusCode}");
        }
    }
}
