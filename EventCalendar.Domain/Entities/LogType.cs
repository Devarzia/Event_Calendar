namespace EventCalendar.Domain
{
    public enum LogType
    {
        AddEvent = 1,
        EditEvent,
        DeleteEvent,
        EmailSent,
        Exception,
        UserLogin,
        UserLogout,
        AccountWarning
    }
}