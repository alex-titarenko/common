using System;
using System.Diagnostics;
using System.IO;


namespace TAlex.Common.Diagnostics.Listeners
{
    /// <summary>
    /// Represents the wrapper for <see cref="TextWriterTraceListener"/> allowing to create daily log files.
    /// </summary>
    /// <typeparam name="T">The type of trace listener for wrapping.</typeparam>
    public class DailyTraceListenerWrapper<T> : TextWriterTraceListener where T : TextWriterTraceListener
    {
        private string _fileName = String.Empty;
        private DateTime _currentDate;

        /// <summary>
        /// Represents the trace listener for wrapping.
        /// </summary>
        public T Listener;
        

        /// <summary>
        /// Initializes a new instance of the <see cref="TAlex.Common.Diagnostics.Listeners.DailyTraceListenerWrapper{T}"/>
        /// class, using the file as the recipient of the debugging and tracing output.
        /// </summary>
        /// <param name="fileName">The name of the file the <see cref="TAlex.Common.Diagnostics.Listeners.DailyTraceListenerWrapper{T}"/> writes to.</param>
        /// <exception cref="System.ArgumentNullException">The file is null.</exception>
        public DailyTraceListenerWrapper(string fileName)
            : base(fileName)
        {
            Listener = (T)Activator.CreateInstance(typeof(T), fileName);
            _fileName = fileName;
        }


        /// <summary>
        /// Writes the value of the object's to the specified listener.
        /// </summary>
        /// <param name="o">An <see cref="System.Object"/> to write.</param>
        public override void Write(object o)
        {
            if (EnsureWrite())
            {
                Listener.Write(o);
            }
        }

        /// <summary>
        /// Writes the specified message to the specified listener.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void Write(string message)
        {
            if (EnsureWrite())
            {
                Listener.Write(message);
            }
        }

        /// <summary>
        /// Writes a category name and the value of the object's to the specified listener.
        /// </summary>
        /// <param name="o">An <see cref="System.Object"/> to write.</param>
        /// <param name="category">A category name used to organize the output.</param>
        public override void Write(object o, string category)
        {
            if (EnsureWrite())
            {
                Listener.Write(o, category);
            }
        }

        /// <summary>
        /// Writes a category name and the message to the specified listener.
        /// </summary>
        /// <param name="message">A message to write.</param>
        /// <param name="category">A category name used to organize the output.</param>
        public override void Write(string message, string category)
        {
            if (EnsureWrite())
            {
                Listener.Write(message, category);
            }
        }

        /// <summary>
        /// Writes the value of the object's to the specified listener,
        /// followed by a line terminator.
        /// </summary>
        /// <param name="o">An <see cref="System.Object"/> to write.</param>
        public override void WriteLine(object o)
        {
            if (EnsureWrite())
            {
                Listener.WriteLine(o);
            }
        }

        /// <summary>
        /// Writes a message to the specified listener, followed by a line terminator.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void WriteLine(string message)
        {
            if (EnsureWrite())
            {
                Listener.WriteLine(message);
            }
        }

        /// <summary>
        /// Writes a category name and the value of the object's to the specified listener,
        /// followed by a line terminator.
        /// </summary>
        /// <param name="o">An <see cref="System.Object"/> to write.</param>
        /// <param name="category">A category name used to organize the output.</param>
        public override void WriteLine(object o, string category)
        {
            if (EnsureWrite())
            {
                Listener.WriteLine(o, category);
            }
        }

        /// <summary>
        /// Writes a category name and a message to the specified listener,
        /// followed by a line terminator.
        /// </summary>
        /// <param name="message">A message to write.</param>
        /// <param name="category">A category name used to organize the output.</param>
        public override void WriteLine(string message, string category)
        {
            if (EnsureWrite())
            {
                Listener.WriteLine(message, category);
            }
        }

        /// <summary>
        /// Emits an error message to the specified listener.
        /// </summary>
        /// <param name="message">A message to write.</param>
        public override void Fail(string message)
        {
            if (EnsureWrite())
            {
                Listener.Fail(message);
            }
        }

        /// <summary>
        /// Emits an error message and a detailed error message to the specified listener.
        /// </summary>
        /// <param name="message">A message to write.</param>
        /// <param name="detailMessage">A detailed message to emit.</param>
        public override void Fail(string message, string detailMessage)
        {
            if (EnsureWrite())
            {
                Listener.Fail(message, detailMessage);
            }
        }

