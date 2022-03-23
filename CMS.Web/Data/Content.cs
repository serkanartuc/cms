using CMS.Web.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Web.Data
{
    public class Content
    {
        [Key]
        public int Id { get; set; }
        
        public int? ParentId { get; set; }
        
        [ForeignKey("ParentId")]
        public Content Parent { get; set; }

        public int OrderNo { get; set; }

        public ContentType Type { get; set; }

    }
}
