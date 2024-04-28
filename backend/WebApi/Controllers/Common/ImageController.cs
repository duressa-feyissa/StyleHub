using System.Security.Claims;
using backend.Application.DTO.Common.Image.DTO;
using backend.Application.Features.Common_Features.Image.Requests.Commands;
using backend.Application.Features.Common_Features.Image.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.WebApi.Controllers.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<ImageResponseDTO>>> FetchAllImagesByUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await mediator.Send(new GetAllImageByUserIdRequest { UserId = userId! });

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ImageResponseDTO>> FetchImageById(string id)
        {
            var result = await mediator.Send(new GetImageByIdRequest { Id = id });
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<ImageResponseDTO>> DeleteImageById(string id)
        {
            var result = await mediator.Send(new DeleteImageRequest { Id = id });
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ImageResponseDTO>> UploadImage([FromBody] ImageUploadDTO dTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await mediator.Send(
                new UploadImageRequest { UserId = userId!, Image = dTO }
            );

            return Ok(result);
        }
    }
}
