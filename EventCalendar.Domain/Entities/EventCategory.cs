using System;

namespace EventCalendar.Domain
{
    public class EventCategory
    {
        public int EventCategoryID { get; set; }
        public string EventCategoryName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
