using Microsoft.EntityFrameworkCore;

namespace CMS.Web.Data
{
    public class CmsDbContext:DbContext
    {
        public CmsDbContext(DbContextOptions<CmsDbContext> options)
        : base(options)
        {
        }               

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    IConfigurationRoot configuration = new ConfigurationBuilder()
        //    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        //    .AddJsonFile("appsettings.json")
        //    .Build();
        //    optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        //}

        public DbSet<ContactForm> ContactForms { get; set; }

        public DbSet<Content> Contents { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Localization> Localizations { get; set; }

        public DbSet<Parameter> Parameters { get; set; }

        public DbSet<Slider> Sliders { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
