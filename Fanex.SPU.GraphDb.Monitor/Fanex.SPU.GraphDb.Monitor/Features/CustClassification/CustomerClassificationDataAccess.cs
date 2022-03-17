namespace Fanex.SPU.GraphDb.Monitor.Features.CustClassification
{
    using Fanex.Data.Repository;
    using Fanex.SPU.GraphDb.Data;
    using Fanex.SPU.GraphDb.Monitor.Features.CustClassification.Criteria;
    using Fanex.SPU.GraphDb.Monitor.Features.CustClassification.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICustomerClassificationDataAccess
    {

        IEnumerable<CustomerClassificationModel> GetCustClassificationFromMySQL(int custId, int batchSize);

        void InsertMissingCustClassification(string custClassificationDataJson);

        IEnumerable<CustomerClassificationModel> GetMissingCustClassificationFromNeo4j(IEnumerable<CustomerClassificationModel> listCustClassification);
    }


    public class CustomerClassificationDataAccess : ICustomerClassificationDataAccess
    {
        private readonly IDynamicRepository repository;

        public CustomerClassificationDataAccess()
        {
            repository = new DynamicRepository();
        }

        public void InsertMissingCustClassification(string custClassificationDataJson)
        {
            var criteria = new InsertMissingCustomerClassificationCriteria
            {
                CustClassificationListJson = custClassificationDataJson
            };

            repository.Execute(criteria);
        }

        public IEnumerable<CustomerClassificationModel> GetCustClassificationFromMySQL(int custId, int batchSize)
        {
            var criteria = new GetCustomerClassificationCriteria
            {
                CustID = custId,
                BatchSize = batchSize
            };

            return repository.Fetch<CustomerClassificationModel>(criteria);
        }

        public IEnumerable<CustomerClassificationModel> GetMissingCustClassificationFromNeo4j(IEnumerable<CustomerClassificationModel> listCustClassification)
        {
            var missingCustClassification = DbSessionFactory.Execute<CustomerClassificationModel>("GetMissingAssociationByDevice", new
            {
                CustClassification = listCustClassification
            }).Data;

            return missingCustClassification;
        }
    }
}
