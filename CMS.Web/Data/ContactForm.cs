namespace CMS.Web.Data
{
    public class ContactForm
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public int Phone { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
