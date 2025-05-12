namespace Kashly.Category.Domain.Interfaces;

public interface IReadCategoryRepository
{
    Task<bool> AnyAsync(string userId, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Enums.CategoryType type, string description, string userId, CancellationToken cancellationToken);
    Task<Entities.Category?> GetByIdAsync(int id, string userId, CancellationToken cancellationToken);
}
