using Kashly.Category.Application.UseCases.Create;
using Kashly.Category.Application.UseCases.CreateDefault;
using Kashly.Category.Application.UseCases.GetById;
using Kashly.Category.Communication.Requests;
using Kashly.Category.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Kashly.Category.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    /// <summary>
    /// Initializes the user categories by creating default categories.
    /// This endpoint is intended for use when the user has no categories.
    /// </summary>
    /// <param name="useCase">The use case for creating default categories.</param>
    /// <remarks>
    /// This endpoint is used to initialize the user categories.
    /// It creates default categories for the user.
    /// The request body should be empty.
    /// </remarks>
    /// <returns>No content.</returns>
    [HttpPost("init")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> InitializeUserCategories(
        [FromServices] ICreateDefaultCategoriesUseCase useCase)
    {
        await useCase.Handle(this.HttpContext.RequestAborted);
        return Ok();
    }

    /// <summary>
    /// Retrieves a category by its ID.
    /// </summary>
    /// <param name="useCase">The use case for retrieving a category by ID.</param>
    /// <param name="id">The ID of the category.</param>
    /// <remarks>
    /// This endpoint is used to retrieve a category by its ID.
    /// The ID should be provided in the URL.
    /// </remarks>
    /// <returns>The category with the specified ID.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCategoryById(
        [FromServices] IGetByIdCategoryUseCase useCase,
        [FromRoute] int id)
    {
        CategoryResponse category = await useCase.Handle(new
            GetByIdCategoryRequest(id), this.HttpContext.RequestAborted);

        return Ok(category);
    }

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="useCase">The use case for creating a category.</param>
    /// <param name="request">The category data to create.</param>
    /// <remarks>
    /// This endpoint is used to create a new category.
    /// The request body should contain the category data.
    /// </remarks>
    /// <returns>The created category.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(IdResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateCategory(
        [FromServices] ICreateCategoryUseCase useCase,
        [FromBody] CreateCategoryRequest request)
    {
        var createdCategoryId = await useCase.Handle(request, this.HttpContext.RequestAborted);

        return CreatedAtAction(nameof(GetCategoryById), new
        {
            id = createdCategoryId
        }, new { id = createdCategoryId });
    }
}
