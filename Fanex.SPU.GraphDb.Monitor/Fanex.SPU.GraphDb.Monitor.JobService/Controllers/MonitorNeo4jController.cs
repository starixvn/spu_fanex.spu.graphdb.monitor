namespace Fanex.SPU.GraphDb.Monitor.JobService.Controllers
{
    using Fanex.SPU.GraphDb.Monitor.JobService.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [ApiController]
    [Route("api/MonitorCTSCustomer")]
    public class MonitorNeo4jController : ControllerBase
    {
        private readonly IMonitorCTSCustomerJobService ctsCustomerJobService;
        private readonly IMonitorAssociationJobService associationJobService;
        private readonly IMonitorCustClassificationJobService custClassificationJobService;
        private readonly IMonitorCustEvidenceJobService custEvidenceJobService;

        public MonitorNeo4jController(
            IMonitorCTSCustomerJobService ctsCustomerJobService,
            IMonitorAssociationJobService associationJobService,
            IMonitorCustClassificationJobService custClassificationJobService,
            IMonitorCustEvidenceJobService custEvidenceJobService)
        {
            this.ctsCustomerJobService = ctsCustomerJobService;
            this.associationJobService = associationJobService;
            this.custClassificationJobService = custClassificationJobService;
            this.custEvidenceJobService = custEvidenceJobService;

        }

        [HttpPost("MonitorCTSCustomerNode")]
        public async Task<bool> MonitorCTSCustomerNode()
        {
            return await ctsCustomerJobService.MonitorCTSCustomerNode();
        }
        
        [HttpPost("MonitorAssociationByDevice")]
        public async Task<bool> MonitorAssociationByDevice()
        {
            return await associationJobService.MonitorAssociationByDevice();
        }

        [HttpPost("MonitorAssociationByIP")]
        public async Task<bool> MonitorAssociationByIP()
        {
            return await associationJobService.MonitorAssociationByIP();
        }

        [HttpPost("MonitorAssociationByAI")]
        public async Task<bool> MonitorAssociationByAI()
        {
            return await associationJobService.MonitorAssociationByAI();
        }

        [HttpPost("MonitorAssociationByManual")]
        public async Task<bool> MonitorAssociationByManual()
        {
            return await associationJobService.MonitorAssociationByManual();
        }

        [HttpPost("MonitorCustClassification")]
        public async Task<bool> MonitorCustClassification()
        {
            return await custClassificationJobService.MonitorCustomerClassification();
        }
        
        [HttpPost("MonitorCustomerEvidence")]
        public async Task<bool> MonitorCustomerEvidence()
        {
            return await custEvidenceJobService.MonitorCustomerEvidence();
        }
    }
}
