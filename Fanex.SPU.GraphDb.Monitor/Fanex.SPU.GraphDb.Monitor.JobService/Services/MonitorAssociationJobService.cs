namespace Fanex.SPU.GraphDb.Monitor.JobService.Services
{
    using Fanex.SPU.GraphDb.Monitor.Features.Association;
    using System.Linq;
    using System.Threading.Tasks;
    using static Fanex.SPU.GraphDb.Monitor.Shares.Consts;

    public interface IMonitorAssociationJobService
    {
        Task<bool> MonitorAssociationByDevice();

        Task<bool> MonitorAssociationByIP();

        Task<bool> MonitorAssociationByManual();
        Task<bool> MonitorAssociationByAI();
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
            if (assByDeviceFromMySQL.Any() && assByDeviceFromMySQL.Count() >= 500)
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

        public Task<bool> MonitorAssociationByIP()
        {
            int lastScannedIDAssByIP = sysParamService.GetValueFromParamID(SystemParameters.MonitorNeo4jCustomer_LastScannedIDAssByIP);
            var assByIPFromMySQL = associationService.GetAssociationByIPFromMySql(lastScannedIDAssByIP);
            if (assByIPFromMySQL.Any() && assByIPFromMySQL.Count() >= 500)
            {
                var missingAssByIP = associationService.GetMissingAssociationByIP(assByIPFromMySQL);
                if (missingAssByIP.Any())
                {
                    associationService.InsertMissingAssByIP(missingAssByIP);
                }

                var maxScannedIDAssByIP = assByIPFromMySQL.Max(c => c.AssByIpID);
                sysParamService.UpdateValueByParamID(maxScannedIDAssByIP, SystemParameters.MonitorNeo4jCustomer_LastScannedIDAssByIP);
            }

            return Task.FromResult(true);
        }
        
        public Task<bool> MonitorAssociationByAI()
        {
            int lastScannedIDAssByAI = sysParamService.GetValueFromParamID(SystemParameters.MonitorNeo4jCustomer_LastScannedIDAssByAI);
            var assByAIFromMySQL = associationService.GetAssociationByAIFromMySql(lastScannedIDAssByAI);
            if (assByAIFromMySQL.Any() && assByAIFromMySQL.Count() >= 500)
            {
                var missingAssByAI = associationService.GetMissingAssociationByAI(assByAIFromMySQL);
                if (missingAssByAI.Any())
                {
                    associationService.InsertMissingAssByAI(missingAssByAI);
                }

                var maxScannedIDAssByIP = assByAIFromMySQL.Max(c => c.AssID);
                sysParamService.UpdateValueByParamID(maxScannedIDAssByIP, SystemParameters.MonitorNeo4jCustomer_LastScannedIDAssByAI);
            }

            return Task.FromResult(true);
        }

        public Task<bool> MonitorAssociationByManual()
        {
            int lastScannedIDAssByManual = sysParamService.GetValueFromParamID(SystemParameters.MonitorNeo4jCustomer_LastScannedIDAssByManual);
            var assByManualFromMySQL = associationService.GetAssociationByManualFromMySql(lastScannedIDAssByManual);
            if (assByManualFromMySQL.Any() && assByManualFromMySQL.Count() >= 200)
            {
                var missingAssByManual = associationService.GetMissingAssociationByManual(assByManualFromMySQL);
                if (missingAssByManual.Any())
                {
                    associationService.InsertMissingAssByManual(missingAssByManual);
                }

                var maxScannedIDAssByIP = assByManualFromMySQL.Max(c => c.AssID);
                sysParamService.UpdateValueByParamID(maxScannedIDAssByIP, SystemParameters.MonitorNeo4jCustomer_LastScannedIDAssByManual);
            }

            return Task.FromResult(true);
        }
    }
}