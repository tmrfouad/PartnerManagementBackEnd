public class MailData
{
    public SmtpData SMTP { get; set; }
    public MailMessageData Message { get; set; }
}

public class MailMessageData
{
    public string[] To { get; set; }
    public string From { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public bool IsBodyHtml { get; set; }
}

public class SmtpData
{
    public string Domain { get; set; }
    public int Port { get; set; }
    public bool EnableSsl { get; set; }
    public int Timeout { get; set; }
    public string DeliveryMethod { get; set; }
    public bool UseDefaultCredentials { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}