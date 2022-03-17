namespace Fanex.SPU.GraphDb.Monitor.Shares
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Consts
    {
        public enum SystemParameters
        {
            MonitorNeo4jCustomer_LastScannedCTSCustID = 68,
            MonitorNeo4jCustomer_LastScannedCTSAssDevID = 71,
            MonitorNeo4jCustomer_LastScannedIDAssByIP = 72,
            MonitorNeo4jCustomer_LastScannedIDAssByAI = 73,
            MonitorNeo4jCustomer_LastScannedIDAssByManual = 74,
            MonitorNeo4jClassification_LastScannedCustID = 75
        };
    }
}
