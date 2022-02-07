namespace Fanex.SPU.GraphDb.Monitor.Features.SystemParameter.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetSystemParameterCriteria : CriteriaBase
    {
        [SpParam(SqlParamName = "ip_ParamID")]
        public int ParamID { get; set; }

        public override string GetSettingKey()
               => "CTS_DC_SystemParameter_Get";

        public override bool IsValid()
            => true;
    }
}
