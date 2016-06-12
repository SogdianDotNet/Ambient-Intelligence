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
    /// class CourseBuilder.
    /// </summary>
    public class CourseBuilder
    {
        /// <summary>
        /// Gets a list of Courses by teacherId.
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public IEnumerable<CourseModel> GetList(Guid teacherId)
        {
            if (teacherId != null)
            {
                using (SDContext sdContext = new SDContext())
                {
                    var sessions = sdContext.Session.GetList(x => x.TeacherId == teacherId);
                    var courses = new List<CourseModel>();
                    foreach (var co in sessions)
                    {
                        courses.Add(new CourseModel()
                        {
                            Id = co.Course.Id,
                            Coordinator = co.Course.Coordinator,
                            CourseName = co.Course.CourseName
                        });
                    }
                    return courses;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets a course by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CourseModel GetById(Guid id)
        {
            try
            {
                if (id != null && id != Guid.Empty)
                {
                    using (SDContext sdContext = new SDContext())
                    {
                        Course obj = sdContext.Course.Get(id);
                        if (obj != null && obj.Id != null && obj.Id != Guid.Empty)
                        {
                            return new CourseModel()
                            {
                                Id = obj.Id,
                                Coordinator = obj.Coordinator,
                                CourseName = obj.CourseName,
                                //StudentAttention = obj.
                                Sessions = obj.Sessions.Select(s => new SessionModel()
                                {
                                    Id = s.Id,
                                    ClassRoom = s.Classroom,
                                    CourseId = s.CourseId,
                                    EndTime = s.EndTime,
                                    KlasId = s.KlasId,
                                    StartTime = s.StartTime,
                                    TeacherId = s.TeacherId
                                }).ToList()
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
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return null;
        }
    }
}
