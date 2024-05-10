using AutoMapper;
using infoManagerAPI.DTO.Authenticate.Request;
using infoManagerAPI.DTO.User.Request;
using infoManagerAPI.DTO.User.Response;
using infoManagerAPI.Exceptions;
using infoManagerAPI.Interfaces.Repositories;
using infoManagerAPI.Interfaces.Services;
using infoManagerAPI.Models;
using infoManagerAPI.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace infoManagerAPI.Services
{
    public class UsersService(IUsersRepository repository, IMapper mapper) : IUsersService
    {
        public async Task<UserResponse> CreateAsync(UserRequest user)
        {
            var emailExist = await repository.GetUserAsync(user.Email);
            if (emailExist != null)
            {
                throw new BadRequestException("Email already registered");
            }

            _= await ValidatePassword(user.Password);

            user.Password = Cryptography.EncryptPassword(user.Password);

            var NewUser = mapper.Map<User>(user);
            var Data = await repository.CreateAsync(NewUser);
            if (!Data) throw new Exception("Error to save new user");
            var result = mapper.Map<UserResponse>(NewUser);
            return result;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await repository.GetByIdAsync(id);
            if (data == null) throw new NotFoundException($"The ID does not exist in ours database");

            var result = await repository.DeleteAsync(data);
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


        public async Task<bool> UpdatePasswordAsync(int id, PasswordRequest request)
        {
            if (request.Password == null || request.Password != request.ConfirmPassword)
                throw new BadRequestException("Password and confirmation password must have the same value. Password must be at least 8 characters long, contain letters, numbers, and at least one uppercase letter");
            
            var user = await repository.GetByIdAsync(id);
            if (user == null) throw new NotFoundException($"The ID does not exist in ours database");

            _ = await ValidatePassword(request.Password);
            user.Password = Cryptography.EncryptPassword(request.Password);
            
            var result = await repository.UpdatePasswordAsync(user);
            return result;
        }

        public async Task<bool> ValidatePassword(string password)
        {
            if (password.Length < 8 || !Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"))
            {
                throw new BadRequestException("Password must be at least 8 characters long, contain letters, numbers, and at least one uppercase letter");
            }
            return true;
        }

       
    }
}
