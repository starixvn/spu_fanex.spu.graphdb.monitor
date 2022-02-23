namespace Fanex.SPU.GraphDb.Monitor.JobService.Services
{
    using Fanex.SPU.GraphDb.Monitor.Features.CTSCustomer;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using static Fanex.SPU.GraphDb.Monitor.Shares.Consts;

    public interface IMonitorCTSCustomerJobService
    {
        Task<bool> MonitorCTSCustomerNode();
    }

    public class MonitorCTSCustomerJobService : IMonitorCTSCustomerJobService
    {
        private readonly ICTSCustomerService ctscustomerService;

        public MonitorCTSCustomerJobService(ICTSCustomerService ctscustomerService)
        {
            this.ctscustomerService = ctscustomerService;
        }

        public Task<bool> MonitorCTSCustomerNode()
        {
            var lastScannedCTSCustID = ctscustomerService.GetLastScannedCTSCustID(SystemParameters.MonitorNeo4jCustomer_LastScannedCTSCustID);
            var mysqlCusts = ctscustomerService.GetCTSCustomerFromMySQL(lastScannedCTSCustID);
            if (mysqlCusts.Any() && mysqlCusts.Count() >= 500)
            {
                var missingCusts = ctscustomerService.GetMissingCTSCustomer(mysqlCusts);
                if (missingCusts.Any())
                {
                    //NotifyTelegramMessage(mess);
                    ctscustomerService.InsertMissingCTSCustomer(missingCusts, DateTime.Now);
                }

                var maxScannedCTSCustID = mysqlCusts.Max(c => c.CTSCustID);
                ctscustomerService.UpdateLastScannedCTSCustID(maxScannedCTSCustID, SystemParameters.MonitorNeo4jCustomer_LastScannedCTSCustID);
            }


            return Task.FromResult(true);
        }
    }
}
