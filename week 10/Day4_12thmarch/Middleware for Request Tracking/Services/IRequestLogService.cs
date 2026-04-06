using Middleware_for_Request_Tracking.Models;

namespace Middleware_for_Request_Tracking.Services
{
    public interface IRequestLogService
    {
        void AddLog(RequestLog log);
        List<RequestLog> GetLogs();
    }
}