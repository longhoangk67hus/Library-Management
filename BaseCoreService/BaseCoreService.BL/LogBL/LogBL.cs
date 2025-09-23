using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCoreService.BL
{
    using BaseCoreService.Entities;
    using Microsoft.Extensions.Configuration;
    using NLog;

    public class LogBL : ILogBL
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public LogBL(IConfiguration configuration)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _configuration = configuration;
        }

        public void LogError(string message, Exception ex = null)
        {
            if (ex == null)
            {
                _logger.Error(message);
            }
            else
            {
                _logger.Error(ex, message);
            }
        }


        public void LogDebug(string message)
        {
            var logEvent = CreateLogEventInfo(LogLevel.Debug, message);
            _logger.Log(logEvent);
        }

        public void LogInfo(string message)
        {
            var logEvent = CreateLogEventInfo(LogLevel.Info, message) ;
            _logger.Log(logEvent);
        }

        public void LogWarn(string message)
        {
            var logEvent = CreateLogEventInfo(LogLevel.Warn, message);
            _logger.Log(logEvent);
        }


        public void LogError(string message)
        {
            var logEvent = CreateLogEventInfo(LogLevel.Error, message);
            _logger.Log(logEvent);
        }



        private LogEventInfo CreateLogEventInfo(LogLevel level, string message)
        {
            var logEvent = new LogEventInfo()
            {
                Level = level,
                Properties =
                {
                    {"AppCode", _configuration["AppSettings:AppCode"]}
                },
                Message = message
            };
            return logEvent;
        }
    }

}
