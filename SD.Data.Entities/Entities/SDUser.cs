using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace SD.Data.Entities.Entities
{
    [Table("SDUser")]
    public class SDUser : IdentityUser, IPrincipal, IIdentity, IUser
    {
        private readonly FormsAuthenticationTicket _ticket;
        
        public SDUser() { }

        public SDUser(FormsAuthenticationTicket ticket)
        {
            _ticket = ticket;
        }
        
        public override string Id { get; set; }
        public override string Email { get; set; }
        public IIdentity Identity
        {
            get
            {
                return this;
            }
        }
        public string Name
        {
            get { return _ticket.Name; }
        }

        public string AuthenticationType
        {
            get { return "User"; }
        }

        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }
        public override string UserName { get; set; }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}
