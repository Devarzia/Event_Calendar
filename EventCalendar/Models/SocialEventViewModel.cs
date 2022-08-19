namespace EventCalendar.Models;
public class SocialEventViewModel
{
    public int SocialEventID { get; set; }
    public string Title { get; set; }
    public string Start { get; set; }
    public string End { get; set; }
    public bool AllDay { get; set; }
    public string Description { get; internal set; }
}