using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCalendar.Application
{
    public interface ISocialEventService
    {
        Task<List<SocialEventDTO>> GetAllSocialEvents();
        Task<List<SocialEventDTO>> SearchSocialEventsByName(string eventName);
        Task<List<SocialEventDTO>> GetSocialEventsForUser(int userID);
        Task<SocialEventDTO> GetSocialEventByID(int socialEventID);
        Task AddSocialEvent(SocialEventDTO dto);
        Task EditSocialEvent(SocialEventDTO dto);
        Task DeleteSocialEvent(int socialEventDTO);
    }
}
