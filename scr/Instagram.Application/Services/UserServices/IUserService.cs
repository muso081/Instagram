using Instagram.Application.DTOs.UserDtos;
using static Instagram.Application.DTOs.UserDtos.CreateUserDto;

namespace Instagram.Application.Services.UserServices;

public interface IUserService
{
    public Task<GetUserDto> GetUserById(long id);
    public Task<GetUserDto> GetUserByUsername(string username);
    public Task<GetUserDto> GetUserByEmail(string email);
    public Task<ICollection<GetUserDto>> GetAllUsers();
    public Task DeleteUser(long id);
    public Task UpdateUserRole(long userId, UserRoleDto role);
}