using Microsoft.EntityFrameworkCore;
using test_backend.Models;

namespace test_backend.Data;

public class TestBackendContext : DbContext
{
    public TestBackendContext(DbContextOptions<TestBackendContext> opts) : base(opts)
    {
    }
    public DbSet<User> Users {get; set;}
    public DbSet<Order> Orders {get; set;}
    public DbSet<Product> Products {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Order>().HasQueryFilter(o => o.DeletedAt == null);
    }

}