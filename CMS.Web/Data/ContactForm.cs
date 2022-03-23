using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Web.Data
{
    public class ContactForm
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(150)]
        // Todo:Email attribute'una bakılacak.
        public string Email { get; set; }

        public long Phone { get; set; }

        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }

        // Todo: Kayıt update edildiği zaman değişiyor mu diye bakılacak.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName ="smalldatetime")]
        public DateTime CreationTime { get; set; }
    }
}
