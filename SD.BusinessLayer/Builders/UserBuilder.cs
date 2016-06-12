using SD.Commons.Shared.Models;
using SD.Commons.Shared.Validation;
using SD.Data;
using SD.Data.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using SD.Security.Utils;
using SD.BusinessLayer.Logging;
using SD.Commons.Shared;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace SD.BusinessLayer.Builders
{
    /// <summary>
    /// class UserBuilder.
    /// </summary>
    public class UserBuilder
    {
        /// <summary>
        /// Returns a list of users.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IEnumerable<UserViewModel> GetList(UserRoles? role)
        {
            switch (role)
                    {
                        case UserRoles.Administrator:
                                using (SDContext sdContext = new SDContext())
                                {
                                    return sdContext.Administrator.GetList()
                                                            .Select(u => new UserViewModel()
                                                            {
                                                                Id = u.Id,
                                                                Firstname = u.Firstname,
                                                                Email = u.Email,
                                                                Lastname = u.Lastname,
                                                                UserId = u.UserId,
                                                                IsAdministrator = "Ja",
                                                                IsStudent = "Nee",
                                                                IsTeacher = "Nee",
                                                                UserRole = UserRoles.Administrator.ToString(),
                                                                Role = UserRoles.Administrator
                                                            });
                                }
                        case UserRoles.Teacher:
                                using (SDContext sdContext = new SDContext())
                                {
                                    return sdContext.Teacher.GetList().Select(u => new UserViewModel()
                                    {
                                        Id = u.Id,
                                        Firstname = u.Firstname,
                                        Lastname = u.Lastname,
                                        Email = u.Email,
                                        UserId = u.UserId,
                                        IsAdministrator = "Nee",
                                        IsStudent = "Nee",
                                        IsTeacher = "Ja",
                                        Role = UserRoles.Teacher,
                                        UserRole = "Docent"
                                    });
                                }                                                 
                        case UserRoles.Student:
                                using (SDContext sdContext = new SDContext())
                                {
                                    return sdContext.Student.GetList()
                                                        .Select(st => new UserViewModel()
                                                        {
                                                            Id = st.Id,
                                                            Email = st.Email,
                                                            Firstname = st.Firstname,
                                                            Lastname = st.Lastname,
                                                            IsAdministrator = "Nee",
                                                            IsStudent = "Ja",
                                                            IsTeacher = "Nee",
                                                            UserRole = UserRoles.Student.ToString(),
                                                            Role = UserRoles.Student
                                                        });
                                }
                    case UserRoles.All:
                            List<UserViewModel> returnValue = new List<UserViewModel>();
                            var admins = new List<AdministratorModel>();
                            var teachers = new List<TeacherModel>();
                            var students = new List<StudentModel>();
                            using (SDContext sdContext = new SDContext())
                            {
                                admins = sdContext.Administrator.GetList().Select(a => new AdministratorModel()
                                {
                                    Id = a.Id,
                                    Email = a.Email,
                                    Firstname = a.Firstname,
                                    Lastname = a.Lastname,
                                    UserId = a.UserId
                                }).ToList();

                                        if (admins == null)
                                        {
                                            admins = null;
                                        }
                                        else
                                        {
                                            foreach (var record in admins)
                                            {
                                                returnValue.Add(new UserViewModel()
                                                {
                                                    Id = record.Id,
                                                    Email = record.Email,
                                                    Firstname = record.Firstname,
                                                    Lastname = record.Lastname,
                                                    IsAdministrator = "Ja",
                                                    IsStudent = "Nee",
                                                    IsTeacher = "Nee",
                                                    UserId = record.UserId,
                                                    UserRole = "Administrator",
                                                    Role = UserRoles.Administrator
                                                });
                                            }
                                        }

                                        teachers = sdContext.Teacher.GetList().Select(t => new TeacherModel()
                                        {
                                            Id = t.Id,
                                            Email = t.Email,
                                            Firstname = t.Firstname,
                                            Lastname = t.Lastname,
                                            UserId = t.UserId
                                        }).ToList();

                                        if (teachers == null)
                                        {
                                            teachers = null;
                                        }
                                        else
                                        {
                                            foreach (var rec in teachers)
                                            {
                                                returnValue.Add(new UserViewModel()
                                                {
                                                    Id = rec.Id,
                                                    Email = rec.Email,
                                                    Firstname = rec.Firstname,
                                                    Lastname = rec.Lastname,
                                                    IsAdministrator = "Nee",
                                                    IsTeacher = "Ja",
                                                    IsStudent = "Nee",
                                                    UserId = rec.UserId,
                                                    UserRole = "Docent",
                                                    Role = UserRoles.Teacher
                                                });
                                            }
                                        }

                                        students = sdContext.Student.GetList().Select(st => new StudentModel()
                                        {
                                            Id = st.Id,
                                            Email = st.Email,
                                            Firstname = st.Firstname,
                                            Lastname = st.Lastname,
                                            KlasId = st.KlasId,
                                            UserId = st.UserId
                                        }).ToList();

                                        if (students == null)
                                        {
                                            students = null;
                                        }
                                        else
                                        {
                                            foreach (var rec in students)
                                            {
                                                returnValue.Add(new UserViewModel()
                                                {
                                                    Id = rec.Id,
                                                    Email = rec.Email,
                                                    Firstname = rec.Firstname,
                                                    Lastname = rec.Lastname,
                                                    IsAdministrator = "Nee",
                                                    IsTeacher = "Nee",
                                                    IsStudent = "Ja",
                                                    UserId = rec.UserId,
                                                    UserRole = "Student",
                                                    Role = UserRoles.Student
                                                });
                                            }
                                        }
                                    return returnValue.OrderByDescending(x => x.IsAdministrator).ThenBy(x => x.IsTeacher).ThenBy(x => x.IsStudent);
                            }
                }
            return null;
        }        

        /// <summary>
        /// Gets an User by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserViewModel GetById(string userId)
        {
            List<ErrorMessage> errors = new List<ErrorMessage>();
            if (!String.IsNullOrEmpty(userId))
            {
                using (SDContext sdContext = new SDContext())
                {
                    var user = GetApplicationUserById(userId);
                    if (sdContext.Administrator.Get(x=>x.UserId == userId) != null)
                    {
                        var admin = sdContext.Administrator.Get(x => x.UserId == userId);
                        UserViewModel model = new UserViewModel()
                        {
                            Id = admin.Id,
                            Email = admin.Email,
                            Firstname = admin.Firstname,
                            Lastname = admin.Lastname,
                            IsAdministrator = "Ja",
                            IsStudent = "Nee",
                            IsTeacher = "Nee",
                            UserId = admin.UserId,
                            Role = UserRoles.Administrator
                        };
                        return model;
                    }
                    else if (sdContext.Teacher.Get(x=>x.UserId == userId) != null)
                    {
                        var teacher = sdContext.Teacher.Get(x => x.UserId == userId);
                        UserViewModel model = new UserViewModel()
                        {
                            Id = teacher.Id,
                            Email = teacher.Email,
                            Firstname = teacher.Firstname,
                            Lastname = teacher.Lastname,
                            IsAdministrator = "Nee",
                            IsStudent = "Nee",
                            IsTeacher = "Ja",
                            UserId = teacher.UserId,
                            Role = UserRoles.Teacher
                        };
                        return model;
                    }
                    else if (sdContext.Student.Get(x=>x.UserId == userId) != null)
                    {
                        var student = sdContext.Student.Get(x => x.UserId == userId);
                        UserViewModel model = new UserViewModel()
                        {
                            Id = student.Id,
                            Email = student.Email,
                            Firstname = student.Firstname,
                            Lastname = student.Lastname,
                            IsAdministrator = "Nee",
                            IsTeacher = "Nee",
                            IsStudent = "Ja",
                            UserId = student.UserId,
                            Role = UserRoles.Student
                        };
                        return model;
                    }
                    else
                    {
                        errors.Add(new ErrorMessage("No user was found."));
                        return null;
                    }
                }
            }
            else
            {
                errors.Add(new ErrorMessage("The id is null or empty."));
                return null;
            }
        }

        /// <summary>
        /// Gets an Administrator entity by the specified email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public AdministratorModel GetAdministratorByEmail(string email)
        {
            if (!String.IsNullOrEmpty(email))
            {
                using (SDContext sdContext = new SDContext())
                {
                    var admin = sdContext.Administrator.Get(x => x.Email == email);
                    if (admin == null)
                    {
                        return null;
                    }
                    AdministratorModel model = new AdministratorModel()
                    {
                        Id = admin.Id,
                        Email = admin.Email,
                        Firstname = admin.Firstname,
                        Lastname = admin.Lastname,
                        UserId = admin.UserId,
                        LastLogin = admin.LastLogin
                    };
                    return model;
                }
            }
            else
            {
                return null;
            }
        }
        
        /// <summary>
        /// Gets the ApplicationUser from the database by its Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApplicationUser GetApplicationUserById(string id)
        {
            try
            {
                if (!String.IsNullOrEmpty(id))
                {
                    using (ApplicationDbContext context = new ApplicationDbContext())
                    {
                        var user = context.Users.FirstOrDefault(x => x.Id == id);
                        if (user != null && !String.IsNullOrEmpty(user.Id))
                        {
                            return user;
                        }
                        else
                        {
                            return null;
                        }
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
        /// Gets ApplicationUser by email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public ApplicationUser GetApplicationUserByEmail(string email)
        {
            try
            {
                if (!String.IsNullOrEmpty(email))
                {
                    using (ApplicationDbContext context = new ApplicationDbContext())
                    {
                        var user = context.Users.FirstOrDefault(x => x.Email == email);
                        if (user != null && !String.IsNullOrEmpty(user.Id))
                        {
                            return user;
                        }
                        else
                        {
                            return null;
                        }
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
        /// Gets the ApplicationUser from the database by teacherId.
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public ApplicationUser GetApplicationUserByTeacherId(Guid teacherId)
        {
            try
            {
                if (teacherId != null && teacherId != Guid.Empty)
                {
                    using (SDContext sdContext = new SDContext())
                    {
                        Teacher teacher = sdContext.Teacher.Get(teacherId);
                        if (teacher != null)
                        {
                            using (ApplicationDbContext context = new ApplicationDbContext())
                            {
                                ApplicationUser user = context.Users.FirstOrDefault(x => x.Id == teacher.UserId);
                                if (user != null)
                                {
                                    return user;
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

        /// <summary>
        /// Inserts a new user to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool Insert(RegisterViewModel model, string userId)
        {
            try
            {
                if (model != null && !String.IsNullOrEmpty(model.Email) && !String.IsNullOrEmpty(model.Password) && !String.IsNullOrEmpty(model.Firstname) && !String.IsNullOrEmpty(model.Lastname))
                {
                    using (SDContext sdContext = new SDContext())
                    {
                        sdContext.Teacher.Insert(new Teacher()
                        {
                            Id = Guid.NewGuid(),
                            Email = model.Email,
                            Firstname = model.Firstname,
                            Lastname = model.Lastname,
                            LastLogin = DateTime.Now,
                            UserId = userId
                        });
                        sdContext.Save();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                return false;
            }
        }
        

        /// <summary>
        /// Assigns the Teacher role to the user.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        public void AssignTeacherRole(string userId)
        {
            if (!String.IsNullOrEmpty(userId))
            {
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    var store = new UserStore<ApplicationUser>(context);
                    var manager = new UserManager<ApplicationUser>(store);
                    var user = context.Users.FirstOrDefault(x => x.Id == userId);
                    manager.AddToRole(user.Id, "Teacher");
                    context.SaveChanges();
                }                    
            }
        }

        /// <summary>
        /// Assigns the SystemAdministrator role to the user.
        /// </summary>
        /// <param name="userId"></param>
        public void AssignAdministratorRole(string userId)
        {
            if (!String.IsNullOrEmpty(userId))
            {
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    var store = new UserStore<ApplicationUser>(context);
                    var manager = new UserManager<ApplicationUser>(store);
                    var user = context.Users.FirstOrDefault(x => x.Id == userId);
                    manager.AddToRole(user.Id, "SystemAdministrator");
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Inserts a new SystemAdministrator to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool InsertAdministrator(RegisterViewModel model, string userId)
        {
            try
            {
                if (model != null && !String.IsNullOrEmpty(userId) && !String.IsNullOrEmpty(model.Email) && !String.IsNullOrEmpty(model.Password) && !String.IsNullOrEmpty(model.Firstname) && !String.IsNullOrEmpty(model.Lastname))
                {
                    using (SDContext sdContext = new SDContext())
                    {
                        sdContext.Administrator.Insert(new Administrator
                        {
                            Id = Guid.NewGuid(),
                            Email = model.Email,
                            Firstname = model.Firstname,
                            Lastname = model.Lastname,
                            UserId = userId
                        });
                        sdContext.Save();                        
                    }
                    AssignAdministratorRole(userId);
                    return true;
                }
                else
                {
                    Logger.Write("Administrator could not be added.", "The model validation returned false. This was caused in the method InsertAdministrator() in the class UserBuilder.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                return false;
            }
        }
        

        /// <summary>
        /// Deletes the user from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool Delete(Guid id)
        {
            try
            {
                if (id != null || id != Guid.Empty)
                {
                    using (SDContext sdContext = new SDContext())
                    {
                        if (sdContext.Administrator.Get(id) != null)
                        {
                            var admin = sdContext.Administrator.Get(id);
                            sdContext.Administrator.Delete(admin);
                            sdContext.Save();
                            DeleteApplicationUser(admin.UserId, admin.Email);
                            return true;
                        }
                        else if (sdContext.Teacher.Get(id) != null)
                        {
                            var teacher = sdContext.Teacher.Get(id);
                            sdContext.Teacher.Delete(teacher);
                            sdContext.Save();
                            DeleteApplicationUser(teacher.UserId, teacher.Email);
                            return true;
                        }
                        else if (sdContext.Student.Get(id) != null)
                        {
                            var student = sdContext.Student.Get(id);
                            sdContext.Student.Delete(id);
                            sdContext.Save();
                            DeleteApplicationUser(student.UserId, student.Email);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    Logger.Write("The user could not be removed.", "This occured in the Delete() method of the UserBuilder class. The id was null or empty.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                return false;
            }
        }

        /// <summary>
        /// Deletes the ApplicationUser from the database.
        /// </summary>
        /// <param name="userId"></param>
        public void DeleteApplicationUser(string userId, string email)
        {
            try
            {
                if (!String.IsNullOrEmpty(userId))
                {
                    using (ApplicationDbContext context = new ApplicationDbContext())
                    {
                        var user = context.Users.FirstOrDefault(x => x.Id == userId && x.Email == email);
                        if (user != null && !String.IsNullOrEmpty(user.Id) && !String.IsNullOrEmpty(user.Email))
                        {
                            context.Users.Remove(user);
                            context.SaveChanges();
                        }
                        else
                        {
                            Logger.Write("The user was not found.", "The user was not found, its properties were null or empty or the ApplicationUser entity was null.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "The user has not been deleted.");
            }
        }

        /// <summary>
        /// Inserts the last login datetime.
        /// </summary>
        /// <param name="email"></param>
        public void InsertLastLoginTeacher(string email)
        {
            try
            {
                if (!String.IsNullOrEmpty(email))
                {
                    using (SDContext sdContext = new SDContext())
                    {
                        Teacher teacher = sdContext.Teacher.Get(x => x.Email == email);
                        if (teacher != null && teacher.Email == email)
                        {
                            teacher.LastLogin = DateTime.Now;
                            sdContext.Teacher.Update(teacher);
                            sdContext.Save();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        /// <summary>
        /// Inserts the last login datetime.
        /// </summary>
        /// <param name="email"></param>
        public void InsertLastLoginAdministrator(string email)
        {
            try
            {
                if (!String.IsNullOrEmpty(email))
                {
                    using (SDContext sdContext = new SDContext())
                    {
                        Administrator admin = sdContext.Administrator.Get(x => x.Email == email);
                        if (admin != null && admin.Email == email)
                        {
                            admin.LastLogin = DateTime.Now;
                            sdContext.Administrator.Update(admin);
                            sdContext.Save();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }

        /// <summary>
        /// Checks if there's any teacher containing the same email as the login email input.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsTeacher(string email)
        {
            try
            {
                if (!String.IsNullOrEmpty(email))
                {
                    using (SDContext sdContext = new SDContext())
                    {
                        Teacher teacher = sdContext.Teacher.Get(x => x.Email == email);
                        if (teacher != null && teacher.Email == email)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                return false;
            }
        }

        /// <summary>
        /// Checks if the using has the SystemAdministrator role.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsAdministrator(string email)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var user = context.Users.FirstOrDefault(x => x.Email == email);
                if (user == null)
                {
                    return false;
                }
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                bool returnValue = manager.IsInRole(user.Id, "SystemAdministrator");

                if (returnValue)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// enum Roles.
        /// </summary>
        public enum Roles
        {
            SystemAdministrator,
            Teacher,
            Student
        }
    }
}