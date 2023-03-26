using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class OnlineShopDbContext : DbContext
    {
        public OnlineShopDbContext() : base()
        {

        }

        public OnlineShopDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=LAPTOP-AT94SBBO\SQLEXPRESS;Database=ITShopDb;Trusted_Connection=True;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // If you have only DbSet<Abstract class> you must add these lines.
            modelBuilder.Entity<Software>();
            modelBuilder.Entity<Hardware>();
            
            // ❗ Mandatory => Set multiple primary keys!
            modelBuilder.Entity<ProductsOrders>().HasKey(po => new { po.OrderId, po.ProductId });
            
            // Optional => You can set the foreign keys with [ForeignKey]
            modelBuilder.Entity<ProductsOrders>().HasOne(po => po.Order).WithMany(o => o.ProductsOrders).HasForeignKey(po => po.OrderId);
            modelBuilder.Entity<ProductsOrders>().HasOne(po => po.Product).WithMany().HasForeignKey(po => po.ProductId);

            //modelBuilder.Entity<ProductsOrders>().Property("Product").IsRequired();
            //modelBuilder.Entity<ProductsOrders>().Property(po => po.Product).IsRequired();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<ProductsOrders> ProductsOrders { get; set; }

    }
}
