using infoManagerAPI.DTO.Authenticate.Request;
using infoManagerAPI.Exceptions;
using infoManagerAPI.Interfaces.Repositories;
using infoManagerAPI.Interfaces.Services;
using infoManagerAPI.Utils;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Core.Types;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace infoManagerAPI.Services
{
    public class AuthService : IAuthService
    {
        public IUsersRepository repository { get; set; }
        public IConfiguration configuration { get; set; }

        public AuthService(IUsersRepository repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
        }

        public async Task<string> GeneratorJwtToken(AuthenticationRequest request)
        {
            if (!request.IsValid)
                throw new BadRequestException("Invalid data");

            var user = await repository.GetUserAsync(request.Email!);

            if (user == null)
                throw new NotFoundException("Invalid data");

            request.Password = Cryptography.EncryptPassword(request.Password);

            if (request.Password != user.Password)
                throw new UnauthorizedException("User or password invalid");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("jwtKey")!);
            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString(), ClaimValueTypes.Integer32),
                new Claim("email", user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(120),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
