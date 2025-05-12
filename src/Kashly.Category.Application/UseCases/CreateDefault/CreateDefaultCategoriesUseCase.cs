
using System.Text.Json;
using Kashly.Category.Application.Interfaces;
using Kashly.Category.Domain.Interfaces;

namespace Kashly.Category.Application.UseCases.CreateDefault;

public class CreateDefaultCategoriesUseCase(
    IUserContext userContext,
    IReadCategoryRepository readRepository,
    IWriteCategoryRepository writeRepository) : ICreateDefaultCategoriesUseCase
{
    private readonly IUserContext userContext = userContext;
    private readonly IReadCategoryRepository readRepository = readRepository;
    private readonly IWriteCategoryRepository writeRepository = writeRepository;

    public async Task Handle(CancellationToken cancellationToken)
    {
        var userId = this.userContext.UserId;

        var hasCategories = await this.readRepository.AnyAsync(userId, cancellationToken);
        if (!hasCategories)
        {
            // Load categories from JSON
            List<Domain.Entities.Category> defaultCategories = LoadDefaultCategories(userId);

            foreach (Domain.Entities.Category category in defaultCategories)
            {
                await this.writeRepository.SaveAsync(category, cancellationToken);
            }
        }
    }

    private static List<Domain.Entities.Category> LoadDefaultCategories(string userId)
    {
        var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Data");
        var jsonData = File.ReadAllText(jsonPath);

        List<Domain.Entities.Category>? categories = JsonSerializer.Deserialize<List<Domain.Entities.Category>>(jsonData)
            ?? throw new InvalidOperationException("Failed to load default categories.");

        return [.. categories.Select(category => new Domain.Entities.Category(
            category.Type,
            category.Description,
            category.Icon,
            category.Color,
            userId
        ))];
    }
}
