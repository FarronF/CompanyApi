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

            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = 1,
                    Name = "Apple Inc.",
                    Exchange = "NASDAQ",
                    Ticker = "AAPL",
                    Isin = "US0378331005",
                    Website = "http://www.apple.com"
                },
                new Company
                {
                    Id = 2,
                    Name = "British Airways Plc",
                    Exchange = "Pink Sheets",
                    Ticker = "BAIRY",
                    Isin = "US1104193065",
                },
                new Company
                {
                    Id = 3,
                    Name = "Heineken NV",
                    Exchange = "Euronext Amsterdam",
                    Ticker = "HEIA",
                    Isin = "NL0000009165",
                },
                new Company
                {
                    Id = 4,
                    Name = "Panasonic Corp",
                    Exchange = "Tokyo Stock Exchange",
                    Ticker = "6752",
                    Isin = "JP3866800000",
                    Website = "http://www.panasonic.co.jp"
                },
                new Company
                {
                    Id = 5,
                    Name = "Porsche Automobil",
                    Exchange = "Deutsche Börse",
                    Ticker = "PAH3",
                    Isin = "DE000PAH0038",
                    Website = "https://www.porsche.com/"
                }
            );
        }
    }
}
