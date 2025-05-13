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
    IValidatorService validator,
    ILogger<CreateCategoryUseCase> logger,
    IReadCategoryRepository readRepository,
    IWriteCategoryRepository writeRepository) : ICreateCategoryUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IValidatorService _validator = validator;
    private readonly ILocalizer _localizer = localizer;
    private readonly IUserContext _userContext = userContext;
    private readonly ILogger<CreateCategoryUseCase> _logger = logger;
    private readonly IReadCategoryRepository _readRepository = readRepository;
    private readonly IWriteCategoryRepository _writeRepository = writeRepository;

    public async Task<int> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        _validator.Validate(request, new CategoryRequestValidator(_localizer));

        var categoryExists = await _readRepository
            .ExistsAsync((CategoryType)request.Type, request.Description, userId, cancellationToken);

        if (categoryExists)
        {
            _logger.LogWarning(
                "Category creation failed. Conflict: Description '{Description}' with Type '{Type}' already exists for UserId {UserId}.",
                request.Description, request.Type, userId
            );
            throw new ConflictException(_localizer.GetString("error.conflict").Value);
        }

        Domain.Entities.Category category = _mapper.Map<Domain.Entities.Category>(request);
        category.AssignUser(userId);

        await _writeRepository.SaveAsync(category, cancellationToken);
        _logger.LogInformation("Category created successfully. Id: {Id}, UserId: {UserId}.",
            category.Id, userId);

        return category.Id;
    }
}
