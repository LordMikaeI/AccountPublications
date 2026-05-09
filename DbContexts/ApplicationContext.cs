using Microsoft.EntityFrameworkCore;
using Kursach_Backend.Models.Users;
using Kursach_Backend.Models.Employees;
using Kursach_Backend.Models.Publications;

namespace Kursach_Backend.DbContexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null;
        public DbSet<Employee> Employees { get; set; } = null;
        public DbSet<Publication> Publications { get; set; } = null;
        public DbSet<Monograph> Monographs { get; set; } = null;
        public DbSet<Article> Articles { get; set; } = null;
        public DbSet<Conference> Conferences { get; set; } = null;
        public DbSet<Dissertation> Dissertations { get; set; } = null;
        public DbSet<Textbook> Textbooks { get; set; } = null;
        public DbSet<Patent> Patents { get; set; } = null;
        public DbSet<ScientificPublication> ScientificPublications { get; set; } = null;
        public DbSet<PublicationEdition> PublicationEditions { get; set; } = null;
        public DbSet<PublicationFile> PublicationFiles { get; set; } = null;

        public ApplicationContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=Kursach_Backend;Username=postgres;Password=123");
        }
    }
}
