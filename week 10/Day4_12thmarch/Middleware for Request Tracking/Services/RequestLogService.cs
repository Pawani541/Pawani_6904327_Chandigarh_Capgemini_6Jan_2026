using Middleware_for_Request_Tracking.Models;

namespace Middleware_for_Request_Tracking.Services
{
    public class RequestLogService : IRequestLogService
    {
        private List<RequestLog> logs = new List<RequestLog>();

        public void AddLog(RequestLog log)
        {
            logs.Add(log);
        }

        public List<RequestLog> GetLogs()
        {
            return logs;
        }
    }
}