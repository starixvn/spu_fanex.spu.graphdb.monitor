namespace Fanex.SPU.GraphDb.Monitor.Features.Association.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class InsertMissingAssByManualCriteria : NonQueryCommand
    {
        [SpParam(SqlParamName = "ip_AssByManualList")]
        public string AssByManualListJson { get; set; }

        public override string GetSettingKey()
        {
            return "CTS_Log_Monitor_Neo4j_AssByManual_InsertMissing";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(AssByManualListJson);
        }
    }
}
