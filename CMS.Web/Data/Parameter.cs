using System.ComponentModel.DataAnnotations;

namespace CMS.Web.Data
{
    public class Parameter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Group { get; set; }

        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Value { get; set; }
    }
}
