using Application.Contracts.Persistance.Repositories;
using Application.DTO.Common.Image.DTO;
using Application.Exceptions;
using Application.Features.Common_Features.Image.Requests.Queries;
using AutoMapper;
using MediatR;

namespace Application.Features.Common_Features.Image.Handlers.Queries
{
    public class GetImageByIdHandler : IRequestHandler<GetImageByIdRequest, ImageResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetImageByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ImageResponseDTO> Handle(
            GetImageByIdRequest request,
            CancellationToken cancellationToken
        )
        {
            var image = await _unitOfWork.ImageRepository.GetById(request.Id);
            if (image == null)
            {
                throw new NotFoundException("Image not found");
            }

            var imageResponse = _mapper.Map<ImageResponseDTO>(image);
            return imageResponse;
        }
    }
}
