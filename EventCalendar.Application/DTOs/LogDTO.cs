using EventCalendar.Domain;
using System;

namespace EventCalendar.Application
{
    public class LogDTO
    {
        public int LogID { get; set; }
        public string LogDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public string LogType { get; set; }
    }
}
