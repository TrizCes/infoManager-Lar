using AutoMapper;
using System.Reflection;

namespace infoManagerAPI.Mapper
{
    public static class AutoMapperConfig
    {
        public static void AutoMapperConfiguration(this IServiceCollection services)
        {
            var profiles = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.BaseType == typeof(Profile))
                .ToArray();

            services.AddAutoMapper(profiles);
        }
    }
}
