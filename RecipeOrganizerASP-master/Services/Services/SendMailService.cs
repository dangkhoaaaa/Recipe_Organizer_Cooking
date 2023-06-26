using System;
using System.Security.Policy;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

public class MailSettings
{
	public string Mail { get; set; }
	public string DisplayName { get; set; }
	public string Password { get; set; }
	public string Host { get; set; }
	public int Port { get; set; }

}

public interface IEmailSender
{
	Task SendEmailAsync(string email, string subject, string message);
    Task SendEmailOTPAsync(string email, string subject, string message);
    Task SendSmsAsync(string number, string message);
}

public class SendMailService : IEmailSender
{


	private readonly MailSettings mailSettings;

	private readonly ILogger<SendMailService> logger;


	// mailSetting được Inject qua dịch vụ hệ thống
	// Có inject Logger để xuất log
	public SendMailService(IOptions<MailSettings> _mailSettings, ILogger<SendMailService> _logger)
	{
		mailSettings = _mailSettings.Value;
		logger = _logger;
		logger.LogInformation("Create SendMailService");
	}



	public Task SendSmsAsync(string number, string message)
	{
		// Cài đặt dịch vụ gửi SMS tại đây
		System.IO.Directory.CreateDirectory("smssave");
		var emailsavefile = string.Format(@"smssave/{0}-{1}.txt", number, Guid.NewGuid());
		System.IO.File.WriteAllTextAsync(emailsavefile, message);
		return Task.FromResult(0);
	}

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage();
        message.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
        message.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = subject;

        string contactPage = "https://localhost:7186/Contact/SendContact";
        var builder = new BodyBuilder();
        builder.HtmlBody = 
            "<tr><td align='center' bgcolor='#e9ecef'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'><tr><td align='left' bgcolor='#ffffff' style='padding: 36px 24px 0; font-family: Helvetica, Arial, sans-serif; border-top: 3px solid #d4dadf;'><h1 style='margin: 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;'>Welcome to Recipe Organizer App!</h1></td></tr></table></td></tr><tr><td align='center' bgcolor='#e9ecef'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'><tr><td align='left' bgcolor='#ffffff' style='padding: 24px; font-family: Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;'><p style='margin: 0;'>You have been registered in the Recipe Organizer app. Please confirm your account by clicking the button below:</td></tr><tr><td align='left' bgcolor='#ffffff'><table border='0' cellpadding='0' cellspacing='0' width='100%'><tr><td align='center' bgcolor='#ffffff' style='padding: 12px;'><table border='0' cellpadding='0' cellspacing='0'><tr><td align='center' bgcolor='#1a82e2' style='border-radius: 6px;'><a href='"
            + HtmlEncoder.Default.Encode(htmlMessage) + 
            "' class='btn btn-primary' style='padding: 16px 36px; font-family: Helvetica, Arial, sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px; background-color: #ff472f; border-color: #ffffff; font-weight: bold;'>Click here</a></td></tr></table></td></tr></table></td></tr><tr><td align='left' bgcolor='#ffffff' style='padding: 24px; font-family:Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;'><p style='margin: 0;'>If that doesn't work, contact to our: " +
            "<a href='"+ contactPage + "' target='_blank'>Contact</a></p></td></tr><tr><td align='left' bgcolor='#ffffff' style='padding: 12px 24px; font-family: Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px; border-bottom: 3px solid #d4dadf'><p style='margin: 0;'>Cheers,<br>Recipe Organizer</p></td></tr></table></td></tr><tr><td align='center' bgcolor='#e9ecef' style='padding: 12px 24px;'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'><tr><td align='center' bgcolor='#e9ecef' style='padding: 12px 24px; font-family:  Helvetica, Arial, sans-serif; font-size: 14px; line-height: 20px; color: #666;'><p style='margin: 0;'>You received this email because we received a request for verify for your account. If you didn't request registered you can safely delete this email.</p></td></tr><tr><td align='center' bgcolor='#e9ecef' style='padding: 12px 24px; font-family:  Helvetica, Arial, sans-serif; font-size: 14px; line-height: 20px; color: #666;'><p style='margin: 0;'>Thu Duc city, Ho Chi Minh city</p></td></tr></table></td></tr></table></body></html>";
        message.Body = builder.ToMessageBody();

