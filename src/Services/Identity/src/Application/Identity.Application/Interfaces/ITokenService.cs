using Identity.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Identity.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user, IList<string> roles);
    }
}