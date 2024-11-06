using Lab_1.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Lab_1
{
    internal class AuthOptions
    {
        public const string ISSUER = "AuthServer"; 
        public const string AUDIENCE = "AuthClient"; 
        const string KEY = "SymmetricSecurityKeySymmetricSecurityKeySymmetricSecurityKey"; 
        public const int LIFETIME = 1; 

  
        private static List<User> users = new List<User>
        {
            new User {Login="knyazev_admin", Password="pass", Role = "admin" },
            new User { Login="knyazev_user", Password="pass", Role = "user" }
        };

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

        internal static object GenerateToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;


            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromHours(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new { token = encodedJwt };
        }

        internal static ClaimsIdentity GetIdentity(string username, string password)
        {
            User User = users.FirstOrDefault(x => x.Login == username && x.Password == password);
            if (User != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, User.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, User.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }


            return null;
        }
    }
}
