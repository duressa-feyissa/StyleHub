using Application.Contracts.Persistance.Repositories;
using Application.DTO.Common.Image.DTO;
using Application.Exceptions;
using Application.Features.Common_Features.Image.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;
using IImageRepository = Application.Contracts.Infrastructure.Repositories.IImageRepository;

namespace Application.Features.Common_Features.Image.Handlers.Commads
{
    public class DeleteImageHandler
        : IRequestHandler<DeleteImageRequest, BaseResponse<ImageResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;

        public DeleteImageHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IImageRepository imageRepository
        )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _imageRepository = imageRepository;
        }

        public async Task<BaseResponse<ImageResponseDTO>> Handle(
            DeleteImageRequest request,
            CancellationToken cancellationToken
        )
        {
            await _imageRepository.Delete(request.Id);
            var image = await _unitOfWork.ImageRepository.GetById(request.Id);
            if (image == null)
            {
                throw new NotFoundException("Image not found");
            }

            await _unitOfWork.ImageRepository.Delete(image);
            return new BaseResponse<ImageResponseDTO>
            {
                Message = "Image Deleted Successfully",
                Success = true,
                Data = _mapper.Map<ImageResponseDTO>(image)
            };
        }
    }
}
