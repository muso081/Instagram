namespace Instagram.Application.Services.Helper;

public static class TimeHelper
{
    public static DateTime GetDateTime()
    {
        var utc = DateTime.UtcNow.AddHours(5);
        return utc;
    }
}
