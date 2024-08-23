using Microsoft.EntityFrameworkCore;
using test_backend.Models;

namespace test_backend.Data;

public class TestBackendContext : DbContext
{
    public TestBackendContext(DbContextOptions<TestBackendContext> opts) : base(opts)
    {
    }
    public DbSet<User> Users {get; set;}
}