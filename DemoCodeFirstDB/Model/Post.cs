using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCodeFirstDB.Model
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        [Required]
        [MaxLength(500)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
