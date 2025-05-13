using Kashly.Category.Domain.Interfaces;

namespace Kashly.Category.Infrastructure.Data.Repositories;

internal class WriteCategoryRepository(WriteDbContext dbContext) : IWriteCategoryRepository
{
    private readonly WriteDbContext _dbContext = dbContext;

    public async Task<Domain.Entities.Category> SaveAsync(Domain.Entities.Category category,
        CancellationToken cancellationToken)
    {
        if (category.Id == 0)
        {
            await _dbContext.Set<Domain.Entities.Category>()
                .AddAsync(category, cancellationToken);
        }
        else
        {
            _dbContext.Set<Domain.Entities.Category>()
                .Update(category);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return category;
    }
}
