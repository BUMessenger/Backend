using System.Net.Mail;
using BUMeesenger.Domain.Exceptions.Services.EmailServiceExceptions;
using BUMessenger.Domain.Interfaces.Services;
using BUMessenger.Domain.Models.Models;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using SmtpStatusCode = MailKit.Net.Smtp.SmtpStatusCode;

namespace BUMessenger.Application.Services.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<IEmailService> _logger;

    public EmailService(IOptions<EmailSettings> emailSettings,
        ILogger<IEmailService> logger)
    {
        _emailSettings = emailSettings.Value ?? throw new ArgumentNullException(nameof(emailSettings));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromAddress));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, _emailSettings.UseSsl);
                await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
        catch (SmtpCommandException ex) when (ex.StatusCode == SmtpStatusCode.MailboxUnavailable)
        {
            _logger.LogInformation("Mailbox {ToEmail} is unavailable.", toEmail);
            throw new ReceiverDoesntExistEmailServiceException($"Mailbox {toEmail} is unavailable.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to send message to mailbox {ToEmail}.", toEmail);
            throw new EmailServiceException($"Failed to send message to mailbox {toEmail}.", e);
        }
    }
}