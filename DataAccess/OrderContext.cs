using Microsoft.EntityFrameworkCore;
using OrderService.Models.Entities;

namespace OrderService.DataAccess
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options): base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(i => i.Order)
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
