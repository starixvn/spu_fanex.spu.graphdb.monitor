namespace Fanex.SPU.GraphDb.Monitor.JobService.Services
{
    using Fanex.SPU.GraphDb.Monitor.Features.SystemParameter;
    using static Fanex.SPU.GraphDb.Monitor.Shares.Consts;

    public interface ISystemParameterService
    {
        int GetValueFromParamID(SystemParameters paraID);

        void UpdateValueByParamID(int value, SystemParameters paraID);

    }

    public class SystemParameterService : ISystemParameterService
    {
        private readonly ISystemParameterDataAccess systemParameterDataAccess;

        public SystemParameterService(ISystemParameterDataAccess systemParameterDataAccess)
        {
            this.systemParameterDataAccess = systemParameterDataAccess;
        }

        public int GetValueFromParamID(SystemParameters paraID)
        {
            var value = systemParameterDataAccess.GetSystemParameterByID(paraID);
            return value != null ? int.Parse(value?.ParameterValue) : 0;
        }

        public void UpdateValueByParamID(int value, SystemParameters paraID)
        {
            systemParameterDataAccess.UpdateSystemParameter(value.ToString(), paraID);
        }
    }
}
