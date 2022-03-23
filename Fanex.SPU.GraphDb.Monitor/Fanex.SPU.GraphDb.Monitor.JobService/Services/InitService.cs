namespace Fanex.SPU.GraphDb.Monitor.JobService.Services
{
    using Fanex.SPU.GraphDb.Monitor.Features.Association;
    using Fanex.SPU.GraphDb.Monitor.Features.CTSCustomer;
    using Fanex.SPU.GraphDb.Monitor.Features.CustClassification;
    using Fanex.SPU.GraphDb.Monitor.Features.CustEvidence;
    using Fanex.SPU.GraphDb.Monitor.Features.SystemParameter;
    using Microsoft.Extensions.DependencyInjection;

    public static class InitService
    {
        public static void RegisterService(this IServiceCollection services)
        {
            RegisterMonitorCTSCustomerJobService(services);
            RegisterMonitorAssociationJobService(services);
            RegisterMonitorCustClassificationJobService(services);
            RegisterMonitorCustEvidenceJobService(services);
        }

        private static void RegisterMonitorCTSCustomerJobService(this IServiceCollection services)
        {
            services.AddSingleton<IMonitorCTSCustomerJobService, MonitorCTSCustomerJobService>();
            services.AddSingleton<ICTSCustomerService, CTSCustomerService>();
            services.AddSingleton<ICTSCustomerDataAccess, CTSCustomerDataAccess>();
            services.AddSingleton<ISystemParameterDataAccess, SystemParameterDataAccess>();
        }

        private static void RegisterMonitorAssociationJobService(this IServiceCollection services)
        {
            services.AddSingleton<IMonitorAssociationJobService, MonitorAssociationJobService>();
            services.AddSingleton<IAssociationService, AssociationService>();
            services.AddSingleton<ISystemParameterService, SystemParameterService>();
            services.AddSingleton<IAssociationDataAccess, AssociationDataAccess>();
        }
        
        private static void RegisterMonitorCustClassificationJobService(this IServiceCollection services)
        {
            services.AddSingleton<IMonitorCustClassificationJobService, MonitorCustClassificationJobService>();
            services.AddSingleton<ICustomerClassificationService, CustomerClassificationService>();
            services.AddSingleton<ISystemParameterService, SystemParameterService>();
            services.AddSingleton<ICustomerClassificationDataAccess, CustomerClassificationDataAccess>();
        }
        
        private static void RegisterMonitorCustEvidenceJobService(this IServiceCollection services)
        {
            services.AddSingleton<IMonitorCustEvidenceJobService, MonitorCustEvidenceJobService>();
            services.AddSingleton<ICustomerEvidenceService, CustomerEvidenceService>();
            services.AddSingleton<ISystemParameterService, SystemParameterService>();
            services.AddSingleton<ICustomerEvidenceDataAccess, CustomerEvidenceDataAccess>();
        }
    }
}
