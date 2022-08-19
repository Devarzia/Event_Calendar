using EventCalendar.Domain;
using System.Collections.Generic;
using System.Linq;

namespace EventCalendar.Models
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IList<string> Roles { get; set; }

    }
}
