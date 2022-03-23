using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Web.Data
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }

        public int LanguageId { get; set; }

        [ForeignKey("LanguageId")]
        public Language Language { get; set; }

        [Required]
        [MaxLength(100)]
        public string ImagePath { get; set; }

        public int OrderNo { get; set; }

        [MaxLength(150)]
        public string Title { get; set; }
    }
}
