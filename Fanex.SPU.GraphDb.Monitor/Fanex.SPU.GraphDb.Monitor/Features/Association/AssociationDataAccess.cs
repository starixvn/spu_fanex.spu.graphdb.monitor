namespace Fanex.SPU.GraphDb.Monitor.Features.Association
{
    using Fanex.Data.Repository;
    using Fanex.SPU.GraphDb.Data;
    using Fanex.SPU.GraphDb.Monitor.Features.CTSCustomer.Criteria;
    using Fanex.SPU.GraphDb.Monitor.Features.CTSCustomer.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using Fanex.SPU.GraphDb.Monitor.Features.Association.Criteria;
    using Fanex.SPU.GraphDb.Monitor.Features.Association.Entity;

    public interface IAssociationDataAccess
    {
        IEnumerable<AssByDeviceModel> GetMissingAssociationByDevice(IEnumerable<AssByDeviceModel> listAssByDevice);

        IEnumerable<AssByDeviceModel> GetAssByDeviceFromMySql(int lastCTSAssDevID, int batchSize);

        void InsertMissingAssByDevice(string assByDeviceJson);
    }

    public class AssociationDataAccess : IAssociationDataAccess
    {
        private readonly IDynamicRepository repository;

        public AssociationDataAccess()
        {
            repository = new DynamicRepository();
        }

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

            var missingCusts = DbSessionFactory.Execute<AssByDeviceModel>("GetMissingAssociationByDevice", new
            {
                Ass = listAssByDevice
            }).Data;

            return missingCusts;
        }
    }
}
