using System.Security.Claims;
using Application.Contracts.Infrastructure.Services;

namespace Infrastructure.Repository
{
    public class CurrentLoggedInService : ICurrentLoggedInService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentLoggedInService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentLoggedInEmail()
        {
            var email = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
            return email ?? string.Empty;
        }

        public Guid GetCurrentLoggedInId()
        {
            var userId = _httpContextAccessor?.HttpContext?.User.FindFirstValue(
                ClaimTypes.NameIdentifier
            );
            if (Guid.TryParse(userId, out var id))
            {
                return id;
            }
            throw new InvalidOperationException("User ID not found in claims.");
        }

        public string GetCurrentLoggedInPhoneNumber()
        {
            var phoneNumber = _httpContextAccessor?.HttpContext?.User.FindFirstValue(
                ClaimTypes.MobilePhone
            );
            return phoneNumber ?? string.Empty;
        }

        public int GetCurrentLoggedInPriority()
        {
            var priorityClaim = _httpContextAccessor?.HttpContext?.User.FindFirst(
                "http://schemas.example.com/claims/priority"
            );
            if (priorityClaim != null && int.TryParse(priorityClaim.Value, out var priority))
            {
                return priority;
            }
            throw new InvalidOperationException("Priority not found in claims.");
        }

        public string GetCurrentLoggedInRole()
        {
            var role = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            return role ?? string.Empty;
        }

        public string GetCurrentLoggedInUsername()
        {
            var username = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
            return username ?? string.Empty;
        }
    }
}
