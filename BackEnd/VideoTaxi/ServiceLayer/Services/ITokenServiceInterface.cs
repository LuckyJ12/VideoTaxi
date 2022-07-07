using DataLayer.Models;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public interface ITokenServiceInterface
    {
        Task<JwtSecurityToken> GetTokenAsync(AppUser user);

        SigningCredentials GetSigningCredentials();
        Task<List<Claim>> GetClaimsAsync(AppUser user);
    }
}
