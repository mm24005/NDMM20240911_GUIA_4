using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NDMM20240911_GUIA_4.Auth

{
    public class JwtAuthenticationService : IJwtAuthenticationService
    {
        private readonly string _key;

        public JwtAuthenticationService(string key)
        {
            _key = key;
        }

        public string Authenticate(string userName)
        {
            // Crear un manejador de tokens JWT
            var tokenHandler = new JwtSecurityTokenHandler();

            // Obtener la clave secreta desde la variable _key
            var tokenKey = Encoding.ASCII.GetBytes(_key);

            // Crear un descriptor de token con la información necesaria
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Definir la identidad del token con el nombre de usuario
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName)
                }),

                // Establecer la fecha de vencimiento (8 horas desde ahora)
                Expires = DateTime.UtcNow.AddHours(8),

                // Configurar la clave de firma y el algoritmo de firma
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            // Generar el token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Convertir el token a una cadena
            return tokenHandler.WriteToken(token);
        }
    }
}
