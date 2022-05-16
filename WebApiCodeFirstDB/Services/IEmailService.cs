namespace WebApiCodeFirstDB.Services
{
    public interface IEmailService
    {
        public string SendMail(string from, string to, string title, string content, string cc);
    }
}
