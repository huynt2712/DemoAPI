using System.ComponentModel.DataAnnotations;

namespace WebApiCodeFirstDB.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public string Content { get; set; }

        [Required]
        [MaxLength(50)]
        public string Slug { get; set; }
        public string Image { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int PostCategoryId { get; set; }
        public virtual PostCategory PostCategory { get; set; }
    }
}
