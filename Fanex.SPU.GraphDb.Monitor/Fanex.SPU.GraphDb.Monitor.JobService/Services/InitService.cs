namespace Fanex.SPU.GraphDb.Monitor.JobService.Services
{
    using Fanex.SPU.GraphDb.Monitor.Features.CTSCustomer;
    using Fanex.SPU.GraphDb.Monitor.Features.SystemParameter;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using System.Threading.Tasks;
    public static class InitService
    {
        public static void RegisterService(this IServiceCollection services)
        {
            RegisterMonitorCTSCustomerJobService(services);
        }

        private static void RegisterMonitorCTSCustomerJobService(this IServiceCollection services)
        {
            services.AddSingleton<IMonitorCTSCustomerJobService, MonitorCTSCustomerJobService>();
            services.AddSingleton<ICTSCustomerService, CTSCustomerService>();
            services.AddSingleton<ICTSCustomerDataAccess, CTSCustomerDataAccess>();
            services.AddSingleton<ISystemParameterDataAccess, SystemParameterDataAccess>();
        }
    }
}
