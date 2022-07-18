using LogAn.Interfaces;

namespace LogAn.UnitTests.Fakes;

public class FakeMockEmailService : IEmailService
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    
    public void SendEmail(string to, string subject, string body)
    {
        To = to;
        Subject = subject;
        Body = body;
    }
}