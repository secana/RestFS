using Microsoft.Extensions.Logging;

namespace RestFS.Console
{
    public static class LogEvents
    {
        // Info
        public static EventId ServiceStart = new EventId(2001, "SERVICE_START");

        // Trace
        public static EventId ReadInfo = new EventId(0001, "READ_INFO");
        public static EventId Read     = new EventId(0003, "READ");
        public static EventId Write    = new EventId(0004, "WRITE");
        public static EventId Delete   = new EventId(0005, "DELETE");

        // Warning
        public static EventId NotFound = new EventId(3001, "NOT_FOUND");
    }
}