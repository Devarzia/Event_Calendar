using AutoMapper;
using EventCalendar.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCalendar.Application
{
    public class EventCategoryService : IEventCategoryService
    {
        protected readonly IRepository<EventCategory> _eventCategoryRepository;
        private readonly IMapper _mapper;

        public EventCategoryService(IRepository<EventCategory> eventCategoryRepository, IMapper mapper)
        {
            _eventCategoryRepository = eventCategoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventCategoryDTO>> GetAllCategories()
        {
            var eventCategories = await _eventCategoryRepository.GetAllEntities();
            var list = _mapper.Map<IEnumerable<EventCategoryDTO>>(eventCategories);
            return list;
        }

        public async Task<EventCategoryDTO> GetCategoryByID(int eventCategoryID)
        {
            var category = await _eventCategoryRepository.GetEntityByID(eventCategoryID);
            var dto = _mapper.Map<EventCategoryDTO>(category);
            return dto;
        }

        public async Task AddEventCategory(EventCategoryDTO dto)
        {
            var category = _mapper.Map<EventCategory>(dto);
            await _eventCategoryRepository.AddEntity(category);
        }

        public async Task EditEventCategory(EventCategoryDTO dto)
        {
            var category = await _eventCategoryRepository.GetEntityByID(dto.EventCategoryID);
            category.EventCategoryName = dto.EventCategoryName;
            await _eventCategoryRepository.EditEntity(category);
        }

        public async Task DeleteEventCategory(int eventCategoryID)
        {
            var category = await _eventCategoryRepository.GetEntityByID(eventCategoryID);
            await _eventCategoryRepository.DeleteEntity(category);
        }
    }
}
