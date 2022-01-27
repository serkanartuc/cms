using CMS.Web.Helpers;

namespace CMS.Web.Data
{
    public class Content
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public Content Parent { get; set; }

        public int OrderNo { get; set; }

        public ContentType Type { get; set; }

        public Localization Localization { get; set; }
    }
}
