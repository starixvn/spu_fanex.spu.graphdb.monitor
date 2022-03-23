namespace Fanex.SPU.GraphDb.Monitor.Features.CustEvidence
{
    using Fanex.Data.Repository;
    using Fanex.SPU.GraphDb.Data;
    using Fanex.SPU.GraphDb.Monitor.Features.CustEvidence.Criteria;
    using Fanex.SPU.GraphDb.Monitor.Features.CustEvidence.Entity;
    using System.Collections.Generic;

    public interface ICustomerEvidenceDataAccess
    {

        IEnumerable<CustomerEvidenceModel> GetCustEvidenceFromMySQL(int custId, int batchSize);

        void InsertMissingCustEvidence(string custEvidenceDataJson);

        IEnumerable<CustomerEvidenceModel> GetMissingCustEvidenceFromNeo4j(IEnumerable<CustomerEvidenceModel> listCustEvidence);
    }


    public class CustomerEvidenceDataAccess : ICustomerEvidenceDataAccess
    {
        private readonly IDynamicRepository repository;

        public CustomerEvidenceDataAccess()
        {
            repository = new DynamicRepository();
        }

        public void InsertMissingCustEvidence(string custEvidenceDataJson)
        {
            var criteria = new InsertMissingCustomerEvidenceCriteria
            {
                CustEviListJson = custEvidenceDataJson
            };

            repository.Execute(criteria);
        }

        public IEnumerable<CustomerEvidenceModel> GetCustEvidenceFromMySQL(int custEvidID, int batchSize)
        {
            var criteria = new GetCustomerEvidenceCriteria
            {
                CustEvidID = custEvidID,
                BatchSize = batchSize
            };

            return repository.Fetch<CustomerEvidenceModel>(criteria);
        }

        public IEnumerable<CustomerEvidenceModel> GetMissingCustEvidenceFromNeo4j(IEnumerable<CustomerEvidenceModel> listCustEvidence)
        {
            var missingCustEvidence = DbSessionFactory.Execute<CustomerEvidenceModel>("GetMissingCustEvidence", new
            {
                CustEvidence = listCustEvidence
            }).Data;

            return missingCustEvidence;
        }
    }
}
