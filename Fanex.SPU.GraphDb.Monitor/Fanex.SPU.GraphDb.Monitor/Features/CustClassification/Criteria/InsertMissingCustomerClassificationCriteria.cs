namespace Fanex.SPU.GraphDb.Monitor.Features.CustClassification.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class InsertMissingCustomerClassificationCriteria : NonQueryCommand
    {
        [SpParam(SqlParamName = "ip_CustClassification")]
        public string CustClassificationListJson { get; set; }


        public override string GetSettingKey()
        {
            return "CTS_Log_Monitor_Neo4j_CustClassification_InsertMissing";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(CustClassificationListJson);
        }
    }
}
