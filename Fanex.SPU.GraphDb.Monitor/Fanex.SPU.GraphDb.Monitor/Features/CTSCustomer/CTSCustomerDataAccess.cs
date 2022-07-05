namespace Fanex.SPU.GraphDb.Monitor.Features.CTSCustomer
{
    using Fanex.Data.Repository;
    using Fanex.SPU.GraphDb.Data;
    using Fanex.SPU.GraphDb.Monitor.Features.CTSCustomer.Criteria;
    using Fanex.SPU.GraphDb.Monitor.Features.CTSCustomer.Entity;
    using Fanex.SPU.GraphDb.Monitor.Features.MonitorReplication;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface ICTSCustomerDataAccess
    {

        IEnumerable<CTSCustomerModel> GetCTSCustomerFromMySQL(int ctsCustId, int batchSize);

        void InsertMissingCust(string custDataJson, DateTime scannedDate);

        IEnumerable<CTSCustomerModel> GetCTSCustomerFromNeo4j(IEnumerable<CTSCustomerModel> mysqlCTSCusts);

        DbStatistic CheckSumAndCountTotal_MySQL(long maxCTSCustID, int maxCustID);

        long GetMaxCTSCustIDInNeo4j();

        DbStatistic CheckSumAndCountTotal_Neo4j(long maxCTSCustID);
    }

    public class CTSCustomerDataAccess : ICTSCustomerDataAccess
    {
        private readonly IDynamicRepository repository;

        public CTSCustomerDataAccess()
        {
            repository = new DynamicRepository();
        }

        public void InsertMissingCust(string custDataJson, DateTime scannedDate)
        {
            var criteria = new InsertMissingCustCriteria
            {
                CustomerListJson = custDataJson,
                ScannedDate = scannedDate
            };

            repository.Execute(criteria);
        }

        public IEnumerable<CTSCustomerModel> GetCTSCustomerFromMySQL(int ctsCustId, int batchSize)
        {
            var criteria = new GetCTSCustomerCriteria
            {
                CTSCustID = ctsCustId,
                BatchSize = batchSize
            };

            return repository.Fetch<CTSCustomerModel>(criteria);
        }
        
        public DbStatistic CheckSumAndCountTotal_MySQL(long maxCTSCustID, int maxCustID)
        {
            var criteria = new CheckSumAndCountTotalMySQLCriteria
            {
                MaxCTSCustID = maxCTSCustID,
                MaxCustID = maxCustID
            };

            return repository.Get<DbStatistic>(criteria);
        }


        public long GetMaxCTSCustIDInNeo4j()
        {
            var maxCTSCustID = DbSessionFactory.Execute<long>("GetMaxCTSCustID", null).Data.FirstOrDefault();

            return maxCTSCustID;
        }
        
        public DbStatistic CheckSumAndCountTotal_Neo4j(long maxCTSCustID)
        {
            var stats = DbSessionFactory.Execute<DbStatistic>("GetSumAndCountTotal_Neo4j", new { MaxCTSCustID = maxCTSCustID }).Data.FirstOrDefault();

            return stats;
        }
        
        public IEnumerable<CTSCustomerModel> GetCTSCustomerFromNeo4j(IEnumerable<CTSCustomerModel> mysqlCTSCusts)
        {
            var missingCusts = DbSessionFactory.Execute<CTSCustomerModel>("GetMissingCTSCustomer", new
            {
                ctsCustList = mysqlCTSCusts.Select(c =>c.CTSCustID)
            }).Data;

            return missingCusts;
        }
    }
}
