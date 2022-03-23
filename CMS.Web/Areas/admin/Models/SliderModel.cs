using CMS.Web.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CMS.Web.Areas.admin.Models
{
    public class SliderModel
    {        
        public int Id { get; set; }

        [Required(ErrorMessage = "Dil alanı gereklidir!")]
        [Range(1,int.MaxValue, ErrorMessage = "Dil alanı gereklidir!")]
        public int? LanguageId { get; set; }
        
        [MaxLength(100)]
        public string? ImagePath { get; set; }
        
        [Required(ErrorMessage = "Sıra numarası alanı gereklidir!")]
        [Range(1, int.MaxValue, ErrorMessage = "Sıra numarası en az 1 olabilir!")]
        public int? OrderNo { get; set; }

        [Required(ErrorMessage = "Başlık alanı gereklidir!")]
        [MaxLength(150, ErrorMessage = "Başlık alanı en fazla 150 karakter olabilir!")]
        public string Title { get; set; }
        
        public IEnumerable<SelectListItem>? LanguageList { get; set; }
    }
}
