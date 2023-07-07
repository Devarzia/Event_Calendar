using EventCalendar.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCalendar.Application
{
    public class LogService : ILogService
    {
        protected IRepository<Log> _logRepository;

        public LogService(IRepository<Log> logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<IEnumerable<LogDTO>> GetLogs()
        {
            var list = new List<LogDTO>();
            var logs = await _logRepository.GetAllEntities();
            foreach (var log in logs)
            {
                var dto = new LogDTO
                {
                    DateCreated = log.DateCreated,
                    LogDescription = log.LogDescription,
                    LogID = log.LogID,
                    LogType = Enum.GetName(typeof(LogType), log.LogType)
                };

                list.Add(dto);
            }

            return list;
        }

        public async Task CreateAddLog(string message)
        {
            var log = new Log
            {
                LogType = LogType.AddEvent,
                LogDescription = message,
                DateCreated = DateTime.Now,
            };

            await _logRepository.AddEntity(log);
        }

        public async Task CreateEditLog(string message)
        {
            var log = new Log
            {
                LogType = LogType.EditEvent,
                LogDescription = message,
                DateCreated = DateTime.Now,
            };

            await _logRepository.AddEntity(log);
        }

        public async Task CreateDeleteLog(string message)
        {
            var log = new Log
            {
                LogType = LogType.DeleteEvent,
                LogDescription = message,
                DateCreated = DateTime.Now,
            };

            await _logRepository.AddEntity(log);
        }

        public async Task CreateExceptionLog(string message)
        {
            var log = new Log
            {
                LogType = LogType.Exception,
                LogDescription = message,
                DateCreated = DateTime.Now,
            };

            await _logRepository.AddEntity(log);
        }

        public async Task CreateAccountWarningLog(string message)
        {
            var log = new Log
            {
                LogType = LogType.AccountWarning,
                LogDescription = message,
                DateCreated = DateTime.Now,
            };

            await _logRepository.AddEntity(log);
        }

        public async Task CreateUserLoginLog(string message)
        {
            var log = new Log
            {
                LogType = LogType.UserLogin,
                LogDescription = message,
                DateCreated = DateTime.Now,
            };

            await _logRepository.AddEntity(log);
        }

        public async Task CreateUserLogoutLog(string message)
        {
            var log = new Log
            {
                LogType = LogType.UserLogout,
                LogDescription = message,
                DateCreated = DateTime.Now,
            };

            await _logRepository.AddEntity(log);
        }
    }
}
