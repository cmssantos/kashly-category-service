namespace Kashly.Category.Domain.Interfaces;

public interface IWriteCategoryRepository
{
    Task<Entities.Category> SaveAsync(Entities.Category category, CancellationToken cancellationToken);
}
