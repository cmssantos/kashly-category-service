using Microsoft.EntityFrameworkCore;

namespace Kashly.Category.Infrastructure.Data;

internal class ReadDbContext(DbContextOptions<ReadDbContext> options) : DbContext(options)
{
    public DbSet<Domain.Entities.Category> Categories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => base.OnModelCreating(modelBuilder);
}
