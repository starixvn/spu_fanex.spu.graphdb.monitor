namespace Fanex.SPU.GraphDb.Monitor.Features.CTSCustomer.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;
    using System;

    public class InsertMissingCustCriteria : NonQueryCommand
    {
        [SpParam(SqlParamName = "ip_CustomerList")]
        public string CustomerListJson { get; set; }

        [SpParam(SqlParamName = "ip_ScannedDate")]
        public DateTime ScannedDate { get; set; }

        public override string GetSettingKey()
        {
            return "CTS_Log_InsertMissingCusts";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(CustomerListJson);
        }
    }
}
