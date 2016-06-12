using SD.Commons.Shared.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SD_Ambience.Controllers.Secured
{
    [SDSecurity(Roles = "SystemAdministrator")]
    public abstract class SecuredAdministratorController : Controller
    {
    }
}