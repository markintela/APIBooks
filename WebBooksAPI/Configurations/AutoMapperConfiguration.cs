using Manager.Mappings;

namespace WebBooksAPI.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(NewBookMappingProfile),
                typeof(NewLibraryMappingProfile),
                typeof(UserAPIMappingProfile));
        }
    }
}
