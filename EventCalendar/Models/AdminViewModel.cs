using EventCalendar.Application;
using EventCalendar.Domain;
using System.Collections.Generic;

namespace EventCalendar.Models
{
    public class AdminViewModel
    {
        public IEnumerable<LogDTO> Logs { get; set; }
        public IEnumerable<ApplicationRole> Roles { get; set; }
        public IEnumerable<ContactDTO> Contacts { get; set; }
        public IEnumerable<EventCategoryDTO> Categories { get; set; }
        public List<UserViewModel> Users { get; set; }
        public UserViewModel User { get; set; }
        public EventCategoryDTO Category { get; set; }
        public ContactDTO Contact { get; set; }
        public ApplicationRole Role { get; set; }

        public AdminViewModel()
        {
            Users = new List<UserViewModel>();
        }
    }
}
