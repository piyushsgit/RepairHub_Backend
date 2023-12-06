using Microsoft.Win32;
using Repository;
using Services;

namespace RepairHub
{
  
        public static class DataRegister
        {
            public static void DataRegisters(this IServiceCollection services)
            {
                Configure(services, RegisterRepository.GetTypes());
                Configure(services, RegisterService.GetTypes());

            }
            public static void Configure(IServiceCollection services, Dictionary<Type, Type> types)
            {
                foreach (KeyValuePair<Type, Type> type in types)
                {
                    services.AddScoped(type.Key, type.Value);
                }

            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSession();
            services.AddMvc();
            services.AddHttpContextAccessor();

        }
        }
    
}
