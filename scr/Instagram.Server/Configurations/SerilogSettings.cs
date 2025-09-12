using Serilog;

namespace Instagram.Server.Configurations;

public static class SerilogSettings
{
    public static void ConfigureSerilog(this WebApplicationBuilder builder)
    {
        //Log.Logger = new LoggerConfiguration()
        //    .ReadFrom.Configuration(builder.Configuration)
        //    .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(dispose: true);

    }
}
