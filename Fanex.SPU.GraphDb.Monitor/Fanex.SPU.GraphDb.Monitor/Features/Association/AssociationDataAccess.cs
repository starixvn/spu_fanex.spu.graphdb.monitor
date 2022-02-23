namespace Fanex.SPU.GraphDb.Monitor.Features.Association
{
    using Fanex.Data.Repository;
    using Fanex.SPU.GraphDb.Data;
    using Fanex.SPU.GraphDb.Monitor.Features.Association.Criteria;
    using Fanex.SPU.GraphDb.Monitor.Features.Association.Entity;
    using System.Collections.Generic;

    public interface IAssociationDataAccess
    {
        IEnumerable<AssByDeviceModel> GetMissingAssociationByDevice(IEnumerable<AssByDeviceModel> listAssByDevice);

        IEnumerable<AssByDeviceModel> GetAssByDeviceFromMySql(int lastCTSAssDevID, int batchSize);

        void InsertMissingAssByDevice(string assByDeviceJson);

        IEnumerable<AssByIpModel> GetMissingAssociationByIP(IEnumerable<AssByIpModel> listAssByIP);

        public void InsertMissingAssByIP(string assByIPJson);

        IEnumerable<AssByIpModel> GetAssByIPFromMySql(int lastIDAssByIP, int batchSize);
    }

    public class AssociationDataAccess : IAssociationDataAccess
    {
        private readonly IDynamicRepository repository;

        public AssociationDataAccess()
        {
            repository = new DynamicRepository();
        }

        #region AssociationByDevice
        public IEnumerable<AssByDeviceModel> GetAssByDeviceFromMySql(int lastCTSAssDevID, int batchSize)
        {
            var criteria = new GetAssByDeviceCriteria
            {
                CTSAssDevID = lastCTSAssDevID,
                BatchSize = batchSize
            };

            return repository.Fetch<AssByDeviceModel>(criteria);
        }

        public void InsertMissingAssByDevice(string assByDeviceJson)
        {
            var criteria = new InsertMissingAssByDeviceCriteria
            {
                AssByDeviceListJson = assByDeviceJson
            };

            repository.Execute(criteria);
        }

        public IEnumerable<AssByDeviceModel> GetMissingAssociationByDevice(IEnumerable<AssByDeviceModel> listAssByDevice)
        {

            var missingAssByIP = DbSessionFactory.Execute<AssByDeviceModel>("GetMissingAssociationByDevice", new
            {
                Ass = listAssByDevice
            }).Data;

            return missingAssByIP;
        }
        #endregion

        #region AssociationByIP
        public IEnumerable<AssByIpModel> GetAssByIPFromMySql(int lastIDAssByIP, int batchSize)
        {
            var criteria = new GetAssByIPCriteria
            {
                IDAssByIP = lastIDAssByIP,
                BatchSize = batchSize
            };

            return repository.Fetch<AssByIpModel>(criteria);
        }

        public void InsertMissingAssByIP(string assByIPJson)
        {
            var criteria = new InsertMissingAssByIPCriteria
            {
                AssByIPListJson = assByIPJson
            };

            repository.Execute(criteria);
        }

        public IEnumerable<AssByIpModel> GetMissingAssociationByIP(IEnumerable<AssByIpModel> listAssByIP)
        {

            var missingAssByIP = DbSessionFactory.Execute<AssByIpModel>("GetMissingAssociationByIP", new
            {
                Ass = listAssByIP
            }).Data;

            return missingAssByIP;
        }
        #endregion
    }
}
