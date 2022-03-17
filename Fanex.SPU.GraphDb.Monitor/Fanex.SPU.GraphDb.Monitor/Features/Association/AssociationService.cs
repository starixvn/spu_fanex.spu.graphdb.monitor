namespace Fanex.SPU.GraphDb.Monitor.Features.Association
{
    using Fanex.SPU.GraphDb.Monitor.Features.Association.Entity;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public interface IAssociationService
    {
        IEnumerable<AssByDeviceModel> GetAssociationByDeviceFromMySql(int lastCTSAssDevID);
        IEnumerable<AssByDeviceModel> GetMissingAssociationByDevice(IEnumerable<AssByDeviceModel> mysqlAssByDev);
        void InsertMissingAssByDevice(IEnumerable<AssByDeviceModel> missingAss);

        IEnumerable<AssByIpModel> GetMissingAssociationByIP(IEnumerable<AssByIpModel> mysqlAssByIP);
        IEnumerable<AssByIpModel> GetAssociationByIPFromMySql(int lastIDAssByIP);
        void InsertMissingAssByIP(IEnumerable<AssByIpModel> missingAss);

        void InsertMissingAssByAI(IEnumerable<AssByAIModel> missingAss);
        IEnumerable<AssByAIModel> GetMissingAssociationByAI(IEnumerable<AssByAIModel> mysqlAssByAI);
        IEnumerable<AssByAIModel> GetAssociationByAIFromMySql(int lastIDAssByAI);

        void InsertMissingAssByManual(IEnumerable<AssByManualModel> missingAss);
        IEnumerable<AssByManualModel> GetMissingAssociationByManual(IEnumerable<AssByManualModel> mysqlAssByManual);
        IEnumerable<AssByManualModel> GetAssociationByManualFromMySql(int lastIDAssByManual);

    }

    public class AssociationService : IAssociationService
    {
        private readonly IAssociationDataAccess associationDataAccess;

        public AssociationService(IAssociationDataAccess associationDataAccess)
        {
            this.associationDataAccess = associationDataAccess;
        }

        #region Association By Device

        public IEnumerable<AssByDeviceModel> GetAssociationByDeviceFromMySql(int lastCTSAssDevID)
        {
            return associationDataAccess.GetAssByDeviceFromMySql(lastCTSAssDevID, 500);
        }

        public IEnumerable<AssByDeviceModel> GetMissingAssociationByDevice(IEnumerable<AssByDeviceModel> mysqlAssByDev)
        {
            return associationDataAccess.GetMissingAssociationByDevice(mysqlAssByDev);
        }

        public void InsertMissingAssByDevice(IEnumerable<AssByDeviceModel> missingAss)
        {
            var assJson = JsonConvert.SerializeObject(missingAss);
            associationDataAccess.InsertMissingAssByDevice(assJson);
        }

        #endregion

        #region Association By IP

        public IEnumerable<AssByIpModel> GetAssociationByIPFromMySql(int lastIDAssByIP)
        {
            return associationDataAccess.GetAssByIPFromMySql(lastIDAssByIP, 500);
        }
        
        public IEnumerable<AssByIpModel> GetMissingAssociationByIP(IEnumerable<AssByIpModel> mysqlAssByIP)
        {
            return associationDataAccess.GetMissingAssociationByIP(mysqlAssByIP);
        }

        public void InsertMissingAssByIP(IEnumerable<AssByIpModel> missingAss)
        {
            var assJson = JsonConvert.SerializeObject(missingAss);
            associationDataAccess.InsertMissingAssByIP(assJson);
        }

        #endregion

        #region Association By AI
        public IEnumerable<AssByAIModel> GetAssociationByAIFromMySql(int lastIDAssByAI)
        {
            return associationDataAccess.GetAssByAIFromMySql(lastIDAssByAI, 500);
        }
        
        public IEnumerable<AssByAIModel> GetMissingAssociationByAI(IEnumerable<AssByAIModel> mysqlAssByAI)
        {
            return associationDataAccess.GetMissingAssociationByAI(mysqlAssByAI);
        }

        public void InsertMissingAssByAI(IEnumerable<AssByAIModel> missingAss)
        {
            var assJson = JsonConvert.SerializeObject(missingAss);
            associationDataAccess.InsertMissingAssByAI(assJson);
        }

        #endregion

        #region Association By Manual
        public IEnumerable<AssByManualModel> GetAssociationByManualFromMySql(int lastIDAssByManual)
        {
            return associationDataAccess.GetAssByManualFromMySql(lastIDAssByManual, 500);
        }
        
        public IEnumerable<AssByManualModel> GetMissingAssociationByManual(IEnumerable<AssByManualModel> mysqlAssByManual)
        {
            return associationDataAccess.GetMissingAssociationByManual(mysqlAssByManual);
        }

        public void InsertMissingAssByManual(IEnumerable<AssByManualModel> missingAss)
        {
            var assJson = JsonConvert.SerializeObject(missingAss);
            associationDataAccess.InsertMissingAssByManual(assJson);
        }

        #endregion
    }
}
