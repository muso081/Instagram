using FluentValidation;
using Instagram.Application.DTOs;
using Instagram.Application.DTOs.UserDtos;
using Instagram.Application.Interfaces;
using Instagram.Application.Services.Helper;
using Instagram.Domain.Entities;
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

        public Task<TokenInfoDto> RefreshTokenAsync(RefreshTokenDto refreshToken)
        {
            throw new NotImplementedException();
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
            var ahshedPassword = PasswordHasher.HashedPass(user.Password);
            var newUser = new User
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Password = ahshedPassword.Hash,
                Salt = ahshedPassword.Salt,
                Bio = user.Bio,
                Role = (UserRole)user.Role
            };
            var id = _userRepository.InsertAsync(newUser);
            return id;
        }
    }
}
