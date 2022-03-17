namespace Fanex.SPU.GraphDb.Monitor.Features.CustClassification.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetCustomerClassificationCriteria : CriteriaBase
    {
        [SpParam(SqlParamName = "ip_CustID")]
        public int CustID { get; set; }

        [SpParam(SqlParamName = "ip_BatchSize")]
        public int BatchSize { get; set; }

        public override string GetSettingKey()
        {
            return "CTS_Log_Monitor_Neo4j_CustClassification_Get";
        }

        public override bool IsValid()
        {
            return CustID >= 0;
        }
    }
}
