namespace Fanex.SPU.GraphDb.Monitor.Features.CTSCustomer.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class CheckSumAndCountTotalMySQLCriteria : CriteriaBase
    {
        [SpParam(SqlParamName = "ip_MaxCTSCustID")]
        public long MaxCTSCustID { get; set; }
        
        [SpParam(SqlParamName = "ip_MaxCustID")]
        public int MaxCustID { get; set; }

        public override string GetSettingKey()
        {
            return "CTS_DC_CheckSumAndCount";
        }

        public override bool IsValid()
        {
            return MaxCTSCustID >= 0;
        }
    }
}
