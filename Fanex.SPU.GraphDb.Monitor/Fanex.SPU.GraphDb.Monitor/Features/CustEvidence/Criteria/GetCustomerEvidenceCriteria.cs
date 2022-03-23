namespace Fanex.SPU.GraphDb.Monitor.Features.CustEvidence.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetCustomerEvidenceCriteria : CriteriaBase
    {
        [SpParam(SqlParamName = "ip_CustEvidID")]
        public int CustEvidID { get; set; }

        [SpParam(SqlParamName = "ip_BatchSize")]
        public int BatchSize { get; set; }

        public override string GetSettingKey()
        {
            return "CTS_Log_Monitor_Neo4j_CustEvidence_Get";
        }

        public override bool IsValid()
        {
            return CustEvidID >= 0;
        }
    }
}
