using Kashly.Category.Application.UseCases.Create;
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
    /// Retrieves a category by its ID.
    /// </summary>
    /// <param name="id">The ID of the category.</param>
    /// <returns>The category with the specified ID.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetCategoryById(
        [FromServices] IGetByIdCategoryUseCase useCase,
        [FromRoute]int id)
    {
        CategoryResponse category = await useCase.Handle(new
            GetByIdCategoryRequest(id), this.HttpContext.RequestAborted);

        return Ok(category);
    }

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="request">The category data to create.</param>
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
