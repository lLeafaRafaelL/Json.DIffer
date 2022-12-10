using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Json.Differ.Infrastructure.Context
{
    public static class MigrationExtensions
    {
        public static IApplicationBuilder ExecuteMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
               .GetRequiredService<IServiceScopeFactory>()
               .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<JsonDifferContext>())
                {
                    context.Database.Migrate();
                }
            }
            return app;
        }
    }
}