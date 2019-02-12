using EruditionJournal.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EruditionJournal.DAL
{
    public class PublicationContext : DbContext
    {
        public PublicationContext() : base("PublicationContext")
        {
        }

        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}