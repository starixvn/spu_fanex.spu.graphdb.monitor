namespace Fanex.SPU.GraphDb.Monitor.JobService.Services
{
    using Fanex.SPU.GraphDb.Monitor.Features.Association;
    using System.Linq;
    using System.Threading.Tasks;
    using static Fanex.SPU.GraphDb.Monitor.Shares.Consts;

    public interface IMonitorAssociationJobService
    {
        Task<bool> MonitorAssociationByDevice();
    }

    public class MonitorAssociationJobService : IMonitorAssociationJobService
    {
        private readonly IAssociationService associationService;
        private readonly ISystemParameterService sysParamService;

        public MonitorAssociationJobService(IAssociationService associationService,
                                            ISystemParameterService sysParamService)
        {
            this.associationService = associationService;
            this.sysParamService = sysParamService;
        }

        public Task<bool> MonitorAssociationByDevice()
        {
            int lastScannedCTSAssDevID = sysParamService.GetValueFromParamID(SystemParameters.MonitorNeo4jCustomer_LastScannedCTSAssDevID);
            var assByDeviceFromMySQL = associationService.GetAssociationByDeviceFromMySql(lastScannedCTSAssDevID);
            if (assByDeviceFromMySQL.Any())
            {
                var missingAssByDev = associationService.GetMissingAssociationByDevice(assByDeviceFromMySQL);
                if (missingAssByDev.Any())
                {
                    associationService.InsertMissingAssByDevice(missingAssByDev);
                }

                var maxScannedCTSAssDevID = assByDeviceFromMySQL.Max(c => c.CTSAssDevID);
                sysParamService.UpdateValueByParamID(maxScannedCTSAssDevID, SystemParameters.MonitorNeo4jCustomer_LastScannedCTSAssDevID);
            }

            return Task.FromResult(true);
        }
    }
}
