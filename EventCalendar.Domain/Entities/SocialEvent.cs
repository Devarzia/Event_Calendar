using System;

namespace EventCalendar.Domain;
public class SocialEvent
{
    public int SocialEventID { get; set; }
    public string SocialEventName { get; set; }
    public string SocialEventDescription { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
    public bool AllDay { get; set; }
    public int CategoryID { get; set; }
    public virtual EventCategory Category { get; set; }
    public int UserID { get; set; }
    public virtual ApplicationUser User { get; set; }
}