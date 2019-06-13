using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UserManagmentSystem
{
    public class TokenManager
    {
        private static string securityKey = "Secure_key_for_token_validation";
        public static SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

        public static class TokenParameters
        {
            public static Func<DateTime> expireDate => () => DateTime.Now.AddHours(24 - DateTime.Now.Hour);
            public static SigningCredentials userSigningCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);
            public static string issuer = "trc";
            public static string audience = "users";
        }
        
        public static string generateToken(string username)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, username));
            var token = new JwtSecurityToken(
                expires: TokenParameters.expireDate(),
                signingCredentials: TokenParameters.userSigningCredentials,
                issuer: TokenParameters.issuer,
                audience: TokenParameters.audience,
                claims: claims
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
