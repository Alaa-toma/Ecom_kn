using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KAshop.BLL.Service
{
    public class EmailSender : IEmailSender
    {
        Task IEmailSender.SendEmailAsync(string email, string subject, string message)
        {

            var client = new SmtpClient("smtp.gmail.com")
            {
                Port =587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("alaatoma58@gmail.com", "hqluwvmojrviyykp")
            };

            return client.SendMailAsync(
                new MailMessage(from: "alaatoma58@gmail.com",
                                to: email,
                                subject,
                                message
                                )
                { IsBodyHtml = true }
                );
        }
    }
}
