using Microsoft.EntityFrameworkCore;

namespace Kashly.Category.Infrastructure.Data;

internal class WriteDbContext(DbContextOptions<WriteDbContext> options) : DbContext(options)
{
    public DbSet<Domain.Entities.Category> Categories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WriteDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
