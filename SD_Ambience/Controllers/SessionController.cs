using SD.BusinessLayer.Builders;
using SD.Commons.Shared.Models;
using SD_Ambience.Controllers.Secured;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SD_Ambience.Controllers
{
    /// <summary>
    /// class SessionController.
    /// </summary>
    public class SessionController : SecuredTeacherController
    {
        /// <summary>
        /// Returns a list of all sessions of the logged in teacher.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ListView(string email)
        {
            return View(new SessionBuilder().GetList(email));
        }

        /// <summary>
        /// Gets the details of a session from the specified id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Details(Guid id)
        {
            return View(new SessionBuilder().GetCourseOverview(id));
        }

        /// <summary>
        /// Draw chart.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DrawChart(ChartModel model)
        {
            return PartialView(model);
        }

        /// <summary>
        /// GET: Opens the start session form page.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult StartSession()
        {
            return View();
        }

        /// <summary>
        /// This post action method starts a new session and sends a JSON object to the API.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StartSession(SessionModel model)
        {
            var httpWebRequest = (HttpWebRequest) WebRequest.Create("http:////////////api/Session");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            SessionStartModelApiPost sessionModel = new SessionStartModelApiPost();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            
            //checking if the sessionmodel is not null.
            if (model != null)
            {
                //getting the current logged in teacher.
                model.TeacherEmail = HttpContext.User.Identity.Name;
                //returning a SessionStartModelApiPost model.
                sessionModel = new SessionBuilder().MakeSessionStartModelReadyForJsonConversion(model);
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    //serializing the sessionModel object to a JSON-object.
                    string jsonReturnValue = serializer.Serialize(sessionModel);
                    streamWriter.Write(jsonReturnValue);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
            }
            SessionModel returnSession = new SessionBuilder().GetById(sessionModel.Id);
            return View("StopSession", returnSession);
        }

        /// <summary>
        /// This post action method stops the session and sends a new JSON object to the API.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult StopSession(SessionModel model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            model = new SessionBuilder().GetById(model.Id);
            SessionStopModelApiPost sessionModel = new SessionStopModelApiPost();

            if (model.Id != Guid.Empty && model.Id != null)
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create("http://////////api/Session");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                sessionModel = new SessionBuilder().MakeSessionStopModelReadyForJsonConversion(model);

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string jsonReturnValue = serializer.Serialize(sessionModel);
                    streamWriter.Write(jsonReturnValue);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                HttpWebResponse httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
            }
            return RedirectToAction("Menu", "Dashboard");
        }
    }
}