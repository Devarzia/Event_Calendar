using EventCalendar.Domain;
using System.Threading.Tasks;

namespace EventCalendar.Application;
public interface IEmailService
{
    Task SendRegistrationConfirmationEmail(ApplicationUser user, string callbackUrl);
    Task SendEmailChangeConfirmationEmail(ApplicationUser user, string callbackUrl);
    Task SendForgotPasswordConfirmationEmail(ApplicationUser user, string callbackUrl);
    Task SendEmailAsync(string email, string subject, string message);
}