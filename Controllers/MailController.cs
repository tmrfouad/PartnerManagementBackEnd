using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[EnableCors("AllowSpecificOrigin")]
public class MailController : Controller
{
    // POST api/Mail
    [HttpPost]
    public ActionResult Post([FromBody]MailData mail)
    {
        if (mail == null)
        {
            BadRequest();
        }

        SmtpClient client = new SmtpClient(mail.SMTP.Domain);
        client.Port = mail.SMTP.Port;
        client.EnableSsl = mail.SMTP.EnableSsl;
        client.Timeout = mail.SMTP.Timeout;
        client.DeliveryMethod = Enum.GetValues(typeof(SmtpDeliveryMethod))
            .Cast<SmtpDeliveryMethod>()
            .SingleOrDefault(o => o.ToString() == mail.SMTP.DeliveryMethod);
        client.UseDefaultCredentials = mail.SMTP.UseDefaultCredentials;
    
        client.Credentials = new NetworkCredential(mail.SMTP.UserName, mail.SMTP.Password);
        MailMessage msg = new MailMessage();
        foreach (var item in mail.Message.To)
        {
            msg.To.Add(new MailAddress(item));
        }
        msg.From = new MailAddress(mail.Message.From);
        msg.Subject = mail.Message.Subject;
        msg.Body = mail.Message.Body;
        msg.IsBodyHtml = mail.Message.IsBodyHtml;
        client.Send(msg);

        return new NoContentResult();
    }
}