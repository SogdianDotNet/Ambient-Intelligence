using SD.BusinessLayer.Logging;
using SD.Commons.Shared.Models;
using SD.Data;
using SD.Data.Entities.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SD.BusinessLayer.Builders
{
    /// <summary>
    /// class DownloadBuilder.
    /// </summary>
    public class DownloadBuilder
    {
        private static Guid fileId = Guid.Parse("CBEE00EC-688E-48EC-A5FE-5A79B3270BB7");

        /// <summary>
        /// Gets the (setup) file from the database.
        /// </summary>
        /// <returns></returns>
        public ClientFileInstallation GetFile()
        {
            try
            {
                using (SDContext sdContext = new SDContext())
                {
                    ClientFileInstallation file = sdContext.ClientFileInstallation.Get(fileId);
                    if (file != null)
                    {
                        return file;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "Couldn't retrieve the file from the database so it will return a null.");
                return null;
            }
        }

        /// <summary>
        /// The SystemAdministrator can only use this method, this will upload the (local) file to the database.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool Upload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                Logger.Write("No file uploaded.", "The file could not be uploaded.");
                return false;
            }

            using (SDContext sdContext = new SDContext())
            {
                byte[] buffer = new BinaryReader(file.InputStream).ReadBytes(file.ContentLength);
                sdContext.ClientFileInstallation.Insert(new ClientFileInstallation()
                {
                    Id = Guid.NewGuid(),
                    FileName = file.FileName,
                    ContentLength = file.ContentLength,
                    ContentType = file.ContentType,
                    Data = buffer
                });
                sdContext.Save();
                return true;
            }
        }

        /// <summary>
        /// Inserts a new record of a new downloaded file.
        /// </summary>
        /// <param name="fileName"></param>
        public void NewDownloadedFile(string fileName)
        {
            if (!String.IsNullOrEmpty(fileName))
            {
                using (SDContext sdContext = new SDContext())
                {
                    sdContext.FileDownload.Insert(new FileDownload()
                    {
                        Id = Guid.NewGuid(),
                        Date = DateTime.Now,
                        IpAddress = GetUserIp(),
                        FileName = fileName
                    });
                    sdContext.Save();
                }
            }
        }

        /// <summary>
        /// Retrieves the UserIp.
        /// </summary>
        /// <returns></returns>
        protected string GetUserIp()
        {
            string visitorsIpAddress = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                visitorsIpAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                visitorsIpAddress = HttpContext.Current.Request.UserHostAddress;
            }
            return visitorsIpAddress;
        }
    }
}
