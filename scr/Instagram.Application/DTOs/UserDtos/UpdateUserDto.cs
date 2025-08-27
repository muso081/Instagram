namespace Instagram.Application.DTOs.UserDtos;

public class UpdateUserDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Bio { get; set; }
    public string ProfilePicture { get; set; }
    public UserRoleDto RoleDto { get; set; }
    public enum UserRoleDto
    {
        User,
        Admin,
        SuperAdmin
    }
}
