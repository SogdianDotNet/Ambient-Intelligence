using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Data.Entities.Entities
{
    [Table("Teacher")]
    public class Teacher
    {
        [Key()]
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public DateTime? LastLogin { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
