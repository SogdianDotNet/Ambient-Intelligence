using SD.BusinessLayer.Builders;
using SD_Ambience.Controllers.Secured;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SD_Ambience.Controllers
{
    /// <summary>
    /// class LogController.
    /// </summary>
    public class LogController : SecuredAdministratorController
    {
        /// <summary>
        /// Gets a list of all logs from the database.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ListView()
        {
            return View(new LogBuilder().GetList().ToList());
        }

        /// <summary>
        /// Gets the details of the log from the specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Details(Guid id)
        {
            if (id != null && id != Guid.Empty)
            {
                return View(new LogBuilder().GetById(id));
            }
            return RedirectToAction("ListView");
        }
    }
}