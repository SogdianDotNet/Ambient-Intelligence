using SD.BusinessLayer.Builders;
using SD.BusinessLayer.Logging;
using SD.Commons.Shared;
using SD.Commons.Shared.Models;
using SD.Data.Entities.Entities;
using SD_Ambience.Controllers.Secured;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SD_Ambience.Controllers
{
    /// <summary>
    /// class CourseController.
    /// </summary>
    public class CourseController : SecuredTeacherController
    {
        /// <summary>
        /// Retrieves a list of Courses.
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ListView(string userEmail)
        {
            ApplicationUser user = new UserBuilder().GetApplicationUserByEmail(userEmail);
            if (user != null)
            {
                return View(new CourseBuilder().GetList(new TeacherBuilder().GetById(user.Email).Id));
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// Details.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Details(Guid id)
        {
            try
            {
                return View(new CourseBuilder().GetById(id));
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
            return View();
        }
    }
}