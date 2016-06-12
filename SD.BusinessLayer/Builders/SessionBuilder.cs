using Newtonsoft.Json;
using SD.BusinessLayer.Logging;
using SD.Commons.Shared;
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
    /// class SessionBuilder.
    /// </summary>
    public class SessionBuilder
    {
        /// <summary>
        /// Gets a list of ended sessions of the current logged in teacher.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public IEnumerable<SessionModel> GetList(string email)
        {
            SDContext sdContext = new SDContext();
            IEnumerable<SessionModel> returnValue = sdContext.Session.GetList(x => x.Teacher.Email == email).Select(x => new SessionModel()
            {
                Id = x.Id,
                ClassRoom = x.Classroom,
                Course = x.Course.CourseName,
                CourseId = x.CourseId,
                EndTime = x.EndTime,
                Klas = x.Klas.Group,
                KlasId = x.KlasId,
                StartTime = x.StartTime,
                TeacherEmail = x.Teacher.Email,
                TeacherId = x.TeacherId
            }).Where(x=>x.EndTime != null);
            return returnValue;
        }

        /// <summary>
        /// Gets a Session by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SessionModel GetById(Guid id)
        {
            if (id != null && id != Guid.Empty)
            {
                using (SDContext sdContext = new SDContext())
                {
                    Session session = sdContext.Session.Get(id);
                    if (session != null)
                    {
                        string teacherEmail = sdContext.Teacher.Get(x => x.Id == session.TeacherId).Email;
                        SessionModel model = new SessionModel()
                        {
                            Id = session.Id,
                            ClassRoom = session.Classroom,
                            CourseId = session.CourseId,
                            KlasId = session.KlasId,
                            StartTime = session.StartTime,
                            TeacherEmail = teacherEmail,
                            TeacherId = session.TeacherId,
                            Klas = session.Klas.Group,
                            Course = session.Course.CourseName,
                            EndTime = session.EndTime
                        };
                        return model;
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

        /// <summary>
        /// Gets a CourseOverviewModel by the specified sessionId. It returns information about the session and the average attention scores of the students in different timestamps.
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        public CourseOverviewModel GetCourseOverview(Guid sessionId)
        {
            CourseOverviewModel model = new CourseOverviewModel();
            if (sessionId != null && sessionId != Guid.Empty)
            {
                using (SDContext sdContext = new SDContext())
                {
                    SessionModel session = GetById(sessionId);
                    IEnumerable<StudentsAttentionModel> studentsAttentions = sdContext.StudentsAttention.GetList(x => x.SessionId == sessionId)
                                                    .Select(x => new StudentsAttentionModel()
                                                    {
                                                        Id = x.Id,
                                                        AttentionScore = x.AttentionScore,
                                                        SessionId = x.SessionId,
                                                        TimeStamp = x.TimeStamp
                                                    });

                    if (session == null && studentsAttentions == null)
                    {
                        return null;
                    }

                    model.SessionId = session.Id;
                    model.ClassRoom = session.ClassRoom;
                    model.EndTime = session.EndTime;
                    model.Klas = session.Klas;
                    model.StartTime = session.StartTime;
                    model.StudentsAttentions = studentsAttentions;
                    model.Course = session.Course;
                    model.Chart.Scores = studentsAttentions.Select(x => x.AttentionScore);
                    model.Chart.TimeStamps = studentsAttentions.Select(x => x.TimeStamp);
                }
            }
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeStamps"></param>
        /// <param name="scores"></param>
        /// <returns></returns>
        public ChartModel GetChart(IEnumerable<DateTime> timeStamps, IEnumerable<float> scores)
        {
            ChartModel chart = new ChartModel();
            if (timeStamps != null && scores != null)
            {
                chart.Scores = scores;
                chart.TimeStamps = timeStamps;
            }
            return chart;
        }

        /// <summary>
        /// Returning a session model to pass it in JSON to the API controller.
        /// </summary>
        /// <param name="teacherEmail"></param>
        /// <param name="courseName"></param>
        /// <param name="klasName"></param>
        /// <returns></returns>
        public SessionModel GetSessionData(string teacherEmail, string courseName, string klasName)
        {
            SessionModel session = new SessionModel();
            if (!string.IsNullOrEmpty(teacherEmail) && !string.IsNullOrEmpty(courseName) && !string.IsNullOrEmpty(klasName))
            {
                using (SDContext context = new SDContext())
                {
                    Guid courseId = GetCourseIdByName(courseName);
                    Guid klasId = GetKlasIdByName(klasName);
                    Guid teacherId = context.Teacher.Get(x => x.Email == teacherEmail).Id;

                    if (teacherId != null && klasId != null && teacherId != null)
                    {
                        session.TeacherId = teacherId;
                        session.KlasId = klasId;
                        session.CourseId = courseId;
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
            return session;
        }

        /// <summary>
        /// Gets the KlasId Guid by klasname.
        /// </summary>
        /// <param name="klas"></param>
        /// <returns></returns>
        private Guid GetKlasIdByName(string klas)
        {
            if (!string.IsNullOrEmpty(klas))
            {
                using (SDContext sdContext = new SDContext())
                {
                    Guid klasId = sdContext.Klas.Get(x => x.Group == klas).Id;
                    if (klasId != null && klasId != Guid.Empty)
                    {
                        return klasId;
                    }
                    else
                    {
                        return Guid.Empty;
                    }
                }
            }
            else
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Gets the courseId guid by courseName.
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        private Guid GetCourseIdByName(string course)
        {
            if (!string.IsNullOrEmpty(course))
            {
                using (SDContext sdContext = new SDContext())
                {
                    Guid courseId = sdContext.Course.Get(x => x.CourseName == course).Id;
                    if (courseId != null && courseId != Guid.Empty)
                    {
                        return courseId;
                    }
                    else
                    {
                        return Guid.Empty;
                    }
                }
            }
            else
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Returns a SessionStartModelApiPost with the right properties to pass to the API as a JSONobject.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SessionStartModelApiPost MakeSessionStartModelReadyForJsonConversion(SessionModel model)
        {
            SessionStartModelApiPost returnValue = new SessionStartModelApiPost();
            try
            {
                string error = "Er is iets misgegaan met het ophalen van de temporary Session object";
                if (model != null)
                {
                    returnValue.Id = Guid.NewGuid();
                    returnValue.ClassRoom = GetLokaalName(model.LokalenEnum);
                    returnValue.Klas = GetKlasName(model.KlasEnum);
                    returnValue.Course = GetCourseName(model.CoursesEnum);
                    var tempSession = GetSessionData(model.TeacherEmail, returnValue.Course, returnValue.Klas);
                    if (tempSession == null)
                    {
                        Logger.Write(error, "MakeSessionModelReadyForJsonConvertion methode in de SessionBuilder class.");
                    }
                    returnValue.KlasId = tempSession.KlasId;
                    returnValue.TeacherId = tempSession.TeacherId;
                    returnValue.CourseId = tempSession.CourseId;
                    returnValue.StartTime = DateTime.Now;
                    returnValue.Command = "Start";
                }
                return returnValue;
            }
            catch (JsonException ex)
            {
                Logger.Write(ex);
                return returnValue;
            }
        }

        /// <summary>
        /// Returns a SessionStopModelApiPost with the right properties to pass to the API as a JSONobject.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SessionStopModelApiPost MakeSessionStopModelReadyForJsonConversion(SessionModel model)
        {
            SessionStopModelApiPost returnValue = new SessionStopModelApiPost();
            try
            {
                string error = "Er is iets misgegaan.";
                if (model != null)
                {
                    returnValue.Id = model.Id;
                    returnValue.Command = "Stop";
                    returnValue.EndTime = DateTime.Now;
                }
                return returnValue;
            }
            catch (JsonException ex)
            {
                Logger.Write(ex);
                return returnValue;
            }
        }

        /// <summary>
        /// Returns the lokaalName in string format.
        /// </summary>
        /// <param name="lokaal"></param>
        /// <returns></returns>
        private string GetLokaalName(LokalenEnum lokaal)
        {
            switch (lokaal)
            {
                case LokalenEnum.h4206:
                    return "H.4.206";
                case LokalenEnum.h4306:
                    return "H.4.306";
                case LokalenEnum.h4308:
                    return "H.4.308";
                default:
                    return "H.4.206";
            }
        }

        /// <summary>
        /// Returns the klasName in string format.
        /// </summary>
        /// <param name="klas"></param>
        /// <returns></returns>
        private string GetKlasName(KlassenEnum klas)
        {
            switch (klas)
            {
                case KlassenEnum.INF1A:
                    return "INF1A";
                case KlassenEnum.INF1B:
                    return "INF1B";
                case KlassenEnum.INF1C:
                    return "INF1C";
                case KlassenEnum.INF2A:
                    return "INF2A";
                case KlassenEnum.INF2B:
                    return "INF2B";
                case KlassenEnum.INF2C:
                    return "INF2C";
                case KlassenEnum.INF3A:
                    return "INF3A";
                case KlassenEnum.INF3B:
                    return "INF3B";
                case KlassenEnum.INF3C:
                    return "INF3C";
                default:
                    return "INF3C";
            }
        }

        /// <summary>
        /// Returns a courseName in string format.
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        private string GetCourseName(CoursesEnum course)
        {
            switch (course)
            {
                case CoursesEnum.INFSEN011:
                    return "INFSEN01-1";
                case CoursesEnum.INFSEN012:
                    return "INFSEN01-2";
                case CoursesEnum.INFDTA011:
                    return "INFDTA01-1";
                case CoursesEnum.INFDTA012:
                    return "INFDTA01-2";
                case CoursesEnum.INFLAB01:
                    return "INFLAB01";
                case CoursesEnum.INFLAB02:
                    return "INFLAB02";
                case CoursesEnum.INFMAN011:
                    return "INFMAN01-1";
                case CoursesEnum.INFMAN012:
                    return "INFMAN01-2";
                default:
                    return "INFLAB02";
            }
        }
    }
}
