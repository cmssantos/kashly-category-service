using Kashly.Category.Domain.Interfaces;

namespace Kashly.Category.Infrastructure.Data.Repositories;

internal class WriteCategoryRepository(WriteDbContext dbContext) : IWriteCategoryRepository
{
    private readonly WriteDbContext dbContext = dbContext;

    public async Task<Domain.Entities.Category> SaveAsync(Domain.Entities.Category category,
        CancellationToken cancellationToken)
    {
        if (category.Id == 0)
        {
            await this.dbContext.Set<Domain.Entities.Category>()
                .AddAsync(category, cancellationToken);
        }
        else
        {
            this.dbContext.Set<Domain.Entities.Category>()
                .Update(category);
        }

        await this.dbContext.SaveChangesAsync(cancellationToken);

        return category;
    }
}
