using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using iteraBus.Dominio.Entidades;
using iteraBus.Dominio.Enumeradores;
using iteraBus.Api.Models;
using System.Security.Cryptography;
using BCrypt.Net;
using iteraBus.Aplicacao.Interfaces;

namespace iteraBus.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioAplicacao _usuarioAplicacao;
        private readonly IConfiguration _configuration;

        public AuthController(IUsuarioAplicacao usuarioAplicacao, IConfiguration configuration)
        {
            _usuarioAplicacao = usuarioAplicacao;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLogin usuarioLogin)
        {
            try
            {
                //Busca o usuário pelo email
                var usuario = await _usuarioAplicacao.ObterPorEmailAsync(usuarioLogin.Email);

                //Veridica se o usuário existe e se a senha está correta
                if (usuario == null || usuarioLogin.Senha != usuario.Senha || !usuario.Ativo)
                    return Unauthorized("Email ou senha inválidos.");

                //Gera o token JWT
                var token = GenerateJwtToken(usuario);

                // Configura as opções do cookie
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true, // Impede acesso via JavaScript
                    Secure = true, // Apenas envia o cookie em conexões HTTPS
                    SameSite = SameSiteMode.Strict, // Protege contra ataques CSRF
                    Expires = DateTime.UtcNow.AddMinutes(30), // Expiração do cookie
                };

                // Armazena o token, role e nome em cookies HttpOnly
                Response.Cookies.Append("auth_token", token, cookieOptions);
                Response.Cookies.Append("user_role", ((TiposUsuario)usuario.TipoUsuarioId).ToString(), cookieOptions);
                Response.Cookies.Append("user_name", usuario.Nome, cookieOptions);

                return Ok(new { message = "Login bem-sucedido." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            //Configura um cookie vazio com expiração no passado
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(-1), // Expira o cookie
            };

            // Remove os cookies
            Response.Cookies.Append("auth_token", "", cookieOptions);
            Response.Cookies.Append("user_role", "", cookieOptions);
            Response.Cookies.Append("user_name", "", cookieOptions);

            return Ok(new { message = "Logout bem-sucedido." });

        }

        [HttpGet("user-role")]
        public IActionResult GetUserRole()
        {
            // Obtém o token do cookie
            var token = Request.Cookies["auth_token"];

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token não encontrado.");
            }

            //Valida o token e extrai as claims
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                //Extrai o role do token
                var role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                if (role == null)
                {
                    return BadRequest("Role não encontrada no token.");
                }

                return Ok(new { role });
            }
            catch
            {
                return Unauthorized("Token inválido.");
            }
        }

        [HttpGet("user-info")]
        public IActionResult GetUserInfo()
        {
            // Obtém o token do cookie
            var token = Request.Cookies["auth_token"];

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token não encontrado.");
            }

            // Valida o token e extrai as claims
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                // Extrai o nome do usuário do token
                var nome = jwtToken.Claims.FirstOrDefault(c => c.Type == "nome")?.Value;
                var id = jwtToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value; 

                if (nome == null)
                {
                    return BadRequest("Nome do usuário não encontrado no token.");
                }

                return Ok(new { nome, id });
            }
            catch
            {
                return Unauthorized("Token inválido.");
            }
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Cria as "claims" (informações) do token
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, usuario.Email), // Email do usuário
        new Claim(ClaimTypes.Role, ((TiposUsuario)usuario.TipoUsuarioId).ToString()), // Role do usuário
        new Claim("nome", usuario.Nome),
        new Claim("id", usuario.Id.ToString())
    };

            // Configura o token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"], // Emissor
                audience: _configuration["Jwt:Audience"], // Público-alvo
                claims: claims, // Informações do usuário
                expires: DateTime.Now.AddMinutes(30), // Expiração do token
                signingCredentials: credentials // Chave de assinatura
            );

            // Gera o token como uma string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GerarToken()
        {
            var tokenBytes = new byte[32];
            RandomNumberGenerator.Fill(tokenBytes); // Preenche o array com bytes aleatórios
            return Convert.ToBase64String(tokenBytes);
        }
    }
}