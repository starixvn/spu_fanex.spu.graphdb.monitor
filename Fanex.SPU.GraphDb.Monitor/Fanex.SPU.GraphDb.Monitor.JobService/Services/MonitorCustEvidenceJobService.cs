namespace Fanex.SPU.GraphDb.Monitor.JobService.Services
{
    using Fanex.SPU.GraphDb.Monitor.Features.CustEvidence;
    using System.Linq;
    using System.Threading.Tasks;
    using static Fanex.SPU.GraphDb.Monitor.Shares.Consts;

    public interface IMonitorCustEvidenceJobService
    {
        Task<bool> MonitorCustomerEvidence();
    }

    public class MonitorCustEvidenceJobService : IMonitorCustEvidenceJobService
    {
        private readonly ICustomerEvidenceService custEvidenceService;
        private readonly ISystemParameterService sysParamService;

        public MonitorCustEvidenceJobService(ICustomerEvidenceService custClassificationService,
                                                    ISystemParameterService sysParamService)
        {
            this.custEvidenceService = custClassificationService;
            this.sysParamService = sysParamService;
        }

        public Task<bool> MonitorCustomerEvidence()
        {
            int lastScannedCustID =  sysParamService.GetValueFromParamID(SystemParameters.MonitorNeo4jCustEvidence_LastScannedCustEvidID);
            var custClassificationFromMySQL = custEvidenceService.GetCustEvidenceFromMySQL(lastScannedCustID);
            if (custClassificationFromMySQL.Any() && custClassificationFromMySQL.Count() >= 500)
            {
                var missingCustClass = custEvidenceService.GetMissingCustEvidence(custClassificationFromMySQL);
                if (missingCustClass.Any())
                {
                    custEvidenceService.InsertMissingCustEvidence(missingCustClass);
                }

                var maxScannedCustEvidID = custClassificationFromMySQL.Max(c => c.CustEvidID);
                sysParamService.UpdateValueByParamID(maxScannedCustEvidID, SystemParameters.MonitorNeo4jCustEvidence_LastScannedCustEvidID);
            }

            return Task.FromResult(true);
        }
    }
}
