using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CMS.Web.Data
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(25)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(150)]
        public string Email { get; set; }

        [Required]
        [MaxLength(250)]
        public string Password { get; set; }
    }
}
