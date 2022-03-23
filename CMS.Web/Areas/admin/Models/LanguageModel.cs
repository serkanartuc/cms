using System.ComponentModel.DataAnnotations;

namespace CMS.Web.Areas.admin.Models
{
    public class LanguageModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }
    }
}
