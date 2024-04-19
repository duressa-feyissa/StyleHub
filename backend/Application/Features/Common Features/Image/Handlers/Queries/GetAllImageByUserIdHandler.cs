using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Common.Image.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Common_Features.Image.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Common_Features.Image.Handlers.Queries
{
    public class GetAllImageByUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<GetAllImageByUserIdRequest, List<ImageResponseDTO>>
    {
        public async Task<List<ImageResponseDTO>> Handle(
            GetAllImageByUserIdRequest request,
            CancellationToken cancellationToken
        )
        {
            var images = await unitOfWork.ImageRepository.GetAll(request.UserId);
            if (images == null)
            {
                throw new NotFoundException("No Images found");
            }

            var imageResponse = mapper.Map<List<ImageResponseDTO>>(images);
            return imageResponse;
        }
    }
}
