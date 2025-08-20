using Instagram.Application.DTOs;
using Instagram.Application.DTOs.UserDtos;
using Microsoft.AspNetCore.Identity.Data;

namespace Instagram.Application.Services
{
    public interface IAuthentificationService
    {
        public Task<long> SignUpAsync(CreateUserDto user);
        public Task<TokenInfoDto> SignInAsync(LoginDto user);
        public Task<TokenInfoDto> RefreshTokenAsync(RefreshTokenDto refreshToken); 
    }
}