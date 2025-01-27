using UNI_Management.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mime;
using static System.Net.Mime.MediaTypeNames;
using Azure.Core;

namespace UNI_Management.Common.Email
{
    public static class EmailHelper
    {
        public static bool SendMail(string to, string subject, string body)
        {
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                try
                {
                    client.Credentials = new NetworkCredential("noreply.ventasemail@gmail.com", "pfhh fave jogk mcnv");
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("noreply.ventasemail@gmail.com"),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(to);

                    client.Send(mailMessage);

                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public static bool SendMail(string to, string subject, string body, byte[] qrCodeBytes)
        {
            using (var client = new SmtpClient(ConfigItems.SMTPHost, ConfigItems.SMTPPort))
            {
                try
                {
                    // Set the credentials and enable SSL
                    client.Credentials = new NetworkCredential(ConfigItems.EmailSender, ConfigItems.EmailPassword);
                    client.EnableSsl = true;

                    // Create the mail message
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(ConfigItems.EmailSender),
                        Subject = subject,
                        IsBodyHtml = true // Set HTML support for the body
                    };
                    mailMessage.To.Add(to);

                    // Create an alternate view for the HTML email body
                    var htmlView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

                    // Add the QR code image as a LinkedResource
                    LinkedResource qrImage = new LinkedResource(new MemoryStream(qrCodeBytes), MediaTypeNames.Image.Png)
                    {
                        ContentId = "qrcode", // The Content-ID for the image
                        TransferEncoding = TransferEncoding.Base64
                    };
                    htmlView.LinkedResources.Add(qrImage);

                    // Attach the alternate view to the email message
                    mailMessage.AlternateViews.Add(htmlView);

                    // Send the email
                    client.Send(mailMessage);

                    return true;
                }
                catch (Exception ex)
                {
                    // Handle/log exception as necessary
                    return false;
                }
            }
        }

        public static bool SendMail(string to, string subject, string body, string cc = null)
        {
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                try
                {
                    client.Credentials = new NetworkCredential("noreply.ventasemail@gmail.com", "pfhh fave jogk mcnv");
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("noreply.ventasemail@gmail.com"),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(to);

                    // If CC is provided, split the string and add each CC email
                    if (!string.IsNullOrEmpty(cc))
                    {
                        foreach (var ccEmail in cc.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            mailMessage.CC.Add(ccEmail.Trim());
                        }
                    }

                    client.Send(mailMessage);

                    return true;
                }
                catch (Exception ex)
                {
                    EmailHelper.SendMail("lonavala.thewalkiestalkies14824@gmail.com", "Log-In OTP for RB-News", DateTime.Now + " in email cc  Exception"+ex.Message+ " " +ex.InnerException);
                    // Handle exceptions (log, rethrow, etc.)
                    return false;
                }
            }
        }
    }
}
