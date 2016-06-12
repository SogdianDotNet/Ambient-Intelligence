using SD.BusinessLayer.Builders;
using SD.Commons.Shared.Models;
using SD.Data.Entities.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SD_Ambience.Controllers
{
    /// <summary>
    /// class DownloadController.
    /// </summary>
    public class DownloadController : Controller
    {
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Upload the setupfile to the database.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "SystemAdministrator")]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (new DownloadBuilder().Upload(file))
            {
                return RedirectToAction("Menu", "Dashboard");
            }
            else
            {
                ViewData["Error"] = "De bestandstype is niet juist of het bestand is te groot. Probeer het nog eens.";
                return PartialView("~/Views/Download/ErrorUpload.cshtml");
            }
        }

        /// <summary>
        /// Download the setup file from the database.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Download()
        {
            ClientFileInstallation file = new DownloadBuilder().GetFile();

            if (file != null)
            {
                byte[] fileData = file.Data;
                Response.AddHeader("Content-type", file.ContentType);
                Response.AddHeader("Content-disposition", "attachment; filename=" + file.FileName);
                byte[] dataBlock = new byte[0x1000];
                long fileSize;
                int bytesRead;
                long totalBytesRead = 0;
                new DownloadBuilder().NewDownloadedFile(file.FileName);
                using (Stream st = new MemoryStream(fileData))
                {
                    fileSize = st.Length;
                    while (totalBytesRead < fileSize)
                    {
                        if (Response.IsClientConnected)
                        {
                            bytesRead = st.Read(dataBlock, 0, dataBlock.Length);
                            Response.OutputStream.Write(dataBlock, 0, bytesRead);
                            Response.Flush();
                            totalBytesRead += bytesRead;
                        }
                    }
                }
                Response.End();
            }
            return RedirectToAction("Menu", "Dashboard");
        }
    }
}