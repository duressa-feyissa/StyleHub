using System.Security.Claims;
using backend.Application.Contracts.Infrastructure.Services;

namespace backend.Infrastructure.Repository
{
    public class CurrentLoggedInService(IHttpContextAccessor httpContextAccessor) : ICurrentLoggedInService
    {
        public string GetCurrentLoggedInEmail()
        {
            var email = httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
            return email ?? string.Empty;
        }

        public Guid GetCurrentLoggedInId()
        {
            var userId = httpContextAccessor?.HttpContext?.User.FindFirstValue(
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
            var phoneNumber = httpContextAccessor?.HttpContext?.User.FindFirstValue(
                ClaimTypes.MobilePhone
            );
            return phoneNumber ?? string.Empty;
        }

        public int GetCurrentLoggedInPriority()
        {
            var priorityClaim = httpContextAccessor?.HttpContext?.User.FindFirst(
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
            var role = httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            return role ?? string.Empty;
        }

        public string GetCurrentLoggedInUsername()
        {
            var username = httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
            return username ?? string.Empty;
        }
    }
}
