using System;
using System.ComponentModel.DataAnnotations;

namespace EventCalendar.Application
{
    public class SocialEventDTO
    {
        public int SocialEventID { get; set; }
        [Required(ErrorMessage = "{0} is required"), Display(Name = "Event Name"), StringLength(50)]
        public string SocialEventName { get; set; }
        [Required(ErrorMessage = "{0} is required"), Display(Name = "Event Description"), StringLength(150)]
        public string SocialEventDescription { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [Required]
        public int UserID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool AllDay { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string DateDifference { get; set; }
        public string Category { get; set; }
        public string User { get; set; }
    }
}
