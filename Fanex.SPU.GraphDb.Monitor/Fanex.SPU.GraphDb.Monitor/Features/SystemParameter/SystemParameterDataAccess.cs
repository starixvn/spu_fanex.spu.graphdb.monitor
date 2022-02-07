namespace Fanex.SPU.GraphDb.Monitor.Features.SystemParameter
{
    using Fanex.Data.Repository;
    using Fanex.SPU.GraphDb.Monitor.Features.SystemParameter.Criteria;
    using System.Linq;
    using static Fanex.SPU.GraphDb.Monitor.Shares.Consts;

    public interface ISystemParameterDataAccess
    {
        SystemParameterEntity GetSystemParameterByID(SystemParameters paramID);

        void UpdateSystemParameter(string paraValue, SystemParameters paramId);
    }

    public class SystemParameterDataAccess : ISystemParameterDataAccess
    {
        private readonly IDynamicRepository repository;

        public SystemParameterDataAccess()
        {
            repository = new DynamicRepository();
        }

        public SystemParameterEntity GetSystemParameterByID(SystemParameters paramID)
        {
            var criteria = new GetSystemParameterCriteria
            {
                ParamID = (int)paramID
            };

            return repository.Fetch<SystemParameterEntity>(criteria).FirstOrDefault();
        }

        public void UpdateSystemParameter(string paraValue, SystemParameters paramId)
        {
            var criteria = new UpdateSystemParameterCriteria
            {
                ParamId = (int)paramId,
                ParamValue = paraValue
            };

            repository.Execute(criteria);
        }
    }
}
