using SD.BusinessLayer.Builders;
using SD.Commons.Shared.Models;
using SD_Ambience.Controllers.Secured;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SD_Ambience.Controllers
{
    /// <summary>
    /// class TeacherController.
    /// </summary>
    public class TeacherController : SecuredTeacherController
    {
        /// <summary>
        /// Details.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Details(string email)
        {
            return View(new TeacherBuilder().GetById(email));
        }

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(string email)
        {
            return View(new TeacherBuilder().GetById(email));
        }

        /// <summary>
        /// Delete post.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TeacherModel model)
        {
            if (model == null)
            {
                return View();
            }
            model = new TeacherBuilder().GetById(model.Email);
            new TeacherBuilder().Delete(model.Id);
            return RedirectToAction("ListView", "User", HttpContext.User.Identity.Name);
        }
    }
}