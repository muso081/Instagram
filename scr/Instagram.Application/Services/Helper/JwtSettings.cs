using Microsoft.Extensions.Primitives;

namespace Instagram.Application.Services.Helper;

public class JwtSettings
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
    public int Lifetime { get; set; }
}
