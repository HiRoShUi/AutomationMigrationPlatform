using MaKro.Core.Logging;
using MaKro.Core.Logging.nsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Makro.Core.ModuleSystem.HelpClasses
{
    public static class ModuleLogger    {

        private static ILogger _logger;
        public static void Init(ILogger aiLogger)
        {
            _logger = aiLogger;
        }

        public static ILogger Logger
        {
            get
            {
                if (_logger == null)
                    _logger = new FileLogger(LogStage.DEBUG);//if no logger was initialized a default debug-logger will be used. //Maybe smtg i have to think about later.
                return _logger;
            }
        }

    }
}
