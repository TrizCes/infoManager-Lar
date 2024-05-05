using AutoMapper;
using infoManagerAPI.DTO.Authenticate.Request;
using infoManagerAPI.DTO.User.Request;
using infoManagerAPI.DTO.User.Response;
using infoManagerAPI.Exceptions;
using infoManagerAPI.Interfaces.Repositories;
using infoManagerAPI.Interfaces.Services;
using infoManagerAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace infoManagerAPI.Services
{
    public class UsersService(IUsersRepository repository, IMapper mapper, IConfiguration configuration) : IUsersService
    {
        public async Task<UserResponse> CreateAsync(UserRequest user)
        {
            var emailExist = await repository.GetUserAsync(user.Email);
            if (emailExist != null)
            {
                throw new BadRequestException("Email already registered");
            }

            _=ValidatePassword(user.Password);

            var NewUser = mapper.Map<User>(user);
            var Data = await repository.CreateAsync(NewUser);
            var result = mapper.Map<UserResponse>(Data);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await GetByIdAsync(id);
             
            var result = await repository.DeleteAsync(id);
            if (!result) throw new DeleteFailureException("Failed to delete");
            return result;
        }

        public async Task<List<UserResponse>> GetAllAsync()
        {
            return mapper.Map<List<UserResponse>>(await repository.GetAllAsync());
        }

        public async Task<UserResponse?> GetByIdAsync(int id)
        {
            var data = await repository.GetByIdAsync(id);
            if (data == null) throw new NotFoundException("This ID does not maches in our database");
            return mapper.Map<UserResponse?>(data);
        }


        public async Task<bool> UpdatePasswordAsync(string password, UserRequest user)
        {
            _=ValidatePassword(password);
            return true;
        }

        public async Task<bool> ValidatePassword(string password)
        {
            if (password.Length < 8 || !Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"))
            {
                throw new BadRequestException("Password must be at least 8 characters long, contain letters, numbers, and at least one uppercase letter");
            }
            return true;
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