        // dùng SmtpClient của MailKit
        using var smtp = new MailKit.Net.Smtp.SmtpClient();

        try
        {
            smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
            await smtp.SendAsync(message);
        }

        catch (Exception ex)
        {
            // Gửi mail thất bại, nội dung email sẽ lưu vào thư mục mailssave
            System.IO.Directory.CreateDirectory("mailssave");
            var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
            await message.WriteToAsync(emailsavefile);

            logger.LogInformation("Error email sending, log at - " + emailsavefile);
            logger.LogError(ex.Message);
        }

        smtp.Disconnect(true);

        logger.LogInformation("send mail to " + email);


    }

    public async Task SendEmailOTPAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage();
        message.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
        message.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = subject;


        var builder = new BodyBuilder();
        builder.HtmlBody = htmlMessage;
        message.Body = builder.ToMessageBody();

        // dùng SmtpClient của MailKit
        using var smtp = new MailKit.Net.Smtp.SmtpClient();

        try
        {
            smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
            await smtp.SendAsync(message);
        }

        catch (Exception ex)
        {
            // Gửi mail thất bại, nội dung email sẽ lưu vào thư mục mailssave
            System.IO.Directory.CreateDirectory("mailssave");
            var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
            await message.WriteToAsync(emailsavefile);

            logger.LogInformation("Error email sending, log at - " + emailsavefile);
            logger.LogError(ex.Message);
        }

        smtp.Disconnect(true);

        logger.LogInformation("send mail to " + email);


    }

    //	public string formatEmail(string? callbackUrl)
    //	{
    //		string format = @$"<tr><td align='center' bgcolor='#e9ecef'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'><tr><td align='left' bgcolor='#ffffff' style='padding: 36px 24px 0; font-family: Helvetica, Arial, sans-serif; border-top: 3px solid #d4dadf;'><h1 style='margin: 0; font-size: 32px; font-weight: 700; letter-spacing: -1px; line-height: 48px;'>Welcome to Recipe Organizer App!</h1></td></tr></table></td></tr><tr><td align='center' bgcolor='#e9ecef'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'><tr><td align='left' bgcolor='#ffffff' style='padding: 24px; font-family: Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;'><p style='margin: 0;'>You have been registered in the Recipe Organizer app. Please confirm your account by clicking the button below:</td></tr><tr><td align='left' bgcolor='#ffffff'><table border='0' cellpadding='0' cellspacing='0' width='100%'><tr><td align='center' bgcolor='#ffffff' style='padding: 12px;'><table border='0' cellpadding='0' cellspacing='0'><tr><td align='center' bgcolor='#1a82e2' style='border-radius: 6px;'><a href='{HtmlEncoder.Default.Encode(callbackUrl)}' class='btn btn-primary' style='padding: 16px 36px; font-family: Helvetica, Arial, sans-serif; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px; background-color: #ff472f; border-color: #ffffff; font-weight: bold;'>Click here</a></td></tr></table></td></tr></table></td></tr><tr><td align='left' bgcolor='#ffffff' style='padding: 24px; font-family:Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px;'><p style='margin: 0;'>If that doesn't work, contact to our: <a href={Url.Page("Contact")} target='_blank'>Contact</a></p></td></tr><tr><td align='left' bgcolor='#ffffff' style='padding: 12px 24px; font-family: Helvetica, Arial, sans-serif; font-size: 16px; line-height: 24px; border-bottom: 3px solid #d4dadf'><p style='margin: 0;'>Cheers,<br>Recipe Organizer</p></td></tr></table></td></tr><tr><td align='center' bgcolor='#e9ecef' style='padding: 12px 24px;'><table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'><tr><td align='center' bgcolor='#e9ecef' style='padding: 12px 24px; font-family:  Helvetica, Arial, sans-serif; font-size: 14px; line-height: 20px; color: #666;'><p style='margin: 0;'>You received this email because we received a request for verify for your account. If you didn't request registered you can safely delete this email.</p></td></tr><tr><td align='center' bgcolor='#e9ecef' style='padding: 12px 24px; font-family:  Helvetica, Arial, sans-serif; font-size: 14px; line-height: 20px; color: #666;'><p style='margin: 0;'>Thu Duc city, Ho Chi Minh city</p></td></tr></table></td></tr></table></body></html>";
    //"		return format;
    //	}
}