namespace Fanex.SPU.GraphDb.Monitor.Features.CTSCustomer.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;
    using System;

    public class GetCTSCustomerCriteria : CriteriaBase
    {
        [SpParam(SqlParamName = "ip_CTSCustID")]
        public int CTSCustID { get; set; }

        [SpParam(SqlParamName = "ip_BatchSize")]
        public int BatchSize { get; set; }

        public override string GetSettingKey()
        {
            return "CTS_Log_CTSCustomer_Get";
        }

        public override bool IsValid()
        {
            return CTSCustID >= 0;
        }
    }
}
