using System.ComponentModel.DataAnnotations;
using System.Text;
using Application.Common;
using Application.Contracts.Infrastructure.Repositories;
using Application.Contracts.Persistance.Repositories;
using Application.DTO.User.UserDTO.DTO;
using Application.Exceptions;
using Application.Features.User_Features.User.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;

namespace Application.Features.User_Features.User.Handlers.Command
{
    public class UpdateUserProfileHandler
        : IRequestHandler<UpdateUserProfileRequest, BaseResponse<UserResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ApiSettings _apiSettings;
        private readonly IImageRepository _imageRepository;

        public UpdateUserProfileHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOptions<ApiSettings> apiSettings,
            IImageRepository imageRepository
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _apiSettings = apiSettings.Value;
            _imageRepository = imageRepository;
        }

        public async Task<BaseResponse<UserResponseDTO>> Handle(
            UpdateUserProfileRequest request,
            CancellationToken cancellationToken
        )
        {
            var user = await _unitOfWork.UserRepository.GetById(request.Id);
            if (user == null)
                throw new NotFoundException("User not found");

            if (request.updateUserProfileDTO.FirstName != null)
            {
                if (request.updateUserProfileDTO.FirstName.Length < 3)
                    throw new ValidationException("First name must be at least 3 characters long");

                user.FirstName = request.updateUserProfileDTO.FirstName;
            }

            if (request.updateUserProfileDTO.LastName != null)
            {
                if (request.updateUserProfileDTO.LastName.Length < 3)
                    throw new ValidationException("Last name must be at least 3 characters long");

                user.LastName = request.updateUserProfileDTO.LastName;
            }

            if (request.updateUserProfileDTO.Latitude != null)
            {
                if (
                    request.updateUserProfileDTO.Latitude < -90
                    || request.updateUserProfileDTO.Latitude > 90
                )
                    throw new ValidationException("Latitude must be between -90 and 90");
                user.Latitude = request.updateUserProfileDTO.Latitude;
            }

            if (request.updateUserProfileDTO.Longitude != null)
            {
                if (
                    request.updateUserProfileDTO.Longitude < -180
                    || request.updateUserProfileDTO.Longitude > 180
                )
                    throw new ValidationException("Longitude must be between -180 and 180");
                user.Longitude = request.updateUserProfileDTO.Longitude;
            }

            if (
                request.updateUserProfileDTO.Country != null
                && request.updateUserProfileDTO.Country.Length > 0
            )
                user.Country = request.updateUserProfileDTO.Country;

            if (
                request.updateUserProfileDTO.City != null
                && request.updateUserProfileDTO.City.Length > 0
            )
                user.City = request.updateUserProfileDTO.City;

            if (
                request.updateUserProfileDTO.Address != null
                && request.updateUserProfileDTO.Address.Length > 0
            )
                user.Address = request.updateUserProfileDTO.Address;

            if (request.updateUserProfileDTO.Password != null)
            {
                if (
                    request.updateUserProfileDTO.Password.Length < 6
                    || request.updateUserProfileDTO.Password.Length > 20
                )
                    throw new ValidationException(
                        "Password must be at least 6 characters long and at most 20 characters long"
                    );

                if (HashPassword(request.updateUserProfileDTO.Password) == user.Password)
                    throw new ValidationException(
                        "New password must be different from the old password"
                    );

                user.Password = HashPassword(request.updateUserProfileDTO.Password);
            }

            if (request.updateUserProfileDTO.ProfilePictureBase64 != null)
            {
                if (request.updateUserProfileDTO.ProfilePictureBase64.Length < 100)
                    throw new ValidationException("Profile picture must not be empty");

                if (user.ProfilePicture != null)
                    user.ProfilePicture = await _imageRepository.Update(
                        request.updateUserProfileDTO.ProfilePictureBase64,
                        user.Id
                    );
                else
                    user.ProfilePicture = await _imageRepository.Upload(
                        request.updateUserProfileDTO.ProfilePictureBase64,
                        user.Id
                    );
            }

            return new BaseResponse<UserResponseDTO>
            {
                Data = _mapper.Map<UserResponseDTO>(user),
                Message = "User deleted successfully",
                Success = true
            };
        }

        private string HashPassword(string password)
        {
            var saltBytes = Encoding.UTF8.GetBytes(_apiSettings.SecretKey ?? "SecretKey");

            string hashedPassword = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: saltBytes,
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8
                )
            );
            return hashedPassword;
        }
    }
}
