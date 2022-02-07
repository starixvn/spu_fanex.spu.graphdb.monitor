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

        public MonitorNeo4jController(
            IMonitorCTSCustomerJobService ctsCustomerJobService)
        {
            this.ctsCustomerJobService = ctsCustomerJobService;
        }

        [HttpPost("MonitorCTSCustomerNode")]
        public async Task<bool> MonitorCTSCustomerNode()
        {
            return await ctsCustomerJobService.MonitorCTSCustomerNode();
        }
    }
}
