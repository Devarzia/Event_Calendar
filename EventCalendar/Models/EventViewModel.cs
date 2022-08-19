using EventCalendar.Application;
using System.Collections.Generic;

namespace EventCalendar.Models
{
    public class EventViewModel
    {
        public IEnumerable<SocialEventDTO> Events { get; set; }
        public IEnumerable<EventCategoryDTO> Categories { get; set; }
        public SocialEventDTO SocialEvent { get; set; }
    }
}
