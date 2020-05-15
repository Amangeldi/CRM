using CRM.BLL.DTO;
using CRM.BLL.Interfaces;
using CRM.DAL.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL.Services
{
    public class LogTemp
    {
        ILogService _logService;
        public LogTemp(ILogService logService)
        {
            _logService = logService;
        }
        public IEnumerable<LogDTO> logs { get; set; }
        public async Task UpdateLogs()
        {
            logs = await _logService.GetLogs();
        }
    }
}
