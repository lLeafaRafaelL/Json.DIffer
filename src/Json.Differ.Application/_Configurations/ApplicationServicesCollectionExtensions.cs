using Json.Differ.Application.Comparisons;
using Json.Differ.Application.Files.CompareFilesDiffs;
using Json.Differ.Application.Files.UploadFileToCompare;
using Json.Differ.Core.Mapping;
using Json.Differ.Core.Validation;
using Json.Differ.Domain.Comparisons.Models;
using Json.Differ.Infrastructure._Configurations;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Json.Differ.Application._Configurations
{
    public static class ApplicationServicesCollectionExtensions
    {
        public static void AddApplicationHandlers(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddShared(configuration);
            AddValidator(services);
            AddMappers(services);

            services.AddScoped<IRequestHandler<UploadFileToCompareRequestDto, UploadFileToCompareResonseDto>, UploadFileToCompareHandler>();
            services.AddScoped<IRequestHandler<CompareFilesDiffsRequestDto, CompareFilesDiffsResponseDto>, CompareFilesDiffsHandler>();
        }

        private static void AddMappers(IServiceCollection services)
        {
            services.AddScoped<IMapper<Comparison, ComparisonDto>, ComparisonMapper>();
        }

        private static void AddValidator(IServiceCollection services)
        {
            services.AddScoped<IDtoValidator<UploadFileToCompareRequestDto>, UploadFileToCompareValidator>();
            services.AddScoped<IDtoValidator<CompareFilesDiffsRequestDto>,CompareFilesDiffsRequestValidator>();      
        }
    }
}
