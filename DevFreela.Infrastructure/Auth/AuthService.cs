using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DevFreela.Infrastructure.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        // Construtor que recebe IConfiguration injetado
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // Método para gerar um token JWT com base no e-mail e função do usuário
        public string GenerationJwtToken(string email, string role)
        {
            // Obtenção das configurações para geração do token
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = _configuration["Jwt:key"];

            // Criação da chave de segurança com base na chave fornecida
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Definição das informações (claims) a serem incluídas no token
            var claims = new List<Claim>
            {
                new Claim("userName", email), // Adiciona o e-mail como claim personalizada
                new Claim(ClaimTypes.Role, role) // Adiciona a função do usuário como claim de papel
            };

            // Criação do token JWT com base nas configurações e informações fornecidas
            var token = new JwtSecurityToken(
                issuer: issuer, // Emissor do token
                audience: audience, // Destinatário do token
                expires: DateTime.Now.AddHours(8), // Data de expiração do token
                signingCredentials: credentials, // Credenciais para assinar o token
                claims: claims // Informações adicionais incluídas no token
            );

            // Manipulador para escrever o token JWT como uma string
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token); // Transforma o token em uma string

            return stringToken; // Retorna o token JWT como uma string
        }

        // Método para calcular o hash SHA256 de uma senha
        public string ComputeSha256Hash(string password)
        {
            // Criação de uma instância de SHA256 para realizar o cálculo do hash
            using (var sha256Hash = SHA256.Create())
            {
                // Computa o hash dos bytes da senha convertidos para UTF-8
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Construção de uma representação hexadecimal do hash "x2"
                var builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                // Retorna o hash SHA256 como uma string hexadecimal
                return builder.ToString();
            }
        }
    }
}
