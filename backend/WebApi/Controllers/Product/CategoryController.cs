using backend.Application.DTO.Product.CategoryDTO.DTO;
using backend.Application.Features.Product_Features.Category.Requests.Commands;
using backend.Application.Features.Product_Features.Category.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.WebApi.Controllers.Product
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<CategoryResponseDTO>>> fetchAllCategorys()
        {
            var result = await mediator.Send(new GetAllCategory());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CategoryResponseDTO>> fetchCategoryById(string id)
        {
            var result = await mediator.Send(new GetCategoryById { Id = id });
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CategoryResponseDTO>> CreateCategory(
            [FromBody] CreateCategoryRequest command
        )
        {
            var result = await mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CategoryResponseDTO>> UpdateCategoryRequest(
            [FromBody] UpdateCategoryRequest command
        )
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CategoryResponseDTO>> DeleteCategoryRequest(string id)
        {
            var result = await mediator.Send(new DeleteCategoryRequest { Id = id });
            return Ok(result);
        }
    }
}
