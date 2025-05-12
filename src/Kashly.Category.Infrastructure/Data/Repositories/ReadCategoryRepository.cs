using Kashly.Category.Domain.Enums;
using Kashly.Category.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kashly.Category.Infrastructure.Data.Repositories;

internal class ReadCategoryRepository(ReadDbContext dbContext) : IReadCategoryRepository
{
    private readonly ReadDbContext dbContext = dbContext;

    public async Task<bool> AnyAsync(string userId, CancellationToken cancellationToken)
        => await this.dbContext.Set<Domain.Entities.Category>()
            .AsNoTracking()
            .AnyAsync(c => c.UserId == userId, cancellationToken);

    public async Task<bool> ExistsAsync(CategoryType type, string description, string userId,
        CancellationToken cancellationToken)
        => await this.dbContext.Set<Domain.Entities.Category>()
            .AsNoTracking()
            .AnyAsync(c => c.Type == type &&
                           c.Description == description &&
                           c.UserId == userId, cancellationToken);

    public async Task<Domain.Entities.Category?> GetByIdAsync(int id, string userId, CancellationToken cancellationToken)
        => await this.dbContext.Set<Domain.Entities.Category>()
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId, cancellationToken);
}
