using CompanyApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyApi.Data
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        {
        }
        public DbSet<Company> Companies { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasIndex(c => c.Isin).IsUnique();
            modelBuilder.Entity<Company>().HasIndex(c => c.Ticker).IsUnique();
            modelBuilder.Entity<Company>().HasIndex(c => c.Name);
        }
    }
}
