using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Common.Image.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Common_Features.Image.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Common_Features.Image.Handlers.Queries
{
    public class GetImageByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetImageByIdRequest, ImageResponseDTO>
    {
        public async Task<ImageResponseDTO> Handle(
            GetImageByIdRequest request,
            CancellationToken cancellationToken
        )
        {
            var image = await unitOfWork.ImageRepository.GetById(request.Id);
            if (image == null)
            {
                throw new NotFoundException("Image not found");
            }

            var imageResponse = mapper.Map<ImageResponseDTO>(image);
            return imageResponse;
        }
    }
}
