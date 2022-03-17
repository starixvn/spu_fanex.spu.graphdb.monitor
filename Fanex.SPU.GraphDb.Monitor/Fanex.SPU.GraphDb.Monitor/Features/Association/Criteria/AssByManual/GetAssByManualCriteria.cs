namespace Fanex.SPU.GraphDb.Monitor.Features.Association.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetAssByManualCriteria : CriteriaBase
    {
        [SpParam(SqlParamName = "ip_AssID")]
        public int IDAssByManual { get; set; }

        [SpParam(SqlParamName = "ip_BatchSize")]
        public int BatchSize { get; set; }

        public override string GetSettingKey()
        {
            return "CTS_Log_Monitor_Neo4j_AssByManual_Get";
        }

        public override bool IsValid()
        {
            return IDAssByManual >= 0;
        }
    }
}
