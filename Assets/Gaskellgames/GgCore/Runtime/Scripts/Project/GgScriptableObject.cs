using UnityEngine;

namespace Gaskellgames
{
    /// <summary>
    /// Code created by Gaskellgames
    /// </summary>
    
    public abstract class GgScriptableObject : ScriptableObject
    {
        #region verboseLogs

        [SerializeField]
        [Tooltip("If verbose logs is true: info message logs will be displayed in the console alongside warning and error message logs for this ScriptableObject instance.")]
        protected bool verboseLogs = true;

        /// <summary>
        /// TODO - Use new Log format!
        /// </summary>
        protected void Log(object message, LogType type = LogType.Log)
        {
            if (type == LogType.Log && !verboseLogs) return;
            GgLogs.Log(this, type, message.ToString());
        }
        
        /// <summary>
        /// If verbose logs is true: info message logs will be displayed in the console alongside warning and error message logs.
        /// </summary>
        /// <param name="logType">Type of log to show in the console.</param>
        /// <param name="format">String format of the log to be shown.</param>
        /// <param name="args">Arguments to be injected to the string format.</param>
        protected void Log(LogType logType, string format, params object[] args)
        {
            if (logType == LogType.Log && !verboseLogs) return;
            GgLogs.Log(this, logType, format, args);
        }
        
        /// <summary>
        /// If verbose logs is true: info message logs will be displayed in the console alongside warning and error message logs.
        /// </summary>
        /// <param name="messageColor">Color of the message to display in the console</param>
        /// <param name="logType">Type of log to show in the console.</param>
        /// <param name="format">String format of the log to be shown.</param>
        /// <param name="args">Arguments to be injected to the string format.</param>
        protected void Log(Color32 messageColor, LogType logType, string format, params object[] args)
        {
            if (logType == LogType.Log && !verboseLogs) return;
            GgLogs.Log(messageColor, this, logType, format, args);
        }

        #endregion
        
    } // class end
}
