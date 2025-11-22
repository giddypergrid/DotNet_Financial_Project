using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace backend.Extensions
{
    public static class ClaimExtension
    {
        public static string getClaimUserId(this ClaimsPrincipal user){
            return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }
        
    }
}
