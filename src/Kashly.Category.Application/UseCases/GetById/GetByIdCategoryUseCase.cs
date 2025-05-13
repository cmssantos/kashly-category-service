using AutoMapper;
using Cms.AspNetCore.JsonLocalizer.Interfaces;
using Kashly.Category.Application.Interfaces;
using Kashly.Category.Communication.Requests;
using Kashly.Category.Communication.Responses;
using Kashly.Category.Domain.Exceptions;
using Kashly.Category.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Kashly.Category.Application.UseCases.GetById;

public class GetByIdCategoryUseCase(
    IMapper mapper,
    ILocalizer localizer,
    IUserContext userContext,
    IReadCategoryRepository readRepository,
    ILogger<GetByIdCategoryUseCase> logger) : IGetByIdCategoryUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly ILocalizer _localizer = localizer;
    private readonly IUserContext _userContext = userContext;
    private readonly ILogger<GetByIdCategoryUseCase> _logger = logger;
    private readonly IReadCategoryRepository _readRepository = readRepository;

    public async Task<CategoryResponse> Handle(GetByIdCategoryRequest request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;

        Domain.Entities.Category? category = await _readRepository.GetByIdAsync(request.Id, userId, cancellationToken);
        if (category is null)
        {
            _logger.LogWarning("Category not found. Id: {Id}, UserId: {UserId}.", request.Id, userId);
            throw new NotFoundException(_localizer.GetString("error.notFound").Value);
        }

        _logger.LogInformation("Category retrieved successfully. Id: {Id}, UserId: {UserId}.", request.Id, userId);
        return _mapper.Map<CategoryResponse>(category);
    }
}
