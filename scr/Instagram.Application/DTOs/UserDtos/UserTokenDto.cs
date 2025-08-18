using static Instagram.Application.DTOs.UserDtos.CreateUserDto;

namespace Instagram.Application.DTOs.UserDtos;

public class UserTokenDto
{
    public long UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRoleDto Role { get; set; }

}
