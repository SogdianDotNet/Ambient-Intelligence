using SD.Commons.Shared.Models;
using SD_Ambience.Controllers.Secured;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SD_Ambience.Controllers
{
    /// <summary>
    /// class DashboardController.
    /// </summary>
    public class DashboardController : SecuredTeacherController
    {
        /// <summary>
        /// Menu.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Menu()
        {
            //Chart chart = new Chart(width: 400, height: 200)
            //.AddTitle("Chart Title")
            //.AddSeries(
            //    name: "Employee",
            //    xValue: new[] { "Peter", "Andrew", "Julie", "Mary", "Dave" },
            //    yValues: new[] { "2", "6", "4", "5", "3" })
            //.Write();
            return View();
        }

        /// <summary>
        /// TEST
        /// </summary>
        /// <returns></returns>
        public ActionResult DrawChart()
        {
            CourseModel course = new CourseModel();
            return PartialView(course);
        }
    }
}