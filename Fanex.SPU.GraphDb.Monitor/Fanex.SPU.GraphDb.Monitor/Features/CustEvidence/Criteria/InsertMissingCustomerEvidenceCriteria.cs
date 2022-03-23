namespace Fanex.SPU.GraphDb.Monitor.Features.CustEvidence.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class InsertMissingCustomerEvidenceCriteria : NonQueryCommand
    {
        [SpParam(SqlParamName = "ip_CustEvidence")]
        public string CustEviListJson { get; set; }


        public override string GetSettingKey()
        {
            return "CTS_Log_Monitor_Neo4j_CustEvidence_InsertMissing";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(CustEviListJson);
        }
    }
}
