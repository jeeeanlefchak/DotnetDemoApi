using Demo.Business.Implementation;
using Demo.Business.Repository;

namespace Demo.Services
{
    public static class Scopes
    {
        public static void AddScopes(IServiceCollection services)
        {
            services.AddScoped<IAddress, AddressRepository>();

        }
    }
}
