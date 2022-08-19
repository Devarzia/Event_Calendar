using System;

namespace EventCalendar.Infrastructure
{
    public class DateDifference
    {
        public string GetDateDifference(DateTime dateModified)
        {
            var now = DateTime.Now;

            var time = (now - dateModified);

            if (time.Days >= 60)
            {
                return dateModified.ToShortDateString();
            }
            else if (time.Days >= 30 && time.Days < 60)
            {
                return "1 month ago";
            }
            else if (time.Days >= 28 && time.Days < 30)
            {
                return $"4 weeks ago";
            }
            else if (time.Days >= 21 && time.Days < 28)
            {
                return "3 weeks ago";
            }
            else if (time.Days >= 14 && time.Days < 21)
            {
                return "2 weeks ago";
            }
            else if (time.Days >= 7 && time.Days < 14)
            {
                return "1 week ago";
            }
            else if (time.Days >= 2 && time.Days < 7)
            {
                return $"{time.Days} days ago";
            }
            else if (time.Days == 1)
            {
                return $"{time.Days} day ago";
            }
            else if (time.Hours <= 23 && time.Hours > 1)
            {
                return $"{time.Hours} hours ago";
            }
            else if (time.Hours == 1)
            {
                return $"{time.Hours} hour ago";
            }
            else if (time.Minutes <= 59 && time.Minutes > 0)
            {
                return $"{time.Minutes} minutes ago";
            }
            else
            {
                return "moments ago";
            }
        }
    }
}
