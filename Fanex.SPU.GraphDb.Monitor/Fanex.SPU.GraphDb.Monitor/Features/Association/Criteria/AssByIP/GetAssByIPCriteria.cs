namespace Fanex.SPU.GraphDb.Monitor.Features.Association.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetAssByIPCriteria : CriteriaBase
    {
        [SpParam(SqlParamName = "ip_IDAssByIP")]
        public int IDAssByIP { get; set; }

        [SpParam(SqlParamName = "ip_BatchSize")]
        public int BatchSize { get; set; }

        public override string GetSettingKey()
        {
            return "CTS_Log_Monitor_Neo4j_AssByIP_Get";
        }

        public override bool IsValid()
        {
            return IDAssByIP >= 0;
        }
    }
}
