using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCalendar.Application
{
    public interface IEventCategoryService
    {
        Task<IEnumerable<EventCategoryDTO>> GetAllCategories();
        Task<EventCategoryDTO> GetCategoryByID(int eventCategoryID);
        Task AddEventCategory(EventCategoryDTO dto);
        Task EditEventCategory(EventCategoryDTO dto);
        Task DeleteEventCategory(int eventCategoryID);
    }
}