using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;

namespace MyRecepiBook.Infrastructure.DataAccess;

public class MyRecepiBookDbContext : DbContext
{
    public MyRecepiBookDbContext(DbContextOptions options) : base(options) { }
    
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyRecepiBookDbContext).Assembly);
    }
}