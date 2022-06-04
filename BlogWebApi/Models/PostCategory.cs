using System.ComponentModel.DataAnnotations;

namespace BlogWebApi.Models
{
    public class PostCategory
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string Slug { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public virtual List<Post>? Posts { get; set; } 
    }
}
