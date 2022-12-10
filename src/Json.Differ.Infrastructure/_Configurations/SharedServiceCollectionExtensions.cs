using Json.Differ.Core.Data.EntityFrameworkCore;
using Json.Differ.Core.Data;
using Json.Differ.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Json.Differ.Domain.Files.Repositories;
using Json.Differ.Infrastructure.Domain.Files.Repositories;
using Json.Differ.Domain.Comparisons.Repositories;
using Json.Differ.Infrastructure.Domain.Comparisons.Repositories;
using Json.Differ.Domain.Files.Factories;
using Json.Differ.Domain.Files.Factories.Impl;
using Json.Differ.Shared.Correlations;
using Json.Differ.Shared.Correlations.Service;
using Json.Differ.Domain.Comparisons.Factories;
using Json.Differ.Domain.Comparisons.Factories.Impl;

namespace Json.Differ.Infrastructure._Configurations
{
    public static class SharedServiceCollectionExtensions
    {
        public static void AddShared(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddScoped<ICorrelationService>(_ => new CorrelationService());

            AddDomainServices(services);
            AddRepositories(services, configuration);
        }

        private static void AddDomainServices(IServiceCollection services)
        {
            services.AddScoped<IComparisonFactory, ComparisonFactory>();
            services.AddScoped<IFileFactory, FileFactory>();
        }

        private static void AddRepositories(IServiceCollection services, IConfigurationRoot configuration)
        {
            var connectionString = configuration.GetConnectionString("JsonDifferConnection");       

            services.AddSingleton(new DbOptions(connectionString));

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<JsonDifferContext>().UseSqlServer(connectionString);
            services.AddSingleton(dbContextOptionsBuilder.Options);
            services.AddDbContext<JsonDifferContext>();

            services.AddScoped(x => new EFUnityOfWorkAsync(x.GetRequiredService<JsonDifferContext>()));
            services.AddScoped<IUnityOfWorkAsync>(x => x.GetRequiredService<EFUnityOfWorkAsync>());

            services.AddScoped<IFileRepository>(x =>
                new FileRepository(x.GetRequiredService<EFUnityOfWorkAsync>()));

            services.AddScoped<IComparisonRepository>(x =>
                new ComparisonRepository(x.GetRequiredService<EFUnityOfWorkAsync>()));
        }

    }
}