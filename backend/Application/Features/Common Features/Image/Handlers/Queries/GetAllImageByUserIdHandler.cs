using Application.Contracts.Persistance.Repositories;
using Application.DTO.Common.Image.DTO;
using Application.Exceptions;
using Application.Features.Common_Features.Image.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Common_Features.Image.Handlers.Queries
{
    public class GetAllImageByUserIdHandler
        : IRequestHandler<GetAllImageByUserIdRequest, List<ImageResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllImageByUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ImageResponseDTO>> Handle(
            GetAllImageByUserIdRequest request,
            CancellationToken cancellationToken
        )
        {
            var images = await _unitOfWork.ImageRepository.GetAll(request.UserId);
            if (images == null)
            {
                throw new NotFoundException("No Images found");
            }

            var imageResponse = _mapper.Map<List<ImageResponseDTO>>(images);
            return imageResponse;
        }
    }
}
