namespace Fanex.SPU.GraphDb.Monitor.Features.Association.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class InsertMissingAssByDeviceCriteria : NonQueryCommand
    {
        [SpParam(SqlParamName = "ip_AssByDeviceList")]
        public string AssByDeviceListJson { get; set; }

        public override string GetSettingKey()
        {
            return "CTS_Log_Monitor_Neo4j_AssByDevice_InsertMissing";
        }

        public override bool IsValid()
        {
            return !string.IsNullOrEmpty(AssByDeviceListJson);
        }
    }
}
