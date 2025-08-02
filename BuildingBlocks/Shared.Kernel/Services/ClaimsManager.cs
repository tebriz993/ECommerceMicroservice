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

        // Köməkçi bir metod yaradırıq ki, kod təkrarçılığı olmasın.
        private string? GetClaimValue(string claimType)
        {
            // FindFirst metodu Claim obyektini qaytarır.
            var claim = GetUser()?.FindFirst(claimType);
            // Əgər claim tapılarsa, onun Value-sunu, tapılmazsa null qaytarırıq.
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
            // IsInRole metodu onsuz da mövcuddur və düzgün işləyir.
            return GetUser()?.IsInRole(role) ?? false;
        }

        public IEnumerable<string> GetUserRoles()
        {
            // FindAll metodu da mövcuddur.
            return GetUser()?.FindAll(ClaimTypes.Role).Select(c => c.Value) ?? Enumerable.Empty<string>();
        }
    }
}