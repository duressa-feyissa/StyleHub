using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Common.Image.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Common_Features.Image.Requests.Commands;
using backend.Application.Response;
using MediatR;
using IImageRepository = backend.Application.Contracts.Infrastructure.Repositories.IImageRepository;

namespace backend.Application.Features.Common_Features.Image.Handlers.Commads
{
    public class DeleteImageHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IImageRepository imageRepository)
        : IRequestHandler<DeleteImageRequest, BaseResponse<ImageResponseDTO>>
    {
        public async Task<BaseResponse<ImageResponseDTO>> Handle(
            DeleteImageRequest request,
            CancellationToken cancellationToken
        )
        {
            await imageRepository.Delete(request.Id);
            var image = await unitOfWork.ImageRepository.GetById(request.Id);
            if (image == null)
            {
                throw new NotFoundException("Image not found");
            }

            await unitOfWork.ImageRepository.Delete(image);
            return new BaseResponse<ImageResponseDTO>
            {
                Message = "Image Deleted Successfully",
                Success = true,
                Data = mapper.Map<ImageResponseDTO>(image)
            };
        }
    }
}
