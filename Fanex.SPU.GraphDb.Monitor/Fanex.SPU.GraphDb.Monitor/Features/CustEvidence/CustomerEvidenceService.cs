namespace Fanex.SPU.GraphDb.Monitor.Features.CustEvidence
{
    using Fanex.SPU.GraphDb.Monitor.Features.CustEvidence.Entity;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public interface ICustomerEvidenceService
    {
        IEnumerable<CustomerEvidenceModel> GetCustEvidenceFromMySQL(int custID);
        void InsertMissingCustEvidence(IEnumerable<CustomerEvidenceModel> missingCustEvidence);
        IEnumerable<CustomerEvidenceModel> GetMissingCustEvidence(IEnumerable<CustomerEvidenceModel> mysqlCustEvidence);
    }

    public class CustomerEvidenceService : ICustomerEvidenceService
    {
        private readonly ICustomerEvidenceDataAccess custEvidenceDataAccess;

        public CustomerEvidenceService(ICustomerEvidenceDataAccess custEvidenceDataAccess)
        {
            this.custEvidenceDataAccess = custEvidenceDataAccess;
        }
        
        public IEnumerable<CustomerEvidenceModel> GetCustEvidenceFromMySQL(int custID)
        {
            return custEvidenceDataAccess.GetCustEvidenceFromMySQL(custID, 500);
        }
        
        public IEnumerable<CustomerEvidenceModel> GetMissingCustEvidence(IEnumerable<CustomerEvidenceModel> mysqlCustEvidence)
        {
            return custEvidenceDataAccess.GetMissingCustEvidenceFromNeo4j(mysqlCustEvidence);
        }

        public void InsertMissingCustEvidence(IEnumerable<CustomerEvidenceModel> missingCustEvidence)
        {
            var custEvidenceJson = JsonConvert.SerializeObject(missingCustEvidence);
            custEvidenceDataAccess.InsertMissingCustEvidence(custEvidenceJson);
        }
    }
}
