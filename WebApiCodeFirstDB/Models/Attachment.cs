using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BlogWebApi.ViewModel.Common.AttachmentEnums;

namespace BlogWebApi.Models
{
    [Table("Attachment")]
    public class Attachment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(300)]
        [Required]
        public string FileName { get; set; }

        [MaxLength(300)]
        [Required]
        public string FilePath { get; set; }

        [Required]
        public RefType RefType { get; set; }

        [Required]
        public int RefId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}
