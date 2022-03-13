using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Smarty.Data.Configurations.MailSettingsConfigurations;
using System.Net;
using System.Net.Mail;

namespace Smarty.Data.Services.Implementations
{
	public class EmailSender : IEmailSender
	{
		private readonly MailSettings _mailSettings;
		public EmailSender(IOptions<MailSettings> mailSettings)
		{
			_mailSettings = mailSettings.Value;
		}
		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			var from = new MailAddress(_mailSettings.Email);
			var to = new MailAddress(email);

			var message = new MailMessage(from, to);
			message.Subject = subject;
			message.Body = $"<html><body>{htmlMessage}</body></html>";
			message.IsBodyHtml = true;
			var smtpClient = new SmtpClient(_mailSettings.Host)
			{
				Port=_mailSettings.Port,
				Credentials = new NetworkCredential(_mailSettings.Email, _mailSettings.Password),
				EnableSsl = true
			};
			smtpClient.Send(message);

		}

	}
}
