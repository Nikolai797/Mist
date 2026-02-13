using GameStore.Data.Entities;
using GameStore.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Game> Games { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        
        builder.Entity<OrderItem>()
            .HasKey(oi => new { oi.OrderId, oi.GameId });

        builder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);

        builder.Entity<OrderItem>()
            .HasOne(oi => oi.Game)
            .WithMany(g => g.OrderItems)
            .HasForeignKey(oi => oi.GameId);

        builder.Entity<Game>()
            .HasOne(g => g.Category)
            .WithMany(c => c.Games)
            .HasForeignKey(g => g.CategoryId);

        builder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);
    }

}
