namespace Fanex.SPU.GraphDb.Monitor.Features.Association.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetAssByDeviceCriteria : CriteriaBase
    {
        [SpParam(SqlParamName = "ip_CTSAssDevID")]
        public int CTSAssDevID { get; set; }

        [SpParam(SqlParamName = "ip_BatchSize")]
        public int BatchSize { get; set; }

        public override string GetSettingKey()
        {
            return "CTS_Log_Monitor_Neo4j_AssByDevice_Get";
        }

        public override bool IsValid()
        {
            return CTSAssDevID >= 0;
        }
    }
}
