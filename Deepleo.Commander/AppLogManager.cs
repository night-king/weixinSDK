using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deepleo.Commander
{
    public class AppLogManager
    {
        public static void Write(string message, bool isLogConsole = true)
        {
            if (isLogConsole)
            {
                Console.WriteLine(message);
            }
            LogWriter.Default.WriteWarning(message);
        }
    }
    /// <summary>
    /// Class to write logging data through Log4Net.
    /// </summary>
    public class LogWriter
    {
        private ILog _log4Net = null;
        private const string DEFAULT_LOGGER_NAME = "Logger";
        /// <summary>
        /// Prevents a default instance of the <see cref="LogWriter"/> class from being created.
        /// </summary>
        /// <param name="log4NetInstance">The log4net instance to be used.</param>
        private LogWriter(ILog log4NetInstance)
        {
            _log4Net = log4NetInstance;
        }

        /// <summary>
        /// Gets a logger with the specified configuration name.
        /// </summary>
        /// <param name="configName">Name of the logger in the configuration.</param>
        /// <returns>The logger obtained.</returns>
        /// <exception cref="System.Configuration.ConfigurationException">Thrown when no logger with the specified configuration name was found.</exception>
        public static LogWriter GetLogger(string configName)
        {
            var logger = LogManager.GetLogger(configName);
            if (logger == null)
            {
                throw new ArgumentException(string.Format("No logger configuration named '{0}' was found in the configuration.", configName), "configName");
            }
            return new LogWriter(logger);
        }

        /// <summary>
        /// Gets the default.
        /// </summary>
        public static LogWriter Default
        {
            get
            {
                return GetLogger(DEFAULT_LOGGER_NAME);
            }
        }

        /// <summary>
        /// Writes an information level logging message.
        /// </summary>
        /// <param name="message">The message to be written.</param>
        public void WriteInfo(object message)
        {
            _log4Net.Info(message);
        }

        /// <summary>
        /// Writes a warning level logging message.
        /// </summary>
        /// <param name="message">The message to be written.</param>
        public void WriteWarning(object message)
        {
            _log4Net.Warn(message);
        }

        /// <summary>
        /// Writes a warning level logging message.
        /// </summary>
        /// <param name="message">The message to be written.</param>
        /// <param name="exception">The exception.</param>
        public void WriteWarning(object message, System.Exception exception)
        {
            _log4Net.Warn(message, exception);
        }

        /// <summary>
        /// Writes the error.
        /// </summary>
        /// <param name="message">The message to be written.</param>
        public void WriteError(object message)
        {
            _log4Net.Error(message);
        }

        /// <summary>
        /// Writes the error level logging message..
        /// </summary>
        /// <param name="message">The message to be written.</param>
        /// <param name="exception">The exception.</param>
        public void WriteError(object message, System.Exception exception)
        {
            _log4Net.Error(message, exception);
        }

        /// <summary>
        /// Writes the fatal error level logging message..
        /// </summary>
        /// <param name="message">The message to be written.</param>
        public void WriteFatal(object message)
        {
            _log4Net.Fatal(message);
        }

        /// <summary>
        /// Writes the fatal error level logging message..
        /// </summary>
        /// <param name="message">The message to be written.</param>
        /// <param name="exception">The exception.</param>
        public void WriteFatal(object message, System.Exception exception)
        {
            _log4Net.Fatal(message, exception);
        }

    }
}
