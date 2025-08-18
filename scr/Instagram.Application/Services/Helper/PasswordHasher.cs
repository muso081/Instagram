namespace Instagram.Application.Services.Helper;
using BCrypt.Net;

public static class PasswordHasher
{
    public static (string Hash, string Salt) HashedPass(string password)
    {
        var salt = Guid.NewGuid().ToString();
        var hash = BCrypt.HashPassword(password + salt);
        return (Hash: hash, Salt: salt);
    }
    public static bool VerifyPass(string pass, string hash, string salt)
    {
        var saltedPass = BCrypt.Verify(pass + salt, hash);
        return saltedPass;
    }
}
