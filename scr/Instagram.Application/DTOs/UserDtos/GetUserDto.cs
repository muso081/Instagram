namespace Instagram.Application.DTOs.UserDtos;

public class GetUserDto
{
    public long UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public UserRoleDto RoleDto { get; set; }
    public enum UserRoleDto
    {
        User,
        Admin,
        SuperAdmin
    }
}
