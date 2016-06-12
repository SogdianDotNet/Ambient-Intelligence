using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Commons.Shared.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Voornaam")]
        public string Firstname { get; set; }
        [Display(Name = "Achternaam")]
        public string Lastname { get; set; }
        public string Email { get; set; }
        [Display(Name = "Is een administrator")]
        public string IsAdministrator { get; set; }
        [Display(Name = "Is een docent")]
        public string IsTeacher { get; set; }
        [Display(Name = "Is een student")]
        public string IsStudent { get; set; }
        [Display(Name = "Rol")]
        public string UserRole { get; set; }
        public UserRoles Role { get; set; }
    }
}
