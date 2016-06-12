using SD.Commons.Shared;
using SD.Data;
using SD.Data.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SD.BusinessLayer.Logging
{
    /// <summary>
    /// static class Logger.
    /// </summary>
    public static class Logger
    {
        //logtypes
        private static Guid logTypeError = new Guid("084898bf-4028-4980-a5f9-82f193aa5177");
        private static Guid logTypeInfo = new Guid("e8f83387-dfb9-4867-9f83-eb36cc9b2bfc");
        private static Guid logTypeWarning = new Guid("9442fc8c-222d-4491-a357-acbe5b95d57b");
        private static Guid logTypeLogin = new Guid("d710a44e-0fdd-475d-a148-436f5687b72a");
        private static Guid logTypePageVisit = new Guid("be58d438-aa56-40fe-a317-92121915d3b9");

        //subtypes
        private static Guid logTypeSubSucces = new Guid("e863d06b-66f9-4b66-ba34-c1dbf6104b45");
        private static Guid logTypeSubWarning = new Guid("8ee46be2-e5b2-4aee-987a-36da86f6f8f3");
        private static Guid logTypeSubFail = new Guid("47e7f033-63c4-40eb-b915-d7a191971a08");
        private static Guid logTypeSubInfo = new Guid("46cf09fd-0cd2-4d17-b8a5-ec3019ecbf6c");


        /// <summary>
        /// Write message to log 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="extra"></param>
        /// <param name="userId"></param>
        /// <param name="logType"></param>
        /// <param name="logTypeSub"></param>
        public static void Write(string message, string extra, string userId, LogTypeEnum logType, LogTypeSubEnum logTypeSub)
        {
            Save(message, "", extra, GetUserId(), GetUrl(), logType, logTypeSub);
        }

        /// <summary>
        /// Writes the specified exception.
        /// </summary>
        /// <param name="exception"></param>
        public static void Write(Exception exception)
        {
            Save("", exception.ToString(), "", GetUserId(), GetUrl(),LogTypeEnum.Error, LogTypeSubEnum.Fail);
        }

        /// <summary>
        /// Writes the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="extra">The extra.</param>
        public static void Write(Exception exception, String extra)
        {
            Save("", exception.ToString(), extra, GetUserId(), GetUrl(), LogTypeEnum.Error, LogTypeSubEnum.Fail);
        }

        /// <summary>
        /// Writes the specified exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="extra"></param>
        public static void Write(String message, String extra)
        {
            Save(message, "", extra, GetUserId(), GetUrl(), LogTypeEnum.Error, LogTypeSubEnum.Fail);
        }

        /// <summary>
        /// Get the current userId if it's available
        /// </summary>
        /// <returns></returns>
        private static string GetUserId()
        {
            string userId = "";
            if (HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                try
                {
                    if (HttpContext.Current.User.Identity.Name != null)
                    {
                        userId = HttpContext.Current.User.Identity.Name;
                    }
                }
                catch (Exception)
                {
                    //no throw;
                }
            }
            return userId;
        }

        /// <summary>
        /// Return a url from the context (if available)
        /// </summary>
        /// <returns></returns>
        private static string GetUrl()
        {
            string url = "";
            try
            {
                if (HttpContext.Current != null)
                {
                    url = HttpContext.Current.Request.Url.ToString();
                }
            }
            catch (Exception)
            {
                url = "";
            }
            return url;
        }

        /// <summary>
        /// Saves the specified message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="extra"></param>
        /// <param name="userId"></param>
        /// <param name="url"></param>
        /// <param name="logType"></param>
        /// <param name="logTypeSub"></param>
        private static void Save(string message, string exception, string extra, string userId, string url, LogTypeEnum logType, LogTypeSubEnum logTypeSub)
        {
            try
            {
                using (SDContext context = new SDContext())
                {
                    SD.Data.Entities.Entities.Log log = new Data.Entities.Entities.Log
                    {
                        Id = Guid.NewGuid(),
                        Date = DateTime.Now,
                        Exception = exception,
                        Message = message,
                        Extra = extra,
                        UserId = userId,
                        LogTypeId = GetLogTypeGuid(logType),
                        LogTypeSubId = GetLogTypeSubGuid(logTypeSub),
                        Url = url
                    };
                    context.Log.Insert(log);
                    context.Save();
                }
            }
            catch (Exception)
            {
                //no throw;
            }
        }

        /// <summary>
        /// Get log type info
        /// </summary>
        /// <param name="logType"></param>
        /// <returns></returns>
        public static Guid GetLogTypeGuid(LogTypeEnum logType)
        {
            switch (logType)
            {
                case LogTypeEnum.Error:
                    return logTypeError;
                case LogTypeEnum.Info:
                    return logTypeInfo;
                case LogTypeEnum.Warning:
                    return logTypeWarning;
                case LogTypeEnum.PageVisit:
                    return logTypePageVisit;
                case LogTypeEnum.Login:
                    return logTypeLogin;
                default:
                    break;
            }
            return logTypeInfo;
        }

        /// <summary>
        /// Get sub info sub log types
        /// </summary>
        /// <param name="logTypeSub"></param>
        /// <returns></returns>
        public static Guid GetLogTypeSubGuid(LogTypeSubEnum logTypeSub)
        {
            switch (logTypeSub)
            {
                case LogTypeSubEnum.Succes:
                    return logTypeSubSucces;
                case LogTypeSubEnum.Warning:
                    return logTypeSubWarning;
                case LogTypeSubEnum.Fail:
                    return logTypeSubFail;
                case LogTypeSubEnum.Info:
                    return logTypeSubInfo;
                default:
                    break;
            }
            return logTypeSubInfo;
        }

        /// <summary>
        /// Get log type info.
        /// </summary>
        /// <param name="logTypeId"></param>
        /// <returns></returns>
        public static string GetLogTypeString(Guid logTypeId)
        {
            switch (logTypeId.ToString())
            {
                case "d710a44e-0fdd-475d-a148-436f5687b72a":
                    return "Login";
                case "084898bf-4028-4980-a5f9-82f193aa5177":
                    return "Error";
                case "be58d438-aa56-40fe-a317-92121915d3b9":
                    return "Page visit";
                case "9442fc8c-222d-4491-a357-acbe5b95d57b":
                    return "Warning";
                case "e8f83387-dfb9-4867-9f83-eb36cc9b2bfc":
                    return "Info";
                default:
                    break;
            };
            return "";
        }

        /// <summary>
        /// Get log type sub info.
        /// </summary>
        /// <param name="logTypeSubId"></param>
        /// <returns></returns>
        public static string GetLogTypeSubString(Guid logTypeSubId)
        {
            switch (logTypeSubId.ToString())
            {
                case "8ee46be2-e5b2-4aee-987a-36da86f6f8f3":
                    return "SubWarning";
                case "e863d06b-66f9-4b66-ba34-c1dbf6104b45":
                    return "Success";
                case "47e7f033-63c4-40eb-b915-d7a191971a08":
                    return "Fail";
                case "46cf09fd-0cd2-4d17-b8a5-ec3019ecbf6c":
                    return "SubInfo";
                default:
                    break;
            };
            return "";
        }

        /// <summary>
        /// enum LogTypeEnum
        /// </summary>
        public enum LogTypeEnum
        {
            Error,
            Info,
            Warning,
            PageVisit,
            Login
        }

        /// <summary>
        ///  enum LogTypeSubEnum
        /// </summary>
        public enum LogTypeSubEnum
        {
            Succes,
            Warning,
            Fail,
            Info
        }
    }
}
