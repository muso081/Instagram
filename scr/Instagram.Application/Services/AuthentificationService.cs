using FluentValidation;
using Instagram.Application.DTOs;
using Instagram.Application.DTOs.UserDtos;
using Instagram.Application.Interfaces;
using Instagram.Application.Services.Helper;
using Instagram.Domain.Entities;
using Microsoft.AspNetCore.Identity.Data;
using System.Security.Claims;
using static Instagram.Application.DTOs.UserDtos.CreateUserDto;
using static Instagram.Domain.Entities.User;

namespace Instagram.Application.Services
{
    internal class AuthentificationService : IAuthentificationService
    {
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IValidator<CreateUserDto> _validator;

        public AuthentificationService(ITokenService tokenService, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IValidator<CreateUserDto> validator)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _validator = validator;
        }

        public async Task<TokenInfoDto> RefreshTokenAsync(RefreshTokenDto request)
        {

            ClaimsPrincipal? principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
            if (principal == null) throw new Exception("Invalid access token.");


            var userClaim = principal.FindFirst(c => c.Type == "UserId");
            var userId = long.Parse(userClaim.Value);


            var refreshToken = await _refreshTokenRepository.SelectRefreshToken(request.RefreshToken, userId);
            if (refreshToken == null || refreshToken.ExpiresAt < DateTime.UtcNow || refreshToken.IsActive)
                throw new Exception("Invalid or expired refresh token.");

            refreshToken.IsActive = true;

            var user = await _userRepository.GetByIdAsync(userId);

            var userGetDto = new UserTokenDto()
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Role = (UserTokenDto.UserRoleDto)user.Role,
            };

            var newAccessToken = _tokenService.GenerateAccessToken(userGetDto);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            var refreshTokenToDB = new RefreshToken()
            {
                Token = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(21),
                IsActive = false,
                UserId = user.UserId
            };

            await _refreshTokenRepository.AddRefreshTokenAsync(refreshTokenToDB);

            return new TokenInfoDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                TokenType = "Bearer",
                ExpiresAt = 24
            };
        }

        public async Task<TokenInfoDto> SignInAsync(LoginDto user)
        {
            if (user.Email is null && user.Username is null)
            {
                throw new ArgumentException("Either Email or USername must be provided.");
            }

            var users = user.Username != null ? await _userRepository.GetByUsernameAsync(user.Username) :
                                                  await _userRepository.GetByEmailAsync(user.Email);
            if (users == null) throw new ArgumentException("Username or password is incorrect");

            var password = PasswordHasher.VerifyPass(user.Password, users.Password, users.Salt);
            if (!password) throw new ArgumentException("Username or password is incorrect");

            var gottenUser = new UserTokenDto
            {
                UserId = users.UserId,
                Username = users.Username,
                Email = users.Email,
                Role = (UserTokenDto.UserRoleDto)users.Role
            };
            var token = _tokenService.GenerateAccessToken(gottenUser);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var RefreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                UserId = users.UserId,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsActive = false
            };
            await _refreshTokenRepository.AddRefreshTokenAsync(RefreshTokenEntity);

            var tokenInfo = new TokenInfoDto
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                ExpiresAt = 7
            };
            return tokenInfo;
        }

        public Task<long> SignUpAsync(CreateUserDto user)
        {
            var validationResult = _validator.Validate(user);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Errors.FirstOrDefault()?.ErrorMessage);
            }
            var hashedPassword = PasswordHasher.HashedPass(user.Password);
            var newUser = new User
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Password = hashedPassword.Hash,
                Salt = hashedPassword.Salt,
                Bio = user.Bio,
                Role = (UserRole)user.Role
            };
            var id = _userRepository.InsertAsync(newUser);
            return id;
        }
    }
}
