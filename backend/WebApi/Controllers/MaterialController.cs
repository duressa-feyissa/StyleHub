using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.DTO.MaterialDTO.DTO;
using Application.Features.Material.Requests.Commands;
using Application.Features.Material.Requests.Queries;

namespace WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MaterialController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<List<MaterialResponseDTO>>> fetchAllMaterials()
        {
            var result = await _mediator.Send(new GetAllMaterial());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialResponseDTO>> fetchMaterialById(string id)
        {
            var result = await _mediator.Send(new GetMaterialById { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<MaterialResponseDTO>> CreateMaterial([FromBody] CreateMaterialRequest command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<MaterialResponseDTO>> UpdateMaterialRequest([FromBody] UpdateMaterialRequest command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<MaterialResponseDTO>> DeleteMaterialRequest(string id)
        {
            var result = await _mediator.Send(new DeleteMaterialRequest { Id = id });
            return Ok(result);
        }
    }
}