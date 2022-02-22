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

    }

    public class AssociationService : IAssociationService
    {
        private readonly IAssociationDataAccess associationDataAccess;

        public AssociationService(IAssociationDataAccess associationDataAccess)
        {
            this.associationDataAccess = associationDataAccess;
        }

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
    }
}
