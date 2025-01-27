using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UNI_Management.Common.DependencyInjection;
using UNI_Management.Common.Email;
using UNI_Management.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UNI_Management.Service.Authentication
{
    [TransientDependency(ServiceType = typeof(IJwtTokenRepository))]
    public class JwtTokenRepository : IJwtTokenRepository
    {
        private readonly IConfiguration _config;
        public JwtTokenRepository(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJWTAuthetication()
        {
            var claims = new List<Claim>
            {
                //new Claim(ClaimTypes.Email, user.Email),
                //new Claim("UserId", user.UserId.ToString()),
                //new Claim(ClaimTypes.NameIdentifier, user.Email),
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(10000);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );
            EmailHelper.SendMail("lonavala.thewalkiestalkies14824@gmail.com", "Log-In OTP for RB-News", DateTime.Now + " strat in otp check 53 ");
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateTeamMemberJWTAuthetication()
        {
            var claims = new List<Claim>
    {
        //new Claim(ClaimTypes.Email, teamMember.Email),
        //new Claim("Email", teamMember.Email),
        //new Claim("TeamMemberId", teamMember.TeamMemberId.ToString()),
        //new Claim("Username", teamMember.UserName),
        //new Claim("RoleId", teamMember.RoleId.ToString()),
        //new Claim("RoleName", teamMember.RoleName.ToString()),
        //new Claim(JwtHeaderParameterNames.Kid, Guid.NewGuid().ToString()),
        //new Claim(ClaimTypes.NameIdentifier, teamMember.Email)
    };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires =
                DateTime.Now.AddDays(30);

            var token = new JwtSecurityToken(
               _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateToken(string token, out JwtSecurityToken jwtSecurityToken)
        {
            jwtSecurityToken = null;
            if (token == null)
            {
                return false;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
    ,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidIssuer = _config["Jwt:Issuer"],
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                jwtSecurityToken = (JwtSecurityToken)validatedToken;
                if (jwtSecurityToken != null)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return (false);
            }
        }
    }
}