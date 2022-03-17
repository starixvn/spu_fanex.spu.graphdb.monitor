namespace Fanex.SPU.GraphDb.Monitor.Features.Association.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetAssByAICriteria : CriteriaBase
    {
        [SpParam(SqlParamName = "ip_AssID")]
        public int IDAssByAI { get; set; }

        [SpParam(SqlParamName = "ip_BatchSize")]
        public int BatchSize { get; set; }

        public override string GetSettingKey()
        {
            return "CTS_Log_Monitor_Neo4j_AssByAI_Get";
        }

        public override bool IsValid()
        {
            return IDAssByAI >= 0;
        }
    }
}