        /// <summary>
        /// Writes trace information, a data object and event information to the specified listener specific output.
        /// </summary>
        /// <param name="eventCache">A <see cref="System.Diagnostics.TraceEventCache"/> object that contains the current process ID, thread ID, and stack trace information.</param>
        /// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
        /// <param name="eventType">One of the System.Diagnostics.TraceEventType values specifying the type of event that has caused the trace.</param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="data">The trace data to emit.</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if (EnsureWrite())
            {
                Listener.TraceData(eventCache, source, eventType, id, data);
            }
        }

        /// <summary>
        /// Writes trace information, an array of data objects and event information to the specified listener specific output.
        /// </summary>
        /// <param name="eventCache">A <see cref="System.Diagnostics.TraceEventCache"/> object that contains the current process ID, thread ID, and stack trace information.</param>
        /// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
        /// <param name="eventType">One of the System.Diagnostics.TraceEventType values specifying the type of event that has caused the trace.</param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="data">An array of objects to emit as data.</param>
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        {
            if (EnsureWrite())
            {
                Listener.TraceData(eventCache, source, eventType, id, data);
            }
        }

        /// <summary>
        /// Writes trace and event information to the specified listener specific output.
        /// </summary>
        /// <param name="eventCache">A <see cref="System.Diagnostics.TraceEventCache"/> object that contains the current process ID, thread ID, and stack trace information.</param>
        /// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
        /// <param name="eventType">One of the System.Diagnostics.TraceEventType values specifying the type of event that has caused the trace.</param>
        /// <param name="id">A numeric identifier for the event.</param>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
        {
            if (EnsureWrite())
            {
                Listener.TraceEvent(eventCache, source, eventType, id);
            }
        }

        /// <summary>
        /// Writes trace information, a formatted array of objects and event information to the specified listener specific output.
        /// </summary>
        /// <param name="eventCache">A <see cref="System.Diagnostics.TraceEventCache"/> object that contains the current process ID, thread ID, and stack trace information.</param>
        /// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
        /// <param name="eventType">One of the System.Diagnostics.TraceEventType values specifying the type of event that has caused the trace.</param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="format">A format string that contains zero or more format items, which correspond to objects in the args array.</param>
        /// <param name="args">An object array containing zero or more objects to format.</param>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        {
            if (EnsureWrite())
            {
                Listener.TraceEvent(eventCache, source, eventType, id, format, args);
            }
        }

        /// <summary>
        /// Writes trace information, a message, and event information to the specified listener specific output.
        /// </summary>
        /// <param name="eventCache">A <see cref="System.Diagnostics.TraceEventCache"/> object that contains the current process ID, thread ID, and stack trace information.</param>
        /// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
        /// <param name="eventType">One of the System.Diagnostics.TraceEventType values specifying the type of event that has caused the trace.</param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="message">A message to write.</param>
        public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        {
            if (EnsureWrite())
            {
                Listener.TraceEvent(eventCache, source, eventType, id, message);
            }
        }

        /// <summary>
        /// Writes trace information, a message, a related activity identity and event information to the specified listener specific output.
        /// </summary>
        /// <param name="eventCache">A <see cref="System.Diagnostics.TraceEventCache"/> object that contains the current process ID, thread ID, and stack trace information.</param>
        /// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
        /// <param name="id">A numeric identifier for the event.</param>
        /// <param name="message">A message to write.</param>
        /// <param name="relatedActivityId">A <see cref="System.Guid"/> object identifying a related activity.</param>
        public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
        {
            if (EnsureWrite())
            {
                Listener.TraceTransfer(eventCache, source, id, message, relatedActivityId);
            }
        }


        /// <summary>
        /// Flushes the output buffer.
        /// </summary>
        public override void Flush()
        {
            lock (this)
            {
                if (Listener.Writer != null)
                {
                    Listener.Flush();
                }
            }
        }


        /// <summary>
        /// Returns the log filename depends on the date.
        /// </summary>
        /// <param name="date">A <see cref="System.DateTime"/> object for generating filename.</param>
        /// <returns>string that represents the log file name.</returns>
        protected virtual string GenerateFileName(DateTime date)
        {
            string fileName = Path.Combine(Path.GetDirectoryName(_fileName),
                String.Format("{0}_{1:yyyy-MM-dd}{2}", Path.GetFileNameWithoutExtension(_fileName), date, Path.GetExtension(_fileName)));

            if (!Path.IsPathRooted(fileName))
            {
                fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            }
            return fileName;
        }

        private bool EnsureWrite()
        {
            DateTime date = DateTime.UtcNow.Date;
            if (_currentDate != date || Listener.Writer == null)
            {
                try
                {
                    if (Listener.Writer != null) Listener.Writer.Close();
                    Listener.Writer = new StreamWriter(GenerateFileName(date), true);
                    _currentDate = date;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
