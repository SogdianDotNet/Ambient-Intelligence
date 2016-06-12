using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Data.Entities.Entities
{
    [Table("Course")]
    public class Course
    {
        [Key()]
        public Guid Id { get; set; }
        public string CourseName { get; set; }
        public string Coordinator { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}