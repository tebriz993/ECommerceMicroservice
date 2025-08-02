using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Shared.Kernel.Services
{
    public class ClaimsManager : IClaimsManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimsManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        private ClaimsPrincipal? GetUser() => _httpContextAccessor.HttpContext?.User;
        private string? GetClaimValue(string claimType)
        {
            var claim = GetUser()?.FindFirst(claimType);
            return claim?.Value;
        }

        public Guid GetUserId()
        {
            var userIdClaim = GetClaimValue(ClaimTypes.NameIdentifier);
            return !string.IsNullOrEmpty(userIdClaim) ? Guid.Parse(userIdClaim) : Guid.Empty;
        }

        public string? GetUserEmail()
        {
            return GetClaimValue(ClaimTypes.Email);
        }

        public string? GetUserName()
        {
            return GetClaimValue(ClaimTypes.Name);
        }

        public bool IsInRole(string role)
        {
            return GetUser()?.IsInRole(role) ?? false;
        }

        public IEnumerable<string> GetUserRoles()
        {
            return GetUser()?.FindAll(ClaimTypes.Role).Select(c => c.Value) ?? Enumerable.Empty<string>();
        }
    }
}