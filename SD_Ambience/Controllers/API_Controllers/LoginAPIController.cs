using SD.BusinessLayer.API_Builders;
using SD_Ambience.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SD_Ambience.Controllers.API_Controllers
{
    /// <summary>
    /// class LoginApiController.
    /// </summary>
    public class LoginAPIController : ApiController
    {

        /// <summary>
        /// Login action method API.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pincode"></param>
        /// <returns></returns>
        [Route("api/LoginApi/Login/{username}/{pincode}")]
        [HttpGet]
        public object Login(string username, string pincode)
        {
            LoginApiModel model = new LoginApiModel();
            if (new LoginAPIBuilder().Login(username, Convert.ToInt32(pincode)))
            {
                model.Result = true;
                return Json(model);
            }
            else
            {
                model.Result = false;
                return Json(model);
            }
        }

        // POST: api/LoginAPI
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/LoginAPI/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/LoginAPI/5
        public void Delete(int id)
        {
        }
    }
}
