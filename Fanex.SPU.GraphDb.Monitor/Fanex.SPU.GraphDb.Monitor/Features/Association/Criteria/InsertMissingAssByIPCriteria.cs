namespace Fanex.SPU.GraphDb.Monitor.Features.Association.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class InsertMissingAssByIPCriteria : NonQueryCommand
    {
        [SpParam(SqlParamName = "ip_AssByIPList")]
        public string AssByIPListJson { get; set; }

        public override string GetSettingKey()
        {
            return "CTS_Log_Monitor_Neo4j_AssByIP_InsertMissing";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(AssByIPListJson);
        }
    }
}
