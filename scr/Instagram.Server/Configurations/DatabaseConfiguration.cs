using Instagram.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Server.Configurations
{
    public static class DatabaseConfiguration
    {
        public static void ConfigureDatabase(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
              options.UseSqlServer(connectionString));
        }
    }
}
