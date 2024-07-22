using Microsoft.EntityFrameworkCore;
using ValeShop.Models;

namespace ValeShop.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<ProductPromo> ProductPromos { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<BillingDetails> BillingDetails { get; set; }
        public DbSet<Promo> Promos { get; set; }

        
    }
}