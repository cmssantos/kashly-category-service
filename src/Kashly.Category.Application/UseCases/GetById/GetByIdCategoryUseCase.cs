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
    private readonly IMapper mapper = mapper;
    private readonly ILocalizer localizer = localizer;
    private readonly IUserContext userContext = userContext;
    private readonly ILogger<GetByIdCategoryUseCase> logger = logger;
    private readonly IReadCategoryRepository readRepository = readRepository;

    public async Task<CategoryResponse> Handle(GetByIdCategoryRequest request, CancellationToken cancellationToken)
    {
        var userId = this.userContext.UserId;

        Domain.Entities.Category? category = await this.readRepository.GetByIdAsync(request.Id, userId, cancellationToken);
        if (category is null)
        {
            this.logger.LogWarning("Category not found. Id: {Id}, UserId: {UserId}.", request.Id, userId);
            throw new NotFoundException(this.localizer.GetString("error.notFound").Value);
        }

        this.logger.LogInformation("Category retrieved successfully. Id: {Id}, UserId: {UserId}.", request.Id, userId);

        return this.mapper.Map<CategoryResponse>(category);
    }
}
