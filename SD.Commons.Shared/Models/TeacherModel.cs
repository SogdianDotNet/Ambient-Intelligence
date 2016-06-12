using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Commons.Shared.Models
{
    public class TeacherModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Voornaam")]
        public string Firstname { get; set; }
        [Display(Name = "Achternaam")]
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Laatst ingelogd")]
        public DateTime? LastLogin { get; set; }
        public ICollection<SessionModel> Sessions { get; set; }
    }
}
