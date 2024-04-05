using Application.DTO.Product.CategoryDTO.DTO;
using Application.Features.Product_Features.Category.Requests.Commands;
using Application.Features.Product_Features.Category.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryResponseDTO>>> fetchAllCategorys()
        {
            var result = await _mediator.Send(new GetAllCategory());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CategoryResponseDTO>> fetchCategoryById(string id)
        {
            var result = await _mediator.Send(new GetCategoryById { Id = id });
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CategoryResponseDTO>> CreateCategory(
            [FromBody] CreateCategoryRequest command
        )
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CategoryResponseDTO>> UpdateCategoryRequest(
            [FromBody] UpdateCategoryRequest command
        )
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<CategoryResponseDTO>> DeleteCategoryRequest(string id)
        {
            var result = await _mediator.Send(new DeleteCategoryRequest { Id = id });
            return Ok(result);
        }
    }
}
