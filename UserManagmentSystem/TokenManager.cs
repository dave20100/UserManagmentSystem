using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagmentSystem
{
    public class TokenManager
    {
        private static string securityKey = "Secure_key_for_token_validation";
        public static SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        public static string generateToken()
        {
            var userSigningCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddDays(14),
                signingCredentials: userSigningCredentials,
                issuer:"dave",
                audience:"users"
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
