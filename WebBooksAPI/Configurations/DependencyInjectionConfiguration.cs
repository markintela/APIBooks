using Data.Repository;
using Manager.Implementation;
using Manager.Interfaces;
using Manager.Mappings;

namespace WebBooksAPI.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services) {
          
            services.AddScoped<ILibraryRepository, LibraryRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUserAPIRepository, UserAPIRepository>();
            services.AddScoped<ILibraryManager, LibraryManager>();
            services.AddScoped<IBookManager, BookManager>();
            services.AddScoped<IUserAPIManager, UserAPIManager>();


        }
    }
}
