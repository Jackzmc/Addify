using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Addify.lib
{
    public class Logger
    {
        private const string FILE_NAME = "Addify.log";
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss.fff";
        public string Name { get; set; }
        public Logger(String name)
        {
            this.Name = name;
            Init();
        }
        public Logger()
        {
            Init();
        }

        #region public
        /// <summary>
        /// Log a DEBUG message
        /// </summary>
        /// <param name="text">Message</param>
        public void Debug(string text)
        {
        #if DEBUG
            WriteFormattedLog(LogLevel.DEBUG, text);
        #endif
        }

        /// <summary>
        /// Log an ERROR message
        /// </summary>
        /// <param name="text">Message</param>
        public void Error(string text)
        {
            WriteFormattedLog(LogLevel.ERROR, text);
        }
        /// <summary>
        /// Log an ERROR message
        /// </summary>
        /// <param name="exception">Exception</param>
        public void Error(Exception exception)
        {
            WriteFormattedLog(LogLevel.ERROR, exception.Message + "\n" + exception.StackTrace);
        }
        /// <summary>
        /// Log an ERROR message
        /// </summary>
        /// <param name="message">Prefix Message</param>
        /// <param name="exception">Exception</param>
        public void Error(String message,Exception exception)
        {
            WriteFormattedLog(LogLevel.ERROR, message + "\n" + exception.Message + "\n" + exception.StackTrace);
        }

        /// <summary>
        /// Log a FATAL ERROR message
        /// </summary>
        /// <param name="text">Message</param>
        public void Fatal(string text)
        {
            WriteFormattedLog(LogLevel.FATAL, text);
        }
        /// <summary>
        /// Log a FATAL ERROR message
        /// </summary>
        /// <param name="exception">Exception</param>
        public void Fatal(Exception exception)
        {
            WriteFormattedLog(LogLevel.FATAL, exception.Message + "\n" + exception.StackTrace);
        }

        /// <summary>
        /// Log a FATAL ERROR message
        /// </summary>
        /// <param name="message">Prefix Message</param>
        /// <param name="exception">Exception</param>
        public void Fatal(String message, Exception exception)
        {
            WriteFormattedLog(LogLevel.FATAL, message + "\n" + exception.Message + "\n" + exception.StackTrace);
        }

        /// <summary>
        /// Log an INFO message
        /// </summary>
        /// <param name="text">Message</param>
        public void Info(string text)
        {
            WriteFormattedLog(LogLevel.INFO, text);
        }

        /// <summary>
        /// Log a TRACE message
        /// </summary>
        /// <param name="text">Message</param>
        public void Trace(string text)
        {
            WriteFormattedLog(LogLevel.TRACE, text);
        }

        /// <summary>
        /// Log a WARNING message
        /// </summary>
        /// <param name="text">Message</param>
        public void Warning(string text)
        {
            WriteFormattedLog(LogLevel.WARNING, text);
        }
        #endregion
        #region private
        private void Init()
        {
            WriteLine("", false);
        }
        private void WriteLine(string msg, bool append = true)
        {
            try
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(FILE_NAME, append, System.Text.Encoding.UTF8))
                {
                    if (!string.IsNullOrEmpty(msg))
                    {
                        writer.WriteLine(msg);
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        private void WriteFormattedLog(LogLevel level, string text)
        {
            string pretext;
            switch (level)
            {
                case LogLevel.TRACE:
                    pretext = System.DateTime.Now.ToString(DATE_FORMAT) + " [TRACE]   ";
                    break;
                case LogLevel.INFO:
                    pretext = System.DateTime.Now.ToString(DATE_FORMAT) + " [INFO]    ";
                    break;
                case LogLevel.DEBUG:
                    pretext = System.DateTime.Now.ToString(DATE_FORMAT) + " [DEBUG]   ";
                    break;
                case LogLevel.WARNING:
                    pretext = System.DateTime.Now.ToString(DATE_FORMAT) + " [WARNING] ";
                    break;
                case LogLevel.ERROR:
                    pretext = System.DateTime.Now.ToString(DATE_FORMAT) + " [ERROR]   ";
                    break;
                case LogLevel.FATAL:
                    pretext = System.DateTime.Now.ToString(DATE_FORMAT) + " [FATAL]   ";
                    break;
                default:
                    pretext = "";
                    break;
            }

            WriteLine(pretext + text);
        }
        #endregion
        [System.Flags]
        private enum LogLevel
        {
            TRACE,
            INFO,
            DEBUG,
            WARNING,
            ERROR,
            FATAL
        }
    }
}
