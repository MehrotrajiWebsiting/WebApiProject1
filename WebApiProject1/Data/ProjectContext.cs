using Microsoft.EntityFrameworkCore;
using WebApiProject1.Models;

namespace WebApiProject1.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"data source = PG02372C; database = Project; Integrated Security = SSPI; Trusted_Connection = True; TrustServerCertificate = True;");
        //}
    }
}
