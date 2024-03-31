using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.Entities.Common;

namespace Domain.Entities.User
{
    public class User : BaseEntity
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required string PhoneNumber { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string latitude { get; set; }

        [Required]
        public required string longitude { get; set; }
        public string? ProfilePicture { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public string? productTargetPreferences { get; set; }
        public string? productCategoryPreferences { get; set; }
        public string? productBrandPreferences { get; set; }
        public string? productColorPreferences { get; set; }
        public bool IsPreferencesSet { get; set; } = false;
        public bool IsEmailVerified { get; set; } = false;
        public bool IsVerified { get; set; } = false;
        public bool IsProfileCompleted { get; set; } = false;
        public string? VerificationCode { get; set; }
        public string? VerificationCodeExpiration { get; set; }
        public string? ResetPasswordCode { get; set; }
        public string? ResetPasswordCodeExpiration { get; set; }
        public string? EmailVerificationCode { get; set; }
        public string? EmailVerificationCodeExpiration { get; set; }
        public string? RefreshToken { get; set; }
        public string? AccessToken { get; set; }
        public string? Token { get; set; }
        public required Role Role { get; set; }
    }
}
