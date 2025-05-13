using Cms.AspNetCore.JsonLocalizer.Interfaces;
using Kashly.Category.Application.Interfaces;
using Kashly.Category.Communication.Requests;
using Kashly.Category.Domain.Exceptions;
using Kashly.Category.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Kashly.Category.Application.UseCases.Archive;

public class ArchiveCategoryUseCase(
    ILocalizer localizer,
    IUserContext userContext,
    ILogger<ArchiveCategoryUseCase> logger,
    IReadCategoryRepository readRepository,
    IWriteCategoryRepository writeRepository) : IArchiveCategoryUseCase
{
    private readonly ILocalizer _localizer = localizer;
    private readonly IUserContext _userContext = userContext;
    private readonly ILogger<ArchiveCategoryUseCase> _logger = logger;
    private readonly IReadCategoryRepository _readRepository = readRepository;
    private readonly IWriteCategoryRepository _writeRepository = writeRepository;

    public async Task Handle(ArchiveCategoryRequest request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;

        Domain.Entities.Category? category = await _readRepository.GetByIdAsync(request.Id, userId, cancellationToken);
        if (category is null)
        {
            _logger.LogWarning("Category not found. Id: {Id}, UserId: {UserId}.", request.Id, userId);
            throw new NotFoundException(_localizer.GetString("error.notFound").Value);
        }

        category.Delete();
        await _writeRepository.SaveAsync(category, cancellationToken);

        _logger.LogInformation("Category archived successfully. Id: {Id}, UserId: {UserId}.", request.Id, userId);
    }
}
