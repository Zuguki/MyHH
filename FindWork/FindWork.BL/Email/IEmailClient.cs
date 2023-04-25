namespace FindWork.BL.Email;

public interface IEmailClient
{
    void SendEmail(string to, string from, string subject, string body);
}