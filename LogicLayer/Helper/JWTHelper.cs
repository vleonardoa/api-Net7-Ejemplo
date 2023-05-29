using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LogicLayer.Helper
{
    public class IMG
    {
        public string CreateToken(string username, string roles, string organizaciones, string Correo, string secretKey)
        {

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, username));
            claims.AddClaim(new Claim(ClaimTypes.Name, username));
            claims.AddClaim(new Claim(ClaimTypes.Email, Correo));

            string[] arrayRols = roles.Split(';');
            string[] arrayOrganizaciones = organizaciones.Split(';');
            /*Roles*/
            foreach (string rol in arrayRols)
            {
                claims.AddClaim(new Claim(ClaimTypes.Role, rol));
            }

            foreach (string org in arrayOrganizaciones)
            {
                claims.AddClaim(new Claim("Organizacion", org));
            }

            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = claims,
                //Expires = DateTime.UtcNow.AddHours(.20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(createdToken);
        }
    }
}