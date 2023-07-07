using AutoMapper;
using EventCalendar.Domain;
using EventCalendar.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCalendar.Application
{
    public class SocialEventService : ISocialEventService
    {
        protected IRepository<SocialEvent> _socialEventRepository;
        protected IRepository<EventCategory> _categoryRepository;
        protected IMapper _mapper;
        protected ILogService _logService;

        public SocialEventService(IRepository<SocialEvent> socialEventRepository,
            IRepository<EventCategory> categoryRepository, ILogService logService, IMapper mapper)
        {
            _socialEventRepository = socialEventRepository;
            _categoryRepository = categoryRepository;
            _logService = logService;
            _mapper = mapper;
        }

        public async Task<List<SocialEventDTO>> GetAllSocialEvents()
        {
            var socialEvents = await _socialEventRepository?.GetAllEntities();
            var list = _mapper.Map<List<SocialEventDTO>>(socialEvents);

            foreach (var dto in list)
            {
                dto.DateDifference = GetDateDifference(dto.DateModified);
            }

            return list;

        }

        public async Task<List<SocialEventDTO>> SearchSocialEventsByName(string eventName)
        {
            var list = new List<SocialEventDTO>();
            var socialEvents = await _socialEventRepository.QueryEntitiesByParameter(x => x.SocialEventName == eventName);

            if (socialEvents.Any())
            {
                list = _mapper.Map<List<SocialEventDTO>>(socialEvents);
            }

            return list;
        }

        public async Task<List<SocialEventDTO>> GetSocialEventsForUser(int userID)
        {
            var list = new List<SocialEventDTO>();
            var socialEvents = await _socialEventRepository.QueryEntitiesByParameter(x => x.UserID == userID);

            if (socialEvents != null)
            {
                list = _mapper.Map<List<SocialEventDTO>>(socialEvents);
            }

            return list;
        }

        public async Task<SocialEventDTO> GetSocialEventByID(int socialEventID)
        {
            var socialEvent = await _socialEventRepository.GetEntityByID(socialEventID);
            var dto = _mapper.Map<SocialEventDTO>(socialEvent);
            return dto;

        }

        public async Task AddSocialEvent(SocialEventDTO dto)
        {
            dto.DateCreated = DateTime.Now;
            if (!dto.AllDay)
            {
                dto.DateModified = DateTime.Now;
            }
            var socialEvent = _mapper.Map<SocialEvent>(dto);
            await _socialEventRepository.AddEntity(socialEvent);
            await _logService.CreateAddLog($"{dto.SocialEventName} added");
            System.Diagnostics.Trace.TraceInformation($"{dto.User} added {dto.SocialEventName}");
        }

        public async Task EditSocialEvent(SocialEventDTO dto)
        {
            var socialEvent = await _socialEventRepository.GetEntityByID(dto.SocialEventID);
            dto.DateModified = DateTime.Now;
            var editedSocialEvent = _mapper.Map(dto, socialEvent);
            await _socialEventRepository.EditEntity(editedSocialEvent);
            await _logService.CreateEditLog($"{dto.SocialEventName} edited");
        }

        public async Task DeleteSocialEvent(int socialEventID)
        {
            var socialEvent = await _socialEventRepository.GetEntityByID(socialEventID);
            await _socialEventRepository.DeleteEntity(socialEvent);
            await _logService.CreateDeleteLog($"{socialEvent.SocialEventName} deleted");
        }

        private static string GetDateDifference(DateTime dateModified)
        {
            var difference = new DateDifference();
            var dateDifference = difference.GetDateDifference(dateModified);
            return dateDifference;
        }
    }
}