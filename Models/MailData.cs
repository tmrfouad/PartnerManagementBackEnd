public class MailData
{
    public SmtpData SMTP { get; set; }
    public MailMessageData Message { get; set; }
}

public class MailMessageData
{
    public MailMessageData(string [] To) {
        this.To = To ;
    }
    public string[] To { get; set; } = {"tmrfouad@gmail.com"};
    public string From { get; set; } = "tabuhmead@acs-me.com";
    public string Subject { get; set; } = "Test Subject" ;
    public string Body { get; set; } = "";
    public bool IsBodyHtml { get; set; } = true;
}

public class SmtpData
{
    public string Domain { get; set; } = "box756.bluehost.com" ;
    public int Port { get; set; } = 465;
    public bool EnableSsl { get; set; } = true ;
    public int Timeout { get; set; } = 3600000 ;
    public string DeliveryMethod { get; set; }
    public bool UseDefaultCredentials { get; set; } = false;
    public string UserName { get; set; } = "tabuhmead@acs-me.com" ;
    public string Password { get; set; } = "02uO+ez8" ;
}