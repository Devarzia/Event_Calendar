using EventCalendar.Domain;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace EventCalendar.Application;
public class EmailService : IEmailService, IEmailSender
{

    private readonly string _apiKey;
    private readonly string _fromName;
    private readonly string _fromEmail;

    public EmailService(IConfiguration configuration)
    {
        _apiKey = configuration["SendGrid:ApiKey"];
        _fromName = configuration["SendGrid:FromName"];
        _fromEmail = configuration["SendGrid:FromEmail"];
    }

    public async Task SendContactFormSubmissionEmail(ContactDTO dto)
    {
        // TO-DO: Create template and change templateID
        var client = new SendGridClient(_apiKey);
        var templateID = "d-95e6aca9b5794c2ea3538f3e1122ff1a";
        var from = new EmailAddress(_fromEmail, _fromName);
        var to = new EmailAddress(dto.Email);
        var dynamicTemplateData = new
        {
            subject = $"Your Message has Been Recieved",
            recipient = $"{dto.FirstName} {dto.LastName}",
            message = $"<p>Thank you for sending us a message! We intend to respond you your message within 24 hours.</p>"
        };

        var sendMessage = MailHelper.CreateSingleTemplateEmail(from, to, templateID, dynamicTemplateData);

        var response = await client.SendEmailAsync(sendMessage);

        if (response.IsSuccessStatusCode)
        {
            System.Console.WriteLine("Email Sent");
        }
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var client = new SendGridClient(_apiKey);
        var templateID = "d-95e6aca9b5794c2ea3538f3e1122ff1a";
        var from = new EmailAddress(_fromEmail, _fromName);
        var to = new EmailAddress(email);
        var dynamicTemplateData = new
        {
            subject,
            recipient = email,
            message,
        };

        var sendMessage = MailHelper.CreateSingleTemplateEmail(from, to, templateID, dynamicTemplateData);

        var response = await client.SendEmailAsync(sendMessage);

        if (response.IsSuccessStatusCode)
        {
            System.Console.WriteLine("Email Sent");
        }
    }

    public async Task SendEmailChangeConfirmationEmail(ApplicationUser user, string callbackUrl)
    {
        // TO-DO: Create template and change templateID
        var client = new SendGridClient(_apiKey);
        var templateID = "d-95e6aca9b5794c2ea3538f3e1122ff1a";
        var from = new EmailAddress(_fromEmail, _fromName);
        var to = new EmailAddress(user.Email);
        var dynamicTemplateData = new
        {
            subject = $"A change has been made to your account".ToUpperInvariant(),
            recipient = $"{user.FirstName} {user.LastName}",
            message = $"<p>A change has been made to your account. The email has changed." +
            $"To confirm this change to your account, click this <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>link</a>.</p><br />" +
            $"<p>Please contact us if you have any questions, comments or concerns!</p>"
        };

        var sendMessage = MailHelper.CreateSingleTemplateEmail(from, to, templateID, dynamicTemplateData);

        var response = await client.SendEmailAsync(sendMessage);

        if (response.IsSuccessStatusCode)
        {
            System.Console.WriteLine("Email Sent");
        }
    }

    public async Task SendForgotPasswordConfirmationEmail(ApplicationUser user, string callbackUrl)
    {
        // TO-DO: Create template and change templateID
        var client = new SendGridClient(_apiKey);
        var templateID = "d-95e6aca9b5794c2ea3538f3e1122ff1a";
        var from = new EmailAddress(_fromEmail, _fromName);
        var to = new EmailAddress(user.Email);
        var dynamicTemplateData = new
        {
            subject = $"Reset Password Request",
            recipient = $"{user.FirstName} {user.LastName}",
            message = $"<p>A request to change your password on your account has been made." +
            $"To reset your password, click this <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>link</a>.</p><br />" +
            $"<p>Please contact us if you have any questions, comments or concerns!</p>"
        };

        var sendMessage = MailHelper.CreateSingleTemplateEmail(from, to, templateID, dynamicTemplateData);

        var response = await client.SendEmailAsync(sendMessage);

        if (response.IsSuccessStatusCode)
        {
            System.Console.WriteLine("Email Sent");
        }
    }

    public async Task SendRegistrationConfirmationEmail(ApplicationUser user, string callbackUrl)
    {
        var client = new SendGridClient(_apiKey);
        var templateID = "d-95e6aca9b5794c2ea3538f3e1122ff1a";
        var from = new EmailAddress(_fromEmail, _fromName);
        var to = new EmailAddress(user.Email);
        var dynamicTemplateData = new
        {
            subject = $"Thank you for registering, {user.FirstName} {user.LastName}",
            recipient = $"{user.FirstName} {user.LastName}",
            message = $"<p>Thank you for registering an account with us! " +
            $"Please confirm your account by clicking this  <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>link</a>.</p><br />" +
            $"<p>Please contact us if you have any questions, comments or concerns!</p>"
        };

        var sendMessage = MailHelper.CreateSingleTemplateEmail(from, to, templateID, dynamicTemplateData);

        var response = await client.SendEmailAsync(sendMessage);

        if (response.IsSuccessStatusCode)
        {
            System.Console.WriteLine("Email Sent");
        }
    }
}