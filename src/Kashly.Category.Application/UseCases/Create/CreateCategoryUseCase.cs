using AutoMapper;
using Cms.AspNetCore.JsonLocalizer.Interfaces;
using Kashly.Category.Application.Interfaces;
using Kashly.Category.Communication.Requests;
using Kashly.Category.Domain.Enums;
using Kashly.Category.Domain.Exceptions;
using Kashly.Category.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Kashly.Category.Application.UseCases.Create;

internal class CreateCategoryUseCase(
    IMapper mapper,
    ILocalizer localizer,
    IUserContext userContext,
    ILogger<CreateCategoryUseCase> logger,
    IReadCategoryRepository readRepository,
    IWriteCategoryRepository writeRepository) : ICreateCategoryUseCase
{
    private readonly IMapper mapper = mapper;
    private readonly ILocalizer localizer = localizer;
    private readonly IUserContext userContext = userContext;
    private readonly ILogger<CreateCategoryUseCase> logger = logger;
    private readonly IReadCategoryRepository readRepository = readRepository;
    private readonly IWriteCategoryRepository writeRepository = writeRepository;

    public async Task<int> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var userId = this.userContext.UserId;

        var categoryExists = await this.readRepository
            .ExistsAsync((CategoryType)request.Type, request.Description, userId, cancellationToken);

        if (categoryExists)
        {
            this.logger.LogWarning("Category creation failed. Conflict: Description '{Description}' already exists for UserId {UserId}.",
                request.Description, userId);
            throw new ConflictException(this.localizer.GetString("error.conflict").Value);
        }

        Domain.Entities.Category category = this.mapper.Map<Domain.Entities.Category>(request);
        category.AssignUser(userId);

        await this.writeRepository.SaveAsync(category, cancellationToken);
        this.logger.LogInformation("Category created successfully. Id: {Id}, UserId: {UserId}.",
            category.Id, userId);

        return category.Id;
    }
}
