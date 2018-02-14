using System;

namespace MaKro.Core.Logging.nsInterfaces
{
    public interface ILogger
    {
        LogStage LocalLogStage { get; }

        void Log(Exception aiException, LogStage aiStage);

        void Log(string aiMessage, LogStage aiStage);
    }
}
