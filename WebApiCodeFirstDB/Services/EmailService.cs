namespace WebApiCodeFirstDB.Services
{
    public class EmailService: IEmailService
    {
        public string SendMail(string from, string to, string title, string content, string cc)
        {
            return $"Send form {from} to {to} .... {title}";
        }
    }

    public class EmailNewService : IEmailService
    {
        public string SendMail(string from, string to, string title, string content, string cc)
        {
            return $"Send form {from} to {to} .... {title}";
        }
    }
}
