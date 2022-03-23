using System.ComponentModel.DataAnnotations;

namespace CMS.Web.Data
{
    public class Language
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }
    }
}
