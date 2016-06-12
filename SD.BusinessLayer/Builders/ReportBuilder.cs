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
    /// class ReportBuilder.
    /// </summary>
    public class ReportBuilder
    {
        /// <summary>
        /// Gets a list of reports by teacherId.
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public IEnumerable<ReportModel> GetList(Guid teacherId)
        {
            if (teacherId != null)
            {
                using (SDContext sdContext = new SDContext())
                {
                    return sdContext.Report.GetList(x => x.Session.TeacherId == teacherId)
                        .Select(x => new ReportModel()
                        {
                            Id = x.Id,
                            Date = x.Date,
                            Rating = x.Rating,
                            Description = x.Description,
                            CourseId = x.CourseId,
                            SessionId = x.SessionId,
                            Session = new SessionModel()
                            {
                                Id = x.Session.Id,
                                ClassRoom = x.Session.Classroom,
                                StartTime = x.Session.StartTime,
                                EndTime = x.Session.EndTime,
                                KlasId = x.Session.KlasId,
                                CourseId = x.Session.CourseId,
                                TeacherId = x.Session.TeacherId
                            }
                        });
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a report by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ReportModel GetById(Guid id)
        {
            if (id != null && id != Guid.Empty)
            {
                using (SDContext sdContext = new SDContext())
                {
                    Report report = sdContext.Report.Get(id);
                    if (report != null)
                    {
                        return new ReportModel()
                        {
                            Id = report.Id,
                            Date = report.Date,
                            Rating = report.Rating,
                            CourseId = report.CourseId,
                            Description = report.Description,
                            SessionId = report.SessionId,
                            Session = new SessionModel()
                            {
                                Id = report.Session.Id,
                                StartTime = report.Session.StartTime,
                                ClassRoom = report.Session.Classroom,
                                KlasId = report.Session.KlasId,
                                CourseId = report.Session.CourseId,
                                EndTime = report.Session.EndTime,
                                TeacherId = report.Session.TeacherId
                            }
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                return null;
            }
        }
    }
}
