namespace Fanex.SPU.GraphDb.Monitor.Features.CTSCustomer
{
    using Fanex.SPU.GraphDb.Monitor.Features.CTSCustomer.Entity;
    using Fanex.SPU.GraphDb.Monitor.Features.SystemParameter;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static Fanex.SPU.GraphDb.Monitor.Shares.Consts;

    public interface ICTSCustomerService
    {
        int GetLastScannedCTSCustID(SystemParameters paraID);

        void UpdateLastScannedCTSCustID(int ctsCustID, SystemParameters paraID);

        IEnumerable<CTSCustomerModel> GetCTSCustomerFromMySQL(int lastScannedCTSCustID);

        IEnumerable<CTSCustomerModel> GetMissingCTSCustomer(IEnumerable<CTSCustomerModel> mysqlCTSCustList);

        void InsertMissingCTSCustomer(IEnumerable<CTSCustomerModel> missingCusts, DateTime scannedDate);

    }

    public class CTSCustomerService : ICTSCustomerService
    {
        private readonly ICTSCustomerDataAccess ctsCustomerdataAccess;
        private readonly ISystemParameterDataAccess systemParameterDataAccess;

        public CTSCustomerService(ICTSCustomerDataAccess ctsCustomerdataAccess,
            ISystemParameterDataAccess systemParameterDataAccess)
        {
            this.ctsCustomerdataAccess = ctsCustomerdataAccess;
            this.systemParameterDataAccess = systemParameterDataAccess;            
        }

        public int GetLastScannedCTSCustID(SystemParameters paraID)
        {
            var lastScannedCTSCustomerID = systemParameterDataAccess.GetSystemParameterByID(paraID);
            return lastScannedCTSCustomerID != null ? int.Parse(lastScannedCTSCustomerID?.ParameterValue) : 0;
        }

        public void UpdateLastScannedCTSCustID(int ctsCustID, SystemParameters paraID)
        {
            systemParameterDataAccess.UpdateSystemParameter(ctsCustID.ToString(), paraID);
        }

        public IEnumerable<CTSCustomerModel> GetCTSCustomerFromMySQL(int lastScannedCTSCustID)
        {
            return ctsCustomerdataAccess.GetCTSCustomerFromMySQL(lastScannedCTSCustID, 500);
        }

        public IEnumerable<CTSCustomerModel> GetMissingCTSCustomer(IEnumerable<CTSCustomerModel> mysqlCTSCustList)
        {

            return ctsCustomerdataAccess.GetCTSCustomerFromNeo4j(mysqlCTSCustList);
        }
        
        public void InsertMissingCTSCustomer(IEnumerable<CTSCustomerModel> missingCusts, DateTime scannedDate)
        {
            var customerJson = JsonConvert.SerializeObject(missingCusts);
            ctsCustomerdataAccess.InsertMissingCust(customerJson, scannedDate);
        }
    }
}
