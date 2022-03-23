using System.ComponentModel.DataAnnotations;

namespace CMS.Web.Areas.admin.Models
{
    public class UserModel 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ad alanı gereklidir.")]
        [MinLength(3, ErrorMessage = "Ad alanı en az {1} karakter olabilir.")]
        [MaxLength(25, ErrorMessage = "Ad alanı en fazla {1} karakter olabilir.")]
        [Display(Name = "Ad")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Soyad alanı gereklidir.")]
        [MinLength(3, ErrorMessage = "Soyad alanı en az {1} karakter olabilir.")]
        [MaxLength(25, ErrorMessage = "Soyad alanı en fazla {1} karakter olabilir.")]
        [Display(Name = "Soyad")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "E-posta alanı gereklidir.")]
        [MaxLength(150, ErrorMessage = "E-posta alanı en fazla {1} karakter olabilir.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Lütfen geçerli bir E-posta adresi giriniz.")]
        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı gereklidir.")]
        [MinLength(5, ErrorMessage = "Şifre alanı en az {1} karakter olabilir.")]
        [MaxLength(16, ErrorMessage = "Şifre alanı en fazla {1} karakter olabilir.")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre(tekrar) alanı gereklidir.")]
        [MinLength(5, ErrorMessage = "Şifre(tekrar) alanı en az {1} karakter olabilir.")]
        [MaxLength(16, ErrorMessage = "Şifre(tekrar) alanı en fazla {1} karakter olabilir.")]
        [Compare("Password",ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }        
        
    }
}
