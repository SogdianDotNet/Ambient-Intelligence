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
    /// class TeacherBuilder.
    /// </summary>
    public class TeacherBuilder
    {
        /// <summary>
        /// Insert a new teacher in the database.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <returns></returns>
        public bool Insert(RegisterViewModel model, ApplicationUser user, string firstname, string lastname)
        {
            if (model != null && user != null && !String.IsNullOrEmpty(user.Email) && !String.IsNullOrEmpty(firstname) && !String.IsNullOrEmpty(lastname))
            {
                using (SDContext sdContext = new SDContext())
                {
                    sdContext.Teacher.Insert(new Teacher()
                    {
                        Id = Guid.NewGuid(),
                        Firstname = firstname,
                        Lastname = lastname,
                        Email = user.Email,
                        UserId = user.Id
                    });
                    sdContext.Save();
                }
                new UserBuilder().AssignTeacherRole(user.Id);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a teacher by its email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public TeacherModel GetById(string email)
        {
            try
            {
                using (SDContext sdContext = new SDContext())
                {
                    Teacher teacher = sdContext.Teacher.Get(x => x.Email == email);
                    if (teacher != null && !String.IsNullOrEmpty(teacher.Email) && teacher.Id != null)
                    {
                        return new TeacherModel()
                        {
                            Id = teacher.Id,
                            Email = teacher.Email,
                            Firstname = teacher.Firstname,
                            Lastname = teacher.Lastname,
                            UserId = teacher.UserId,
                            LastLogin = teacher.LastLogin,
                            Sessions = teacher.Sessions.Select(s => new SessionModel()
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
                        Logger.Write("The teacher has not been found.", "This error occured while trying to get a teacher from the database.", 
                            null, Logger.LogTypeEnum.Warning, Logger.LogTypeSubEnum.Fail);
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return null;
        }

        /// <summary>
        /// Deletes the teacher entity.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(Guid id)
        {
            try
            {
                if (id == null || id == Guid.Empty)
                {
                    return false;
                }

                using (SDContext sdContext = new SDContext())
                {
                    Teacher teacher = sdContext.Teacher.Get(id);
                    if (teacher == null)
                    {
                        return false;
                    }
                    sdContext.Teacher.Delete(id);
                    sdContext.Save();
                    new UserBuilder().DeleteApplicationUser(teacher.UserId, teacher.Email);
                }
                return true;                
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return false;
        }
    }
}
