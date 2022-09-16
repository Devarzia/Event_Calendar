using AutoMapper;
using EventCalendar.Domain;

namespace EventCalendar.Application
{
    public class EventCalendarProfile : Profile
    {
        public EventCalendarProfile()
        {
            CreateMap<SocialEvent, SocialEventDTO>()
                .ForMember(x => x.User, x => x.MapFrom(x => (x.User != null) ? $"{x.User.FirstName} {x.User.LastName}" : ""))
                .ForMember(x => x.Category, x => x.MapFrom(x => (x.Category != null) ? x.Category.EventCategoryName : ""))
                .ReverseMap();

            CreateMap<EventCategory, EventCategoryDTO>().ReverseMap();

            CreateMap<Log, LogDTO>().ReverseMap();
        }
    }
}
