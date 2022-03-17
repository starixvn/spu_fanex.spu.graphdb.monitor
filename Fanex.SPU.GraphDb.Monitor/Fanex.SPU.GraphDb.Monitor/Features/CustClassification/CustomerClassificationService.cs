namespace Fanex.SPU.GraphDb.Monitor.Features.CustClassification
{
    using Fanex.SPU.GraphDb.Monitor.Features.CustClassification.Entity;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public interface ICustomerClassificationService
    {
        IEnumerable<CustomerClassificationModel> GetCustClassificationFromMySQL(int custID);
        void InsertMissingCustClassification(IEnumerable<CustomerClassificationModel> missingCustClassification);
        IEnumerable<CustomerClassificationModel> GetMissingCustClassification(IEnumerable<CustomerClassificationModel> mysqlCustClassification);
    }

    public class CustomerClassificationService : ICustomerClassificationService
    {
        private readonly ICustomerClassificationDataAccess custClassificationDataAccess;

        public CustomerClassificationService(ICustomerClassificationDataAccess custClassificationDataAccess)
        {
            this.custClassificationDataAccess = custClassificationDataAccess;
        }
        
        public IEnumerable<CustomerClassificationModel> GetCustClassificationFromMySQL(int custID)
        {
            return custClassificationDataAccess.GetCustClassificationFromMySQL(custID, 500);
        }
        
        public IEnumerable<CustomerClassificationModel> GetMissingCustClassification(IEnumerable<CustomerClassificationModel> mysqlCustClassification)
        {
            return custClassificationDataAccess.GetMissingCustClassificationFromNeo4j(mysqlCustClassification);
        }

        public void InsertMissingCustClassification(IEnumerable<CustomerClassificationModel> missingCustClassification)
        {
            var classificationJson = JsonConvert.SerializeObject(missingCustClassification);
            custClassificationDataAccess.InsertMissingCustClassification(classificationJson);
        }
    }
}
