namespace Fanex.SPU.GraphDb.Monitor.JobService.Services
{
    using Fanex.NAP.SPU.Notification.Services;
    using Fanex.SPU.GraphDb.Monitor.Features.CTSCustomer;
    using Fanex.SPU.GraphDb.Monitor.Features.MonitorReplication;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using static Fanex.SPU.GraphDb.Monitor.Shares.Consts;

    public interface IMonitorCTSCustomerJobService
    {
        Task<bool> MonitorCTSCustomerNode();

        Task<bool> CheckDataConsistencyNeo4j();
    }

    public class MonitorCTSCustomerJobService : IMonitorCTSCustomerJobService
    {
        private readonly ICTSCustomerService ctscustomerService;

        public MonitorCTSCustomerJobService(ICTSCustomerService ctscustomerService)
        {
            this.ctscustomerService = ctscustomerService;
        }

        public Task<bool> MonitorCTSCustomerNode()
        {
            var lastScannedCTSCustID = ctscustomerService.GetLastScannedCTSCustID(SystemParameters.MonitorNeo4jCustomer_LastScannedCTSCustID);
            var mysqlCusts = ctscustomerService.GetCTSCustomerFromMySQL(lastScannedCTSCustID);
            if (mysqlCusts.Any() && mysqlCusts.Count() >= 500)
            {
                var missingCusts = ctscustomerService.GetMissingCTSCustomer(mysqlCusts);
                if (missingCusts.Any())
                {
                    //NotifyTelegramMessage(mess);
                    ctscustomerService.InsertMissingCTSCustomer(missingCusts, DateTime.Now);
                }

                var maxScannedCTSCustID = mysqlCusts.Max(c => c.CTSCustID);
                ctscustomerService.UpdateLastScannedCTSCustID(maxScannedCTSCustID, SystemParameters.MonitorNeo4jCustomer_LastScannedCTSCustID);
            }

            return Task.FromResult(true);
        }

        public Task<bool> CheckDataConsistencyNeo4j()
        {
            var maxCTSCustID = ctscustomerService.GetMaxCTSCustIDInNeo4j();
            var statsMySQL = ctscustomerService.CheckSumAndCountTotal_MySQL(maxCTSCustID, 0);
            var statsNeo4j = ctscustomerService.CheckSumAndCountTotal_Neo4j(maxCTSCustID);
            if (CheckDataConsistency(statsMySQL, statsNeo4j))
            {
                string mess = "Compare data MySQL vs Neo4j \n\r" +
                    "CTSCustomer count total: " + statsMySQL.CTSCustomerCountTotal + " vs " + statsNeo4j.CTSCustomerCountTotal + "\n\r" +
                    "CTSCustomer sum CTSCustID: " + statsMySQL.CTSCustomerSumValue + " vs " + statsNeo4j.CTSCustomerSumValue + "\n\r" +
                    "---------------- \n\r" +
                    "AssByDevice count total: " + statsMySQL.AssDevCountTotal + " vs " + statsNeo4j.AssDevCountTotal + "\n\r" +
                    "AssByDevice sum DeviceID: " + statsMySQL.AssDevSumDeviceID + " vs " + statsNeo4j.AssDevSumDeviceID + "\n\r" +
                    "AssByDevice sum CTSCustID: " + statsMySQL.AssDevSumCTSCustID + " vs " + statsNeo4j.AssDevSumCTSCustID + "\n\r" +
                    "---------------- \n\r" +
                    "AssByIP count total: " + statsMySQL.AssIPCountTotal + " vs " + statsNeo4j.AssIPCountTotal + "\n\r" +
                    "AssByIP sum FromCustID: " + statsMySQL.AssIPSumFromCustID + " vs " + statsNeo4j.AssIPSumFromCustID + "\n\r" +
                    "AssByIP sum ToCustID: " + statsMySQL.AssIPSumToCustID + " vs " + statsNeo4j.AssIPSumToCustID + "\n\r" +
                    "---------------- \n\r" +
                    "AssByAI count total: " + statsMySQL.AssAICountTotal + " vs " + statsNeo4j.AssAICountTotal + "\n\r" +
                    "AssByAI sum FromCustID: " + statsMySQL.AssAISumFromCustID + " vs " + statsNeo4j.AssAISumFromCustID + "\n\r" +
                    "AssByAI sum ToCustID: " + statsMySQL.AssAISumToCustID + " vs " + statsNeo4j.AssAISumToCustID + "\n\r" +
                    "---------------- \n\r" +
                    "AssGroupAI count total: " + statsMySQL.AssGroupAICountTotal + " vs " + statsNeo4j.AssGroupAICountTotal + "\n\r" +
                    "AssGroupAI sum CustID: " + statsMySQL.AssGroupAISumCustID + " vs " + statsNeo4j.AssGroupAISumCustID + "\n\r" +
                    "AssGroupAI sum GroupID: " + statsMySQL.AssGroupAISumGroupID + " vs " + statsNeo4j.AssGroupAISumGroupID + "\n\r" +
                    "---------------- \n\r" +
                    "AssManual count total: " + statsMySQL.AssManualCountTotal + " vs " + statsNeo4j.AssManualCountTotal + "\n\r" +
                    "AssManual sum FromCTSCustID: " + statsMySQL.AssManualSumFromCTSCustID + " vs " + statsNeo4j.AssManualSumFromCTSCustID + "\n\r" +
                    "AssManual sum ToCTSCustID: " + statsMySQL.AssManualSumToCTSCustID + " vs " + statsNeo4j.AssManualSumToCTSCustID + "\n\r" +
                    "---------------- \n\r" +
                    "AssByAI count total: " + statsMySQL.CustEvidenceCountTotal + " vs " + statsNeo4j.CustEvidenceCountTotal + "\n\r" +
                    "AssByAI sum CTSCustID: " + statsMySQL.CustEvidenceSumCTSCustID + " vs " + statsNeo4j.CustEvidenceSumCTSCustID + "\n\r" +
                    "AssByAI sum EviID: " + statsMySQL.CustEvidenceSumEviID + " vs " + statsNeo4j.CustEvidenceSumEviID + "\n\r" +
                    "---------------- \n\r"
                    ;

                SendMessageToTelegram(mess);
            }

            /*
             * 1. Get Max CTSCustID
             * 2. Count total and Check Sum from MySQL with MaxCTSCustID
             * 3. Count total and Check Sum from Neo4j with MaxCTSCustID
             * 4. Compare and notify
             */


            return Task.FromResult(true);
        }

