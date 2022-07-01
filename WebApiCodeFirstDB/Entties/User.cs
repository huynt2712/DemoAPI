using System.ComponentModel.DataAnnotations;

namespace BlogWebApi.Entties
{
    public class User
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
