namespace Fanex.SPU.GraphDb.Monitor.JobService.Services
{
    using Fanex.SPU.GraphDb.Monitor.Features.CTSCustomer;
    using Fanex.SPU.GraphDb.Monitor.Features.CustClassification;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using static Fanex.SPU.GraphDb.Monitor.Shares.Consts;

    public interface IMonitorCustClassificationJobService
    {
        Task<bool> MonitorCustomerClassification();
    }

    public class MonitorCustClassificationJobService : IMonitorCustClassificationJobService
    {
        private readonly ICustomerClassificationService custClassificationService;
        private readonly ISystemParameterService sysParamService;

        public MonitorCustClassificationJobService(ICustomerClassificationService custClassificationService,
                                                    ISystemParameterService sysParamService)
        {
            this.custClassificationService = custClassificationService;
            this.sysParamService = sysParamService;
        }

        public Task<bool> MonitorCustomerClassification()
        {
            int lastScannedCustID =  sysParamService.GetValueFromParamID(SystemParameters.MonitorNeo4jClassification_LastScannedCustID);
            var custClassificationFromMySQL = custClassificationService.GetCustClassificationFromMySQL(lastScannedCustID);
            if (custClassificationFromMySQL.Any() && custClassificationFromMySQL.Count() >= 500)
            {
                var missingCustClass = custClassificationService.GetMissingCustClassification(custClassificationFromMySQL);
                if (missingCustClass.Any())
                {
                    custClassificationService.InsertMissingCustClassification(missingCustClass);
                }

                var maxScannedCTSAssDevID = custClassificationFromMySQL.Max(c => c.CustID);
                sysParamService.UpdateValueByParamID(maxScannedCTSAssDevID, SystemParameters.MonitorNeo4jClassification_LastScannedCustID);
            }

            return Task.FromResult(true);
        }
    }
}
