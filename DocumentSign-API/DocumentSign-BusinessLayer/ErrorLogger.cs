using DocumentSign_Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace MbpmAPI.BusinessLogic
{
    public static class ErrorLogger
    {
        public static string connectionString;
        public static void LogError(string module, string operation, string rawData, Exception fullException)
        {
            try
            {
                string exception = "";
                string innerException = "";
                string innerInnerException = "";
                try
                {
                    exception = fullException.Message;
                }
                catch (Exception ex) { }
                try
                {
                    innerException = fullException.InnerException.Message;
                }
                catch (Exception ex) { }
                try
                {
                    innerInnerException = fullException.InnerException.InnerException.Message;
                }
                catch (Exception ex) { }
                //using (EventLog eventLog = new EventLog("CCGAppSuite"))
                //{
                //    try
                //    {
                //        eventLog.Source = "CCGAppSuite";
                //        eventLog.WriteEntry(exception, EventLogEntryType.Error);
                //    }
                //    catch(Exception ex)
                //    {

                //    }
                //}

                try
                {
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        db.Database.Connection.ConnectionString = connectionString;
                        ErrorLog errorLog = new ErrorLog();
                        errorLog.Date = DateTime.Now;
                        errorLog.Action = operation;
                        errorLog.Data = rawData;
                        errorLog.Exception = exception;
                        errorLog.InnerException = innerException;
                        errorLog.InnerInnerException = innerInnerException;
                        errorLog.Module = module;
                        db.ErrorLogs.Add(errorLog);
                        db.SaveChanges();

                    }
                }
                catch (Exception ex)
                {

                }
            }
            catch (Exception exception)
            {

            }
        }

        public static string Serialize<T>(this T value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter))
                {
                    xmlserializer.Serialize(writer, value);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }

    }
}
