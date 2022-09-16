using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCalendar.Application
{
    public interface ILogService
    {
        Task<IEnumerable<LogDTO>> GetLogs();
        Task CreateAddLog(string message);
        Task CreateEditLog(string message);
        Task CreateDeleteLog(string message);
        Task CreateExceptionLog(string messsage);
        Task CreateUserLoginLog(string messsage);
        Task CreateUserLogoutLog(string messsage);
        Task CreateAccountWarningLog(string messsage);
    }
}
