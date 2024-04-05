using System.Security.Claims;
using Application.DTO.Common.Image.DTO;
using Application.Features.Common_Features.Image.Requests.Commands;
using Application.Features.Common_Features.Image.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<ImageResponseDTO>>> FetchAllImagesByUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _mediator.Send(new GetAllImageByUserIdRequest { UserId = userId! });

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ImageResponseDTO>> FetchImageById(string id)
        {
            var result = await _mediator.Send(new GetImageByIdRequest { Id = id });
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<ImageResponseDTO>> DeleteImageById(string id)
        {
            var result = await _mediator.Send(new DeleteImageRequest { Id = id });
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ImageResponseDTO>> UploadImage([FromBody] ImageUploadDTO dTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = await _mediator.Send(
                new UploadImageRequest { UserId = userId!, Image = dTO }
            );
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
