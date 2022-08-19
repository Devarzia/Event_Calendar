using EventCalendar.Application;
using System.Collections.Generic;

namespace EventCalendar.Models
{
    public class HomeViewModel
    {
        public List<ContactDTO> Contacts { get; set; }
        public ContactDTO Contact { get; set; }
    }
}