        private void SendMessageToTelegram(string message)
        {
            var telegramService = new NotificationService();
            telegramService
               .Config("0A40aOc7CtpzjbBSJu8KNWQRoXyjRvf9", "-1001601670460", "https://bot.nexdev.net/skynex/api/messages/forwardwithtoken")
               .Message(new MessageModel("Neo4j - Check Data Consistency", DateTime.Now, "PRO", message)) //appSettings.EnvironmentString 
               .SendWithoutDelay(isInfo: true);
        }

        private bool CheckDataConsistency(DbStatistic statsMySQL, DbStatistic statsNeo4j)
        {
            var result = statsMySQL.CTSCustomerCountTotal != statsNeo4j.CTSCustomerCountTotal
                    || statsMySQL.CTSCustomerSumValue != statsNeo4j.CTSCustomerSumValue
                    || statsMySQL.AssDevCountTotal != statsNeo4j.AssDevCountTotal
                    || statsMySQL.AssDevSumCTSCustID != statsNeo4j.AssDevSumCTSCustID
                    || statsMySQL.AssDevSumDeviceID != statsNeo4j.AssDevSumDeviceID
                    || statsMySQL.AssIPCountTotal != statsNeo4j.AssIPCountTotal
                    || statsMySQL.AssIPSumFromCustID != statsNeo4j.AssIPSumFromCustID
                    || statsMySQL.AssIPSumToCustID != statsNeo4j.AssIPSumToCustID
                    || statsMySQL.AssAICountTotal != statsNeo4j.AssAICountTotal
                    || statsMySQL.AssAISumFromCustID != statsNeo4j.AssAISumFromCustID
                    || statsMySQL.AssAISumToCustID != statsNeo4j.AssAISumToCustID
                    || statsMySQL.AssGroupAICountTotal != statsNeo4j.AssGroupAICountTotal
                    || statsMySQL.AssGroupAISumCustID != statsNeo4j.AssGroupAISumCustID
                    || statsMySQL.AssGroupAISumGroupID != statsNeo4j.AssGroupAISumGroupID
                    || statsMySQL.AssManualCountTotal != statsNeo4j.AssManualCountTotal
                    || statsMySQL.AssManualSumFromCTSCustID != statsNeo4j.AssManualSumFromCTSCustID
                    || statsMySQL.AssManualSumToCTSCustID != statsNeo4j.AssManualSumToCTSCustID
                    || statsMySQL.CustEvidenceCountTotal != statsNeo4j.CustEvidenceCountTotal
                    || statsMySQL.CustEvidenceSumCTSCustID != statsNeo4j.CustEvidenceSumCTSCustID
                    || statsMySQL.CustEvidenceSumEviID != statsNeo4j.CustEvidenceSumEviID;

            return result;
        }
    }
}
