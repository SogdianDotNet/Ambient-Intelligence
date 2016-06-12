using SD.BusinessLayer.Logging;
using SD.Commons.Shared.Models;
using SD.Data;
using SD.Data.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.BusinessLayer.Builders
{
    /// <summary>
    /// class LogBuilder.
    /// </summary>
    public class LogBuilder
    {
        /// <summary>
        /// Gets a list of logs.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LogModel> GetList()
        {
            using (SDContext sdContext = new SDContext())
            {
                return sdContext.Log.GetList().Select(x => new LogModel()
                {
                    Id = x.Id,
                    Date = x.Date,
                    Exception = x.Exception,
                    Extra = x.Extra,
                    LogType = Logger.GetLogTypeString(x.LogTypeId),
                    LogTypeSub = Logger.GetLogTypeSubString(x.LogTypeSubId),
                    LogTypeId = x.LogTypeId,
                    LogTypeSubId = x.LogTypeSubId,
                    Message = x.Message,
                    Url = x.Url,
                    UserId = x.UserId
                });
            }
        }

        /// <summary>
        /// Gets a log by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LogModel GetById(Guid id)
        {
            if (id != null && id != Guid.Empty)
            {
                using (SDContext sdContext = new SDContext())
                {
                    Log log = sdContext.Log.Get(id);
                    if (log == null)
                    {
                        return null;
                    }

                    LogModel model = new LogModel()
                    {
                        Id = log.Id,
                        Date = log.Date,
                        Exception = log.Exception,
                        Extra = log.Extra,
                        LogType = Logger.GetLogTypeString(log.LogTypeId),
                        LogTypeSub = Logger.GetLogTypeSubString(log.LogTypeSubId),
                        LogTypeId = log.LogTypeId,
                        LogTypeSubId = log.LogTypeSubId,
                        Message = log.Message,
                        Url = log.Url,
                        UserId = log.UserId
                    };
                    return model;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
