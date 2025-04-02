namespace BUMessenger.Domain.Interfaces.Services;

public interface IEmailService
{
    /// <summary>
    /// Отправляет письмо
    /// </summary>
    /// <param name="toEmail">Адрес, куда отправить</param>
    /// <param name="subject">Тема письма</param>
    /// <param name="body">Тело письма</param>
    /// <returns></returns>
    Task SendEmailAsync(string toEmail, string subject, string body);
}