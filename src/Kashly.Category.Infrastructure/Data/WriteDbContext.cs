using Kashly.Category.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Kashly.Category.Infrastructure.Data;

internal class WriteDbContext(DbContextOptions<WriteDbContext> options, IOptions<DatabaseSettings> config)
    : DbContext(options)
{
    private readonly string _schema = config.Value.Schema;

    public DbSet<Domain.Entities.Category> Categories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(_schema);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WriteDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
