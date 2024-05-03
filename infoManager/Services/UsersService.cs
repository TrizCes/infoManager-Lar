using AutoMapper;
using infoManagerAPI.DTO.Authenticate.Request;
using infoManagerAPI.DTO.User.Request;
using infoManagerAPI.DTO.User.Response;
using infoManagerAPI.Exceptions;
using infoManagerAPI.Interfaces.Repositories;
using infoManagerAPI.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace infoManagerAPI.Services
{
    public class UsersService(IUsersRepository repository, IMapper mapper, IConfiguration configuration) : IUsersService
    {
        public async Task<UserResponse> CreateAsync(UserRequest user)
        {
            var emailExist = await repository.GetUserAsync(user.Email);
            if (emailExist == null) throw new BadRequestException("Email already registered");


        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserResponse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponse?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> UpdatePasswordAsync(string password, UserRequest user)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GeneratorJwtToken(AuthenticationRequest request)
        {
            if (!request.IsValid)
                throw new BadRequestException("Invalid data");

            var user = await repository.GetUserAsync(request.UserEmail!);

            if (user == null)
                throw new NotFoundException("Invalid data");

            if (request.Password != user.Password)
                throw new UnauthorizedException("User or password invalid");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("jwtKey")!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim("id", user.Id.ToString(), ClaimValueTypes.Integer32),
                        new Claim("role", Convert.ToInt32(user.Role).ToString(), ClaimValueTypes.Integer32)
                    }
                ),
                Expires = DateTime.UtcNow.AddMinutes(
                    configuration.GetValue<int>("App:secret-timeout")!
                ),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
