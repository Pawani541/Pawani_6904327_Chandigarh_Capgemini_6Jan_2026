namespace Middleware_for_Request_Tracking.Models
{
    public class RequestLog
    {
        public string? Url { get; set; }

        public long ExecutionTime { get; set; }
    }
}