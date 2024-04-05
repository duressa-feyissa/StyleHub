using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.Entities.Common;

namespace Domain.Entities.User
{
    public class User : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
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
        public bool IsPhoneNumberVerified { get; set; } = false;
        public bool IsProfileCompleted { get; set; } = false;
        public string? PhoneNumberVerificationCode { get; set; }
        public DateTime? PhoneNumberVerificationCodeExpiration { get; set; }
        public string? ResetPasswordCode { get; set; }
        public DateTime? ResetPasswordCodeExpiration { get; set; }
        public string? EmailVerificationCode { get; set; }
        public DateTime? EmailVerificationCodeExpiration { get; set; }
        public required Role Role { get; set; }
    }
}
