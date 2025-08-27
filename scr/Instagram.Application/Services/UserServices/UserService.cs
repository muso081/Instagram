using Instagram.Application.DTOs.UserDtos;
using Instagram.Application.Interfaces;
using static Instagram.Application.DTOs.UserDtos.GetUserDto;
using static Instagram.Domain.Entities.User;

namespace Instagram.Application.Services.UserServices;

internal class UserService(IUserRepository _userRepository) : IUserService
{
    public async Task DeleteUser(long id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new ArgumentException($"User with id {id} not found to delete.");
        }
        _userRepository.Delete(user);
    }

    public async Task<ICollection<GetUserDto>> GetAllUsers()
    {
        var users = await _userRepository.SelectAllAsync();
        users = users.Where(u => u.Role == UserRole.User).ToList();
        var res = users.Select(u => new GetUserDto
        {
            UserId = u.UserId,
            Username = u.Username,
            Email = u.Email,
            RoleDto = (UserRoleDto)u.Role
        }).ToList();

        return res;
    }

    public async Task<GetUserDto> GetUserByEmail(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null)
        {
            throw new ArgumentException($"User with email {email} not found.");
        }
        var res = new GetUserDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            RoleDto = (UserRoleDto)user.Role
        };
        return res;
    }

    public async Task<GetUserDto> GetUserById(long id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new ArgumentException($"User with id {id} not found.");
        }

        var res = new GetUserDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            RoleDto = (UserRoleDto)user.Role
        };
        return res;
    }

    public async Task<GetUserDto> GetUserByUsername(string username)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null)
        {
            throw new ArgumentException($"User with username {username} not found.");
        }

        var res = new GetUserDto
        {
            UserId = user.UserId,
            Username = user.Username,
            Email = user.Email,
            RoleDto = (UserRoleDto)user.Role
        };
        return res;

    }

    public async Task UpdateUserRole(long userId, CreateUserDto.UserRoleDto role)
    {
        await _userRepository.UpdateUserRole(userId, (UserRole)role);
    }
}
