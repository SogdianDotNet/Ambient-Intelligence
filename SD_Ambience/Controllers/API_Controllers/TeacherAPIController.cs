using SD.BusinessLayer.Builders;
using SD.Commons.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SD_Ambience.Controllers.API_Controllers
{
    public class TeacherAPIController : ApiController
    {
        [HttpGet]
        [Route("api/TeacherApi/GetTeacherDetails/{email}")]
        public object GetTeacherDetails(string email)
        {
            if (!String.IsNullOrEmpty(email))
            {
                return Json(new TeacherBuilder().GetById(email));
            }
            else
            {
                return null;
            }
        }
    }
}
