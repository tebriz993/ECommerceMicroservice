using System;
using System.Collections.Generic;

namespace Shared.Kernel.Services
{
    public interface IClaimsManager
    {
        Guid GetUserId();
        string? GetUserEmail();
        string? GetUserName();
        bool IsInRole(string role);
        IEnumerable<string> GetUserRoles();
    }
}