using Microsoft.EntityFrameworkCore;
namespace webmnv_ef_01.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.Migrate();

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Customer> Customers { get; set; }
    }

}