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

        public MonitorNeo4jController(
            IMonitorCTSCustomerJobService ctsCustomerJobService,
            IMonitorAssociationJobService associationJobService,
            IMonitorCustClassificationJobService custClassificationJobService)
        {
            this.ctsCustomerJobService = ctsCustomerJobService;
            this.associationJobService = associationJobService;
            this.custClassificationJobService = custClassificationJobService;

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
    }
}
