using Application.Contracts.Infrastructure.Repositories;
using Application.Exceptions;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Infrastructure.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly Cloudinary _cloudinary;

        public ImageRepository(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<bool> Delete(string publicId)
        {
            try
            {
                var deletionParams = new DeletionParams(publicId);

                var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

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

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

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

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

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
