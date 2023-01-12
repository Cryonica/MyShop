using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.IO;
using System.Linq;

namespace MyShop.Services
{
    public class SqlDbContext : IdentityDbContext
    {
        private readonly IConfiguration _configuration;
        public SqlDbContext(DbContextOptions<DbContext> options) : base(options)
        {
        }
       
        public SqlDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            }
            //options.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=TestJobBD11;Trusted_Connection=True;");
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<SalePoint> SalePoints { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleData> SaleDatas { get; set; }
        public DbSet<ProvidedProduct> ProvidedProducts { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Buyer>()
                .HasOne(b => b.User)
                .WithMany(u => u.Buyers)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Sale>()
                .HasMany(s => s.Products)
                .WithMany(p => p.Sales);
            
        }
        public void Seed()
        {
           if (!Buyers.Any())
            {
                var MockProducts = MockDataGenerator<Product>.GetMockItems("Products.json");
                var MockSailPoints = MockDataGenerator<SalePoint>.GetMockItems("SalePoints.json");
                //var MockBuyers = MockDataGenerator<Buyer>.GetMockItems("Buyers.json");

                //Buyers.AddRange(MockBuyers);
                Products.AddRange(MockProducts);
                SalePoints.AddRange(MockSailPoints);

                SaveChanges();
            }
        }
        public Buyer GetBuyerByUser(ApplicationUser user)
        {
            return Buyers.FirstOrDefault(b => b.UserId == user.Id);
        }
    }
}
       
