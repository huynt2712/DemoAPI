namespace WebApiCodeFirstDB.Services
{
    public class UserService
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string DateOfBirth { get; set; }


        private IEmailService emailService;

        public UserService(string userName, string password, string dateOfBirth, IEmailService emailService)
        {
            UserName = userName;
            Password = password;
            DateOfBirth = dateOfBirth;
            this.emailService = emailService;
        }

        public string SendMail()
        {
            //var emailService = new EmailService(); //UserService dependece EmailService
            //DI injection IEmailService into constructor of UserService
            //UserService interface
            //OOP interface 

            var email1 = new  EmailService(); //trasient 

            var email2 = new EmailService(); //Singleton

            return emailService.SendMail(UserName, "test", "test title", "test content", "CC test");
        }
    }
}
