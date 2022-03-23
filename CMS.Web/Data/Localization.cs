using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Web.Data
{
    public class Localization
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [MaxLength(250)]
        public string ShortContent { get; set; }

        public string FullContent { get; set; }
        
        public int ContentId { get; set; }

        [ForeignKey("ContentId")]
        public Content Content { get; set; }

        [Required]
        [MaxLength(150)]
        public string SeoLink { get; set; }
        
        public int LanguageId { get; set; }

        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
    }
}
