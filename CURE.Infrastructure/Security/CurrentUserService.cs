using CURE.Application.Interfaces.Security;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CURE.Infrastructure.Security
{
    public class CurrentUserService : ICurrentUserService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        private ClaimsPrincipal User => _httpContextAccessor.HttpContext!.User;
        public long UserId => long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public string? Email => User.FindFirstValue(ClaimTypes.Email);
        public bool IsAdmin => User.IsInRole("Admin");

        public bool IsNurse => User.IsInRole("Nurse");

        public bool IsPatient => User.IsInRole("Patient");
    }
}