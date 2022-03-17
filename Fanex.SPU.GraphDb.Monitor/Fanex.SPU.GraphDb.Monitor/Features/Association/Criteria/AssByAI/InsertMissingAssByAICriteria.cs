namespace Fanex.SPU.GraphDb.Monitor.Features.Association.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class InsertMissingAssByAICriteria : NonQueryCommand
    {
        [SpParam(SqlParamName = "ip_AssByAIList")]
        public string AssByAIListJson { get; set; }

        public override string GetSettingKey()
        {
            return "CTS_Log_Monitor_Neo4j_AssByAI_InsertMissing";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(AssByAIListJson);
        }
    }
}
