﻿using AutoMapper;
using EventCalendar.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCalendar.Application
{
    public class ContactService : IContactService
    {
        private readonly IRepository<ContactInfo> _contactInfoRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public ContactService(IRepository<ContactInfo> contactInfoRepository, IMapper mapper, IEmailService emailService)
        {
            _contactInfoRepository = contactInfoRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<List<ContactDTO>> GetContactInformationList()
        {
            var contacts = await _contactInfoRepository.GetAllEntities();
            var list = _mapper.Map<List<ContactDTO>>(contacts);
            return list;
        }

        public async Task SubmitContactInformation(ContactDTO dto)
        {
            var contactInfo = _mapper.Map<ContactInfo>(dto);
            await _contactInfoRepository.AddEntity(contactInfo);
            await _emailService.SendContactFormSubmissionEmail(dto);
        }
    }
}
