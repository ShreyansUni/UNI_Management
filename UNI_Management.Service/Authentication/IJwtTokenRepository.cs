using UNI_Management.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNI_Management.Service.Authentication
{
    public interface IJwtTokenRepository
    {
        public string GenerateJwtToken(string email, int id);
        public string GenerateTeamMemberJWTAuthetication();
        public bool ValidateToken(string token, out JwtSecurityToken jwtSecurityToken);
    }
}