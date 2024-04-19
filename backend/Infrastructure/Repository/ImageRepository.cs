using backend.Application.Contracts.Infrastructure.Repositories;
using backend.Application.Exceptions;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace backend.Infrastructure.Repository
{
    public class ImageRepository(Cloudinary cloudinary) : IImageRepository
    {
        public async Task<bool> Delete(string publicId)
        {
            try
            {
                var deletionParams = new DeletionParams(publicId);

                var deletionResult = await cloudinary.DestroyAsync(deletionParams);

                if (deletionResult != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> Update(
            string base64Image,
            string publicId,
            bool backgroundRemoval = false
        )
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(@$"{base64Image}"),
                PublicId = publicId,
                BackgroundRemoval = backgroundRemoval ? "cloudinary_ai" : null,
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            if (uploadResult != null)
            {
                if (uploadResult.SecureUrl != null)
                {
                    return uploadResult.SecureUrl.ToString();
                }

                if (uploadResult.Error != null)
                {
                    throw new BadRequestException(uploadResult.Error.Message);
                }

                throw new BadRequestException("Upload result is null");
            }
            else
            {
                throw new BadRequestException("Upload result is null");
            }
        }

        public async Task<string> Upload(
            string base64Image,
            string publicId,
            bool backgroundRemoval = false
        )
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(@$"{base64Image}"),
                PublicId = publicId,
                BackgroundRemoval = backgroundRemoval ? "cloudinary_ai" : null,
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);

            if (uploadResult != null)
            {
                if (uploadResult.SecureUrl != null)
                {
                    return uploadResult.SecureUrl.ToString();
                }

                if (uploadResult.Error != null)
                {
                    throw new BadRequestException(uploadResult.Error.Message);
                }

                throw new BadRequestException("Upload result is null");
            }
            else
            {
                throw new BadRequestException("Upload result is null");
            }
        }
    }
}
