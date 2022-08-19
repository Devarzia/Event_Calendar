using System;

namespace EventCalendar.Domain
{
    public class Log
    {
        public int LogID { get; set; }
        public string LogDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public LogType LogType { get; set; }
    }
}
