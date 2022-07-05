
namespace Fanex.SPU.GraphDb.Monitor.Features.MonitorReplication
{
    public class DbStatistic
    {
        public long CTSCustomerCountTotal { get; set; }
        public long CTSCustomerSumValue { get; set; }
        public long AssDevCountTotal { get; set; }
        public long AssDevSumCTSCustID { get; set; }
        public long AssDevSumDeviceID { get; set; }
        public long AssIPCountTotal { get; set; }
        public long AssIPSumFromCustID { get; set; }
        public long AssIPSumToCustID { get; set; }
        public long AssAICountTotal { get; set; }
        public long AssAISumFromCustID { get; set; }
        public long AssAISumToCustID { get; set; }
        public long AssGroupAICountTotal { get; set; }
        public long AssGroupAISumCustID { get; set; }
        public long AssGroupAISumGroupID { get; set; }
        public long AssManualCountTotal { get; set; }
        public long AssManualSumFromCTSCustID { get; set; }
        public long AssManualSumToCTSCustID { get; set; }
        public long CustEvidenceCountTotal { get; set; }
        public long CustEvidenceSumCTSCustID { get; set; }
        public long CustEvidenceSumEviID { get; set; }
    }
}
