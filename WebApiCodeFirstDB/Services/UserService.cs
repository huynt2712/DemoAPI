namespace WebApiCodeFirstDB.Services
{
    public class UserService
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string DateOfBirth { get; set; }


        private IEmailService emailService;

        public UserService(string userName, string password, string dateOfBirth,
            IEmailService emailService)
        {
            UserName = userName;
            Password = password;
            DateOfBirth = dateOfBirth;
            this.emailService = emailService;
        }

        public string SendMail()
        {
            return emailService.SendMail(UserName, "test", "test title", "test content", "CC test");
        }
    }
}
