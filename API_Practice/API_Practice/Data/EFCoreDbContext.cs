using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using API_Practice.Models;

namespace API_Practice.Data
{
    public class EFCoreDbContext : DbContext
    {
        public EFCoreDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MIHEER-LAPTOP;Database=NET_FSD;Trusted_Connection=True;" +
                "Encrypt=True;TrustServerCertificate=True;");
            //base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Branch> Branches { get; set; }
    }
}
