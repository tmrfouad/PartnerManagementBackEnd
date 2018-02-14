using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.IO;

public static class MailHelper
{
    public static void sendMail(MailData mail)
    {
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
    }

    public static string MessageBody(string name, string phone)
    {
        string messageBody;

        // var webRequest = WebRequest.Create(@"http://yourUrl");

        // using (var response = webRequest.GetResponse())
        // using(var content = response.GetResponseStream())
        // using(var reader = new StreamReader(content)){
        //     var strContent = reader.ReadToEnd();

        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MailTemplete.html");
        using (var file = new StreamReader(path))
        {
            messageBody = file.ReadToEnd();
            messageBody = messageBody.Replace("{{ NAME }}", name);
            messageBody = messageBody.Replace("{{ phone }}", phone);
        }
        return messageBody;
    }

}