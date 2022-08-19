using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCalendar.Application
{
    public interface IContactService
    {
        Task<List<ContactDTO>> GetContactInformationList();
        Task SubmitContactInformation(ContactDTO dto);
    }
}
