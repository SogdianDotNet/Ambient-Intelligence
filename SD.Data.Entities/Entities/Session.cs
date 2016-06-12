using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Data.Entities.Entities
{
    [Table("Session")]
    public class Session
    {
        [Key()]
        public Guid Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Classroom { get; set; }
        public string Command { get; set; }
        [ForeignKey("Course")]
        public Guid CourseId { get; set; }
        [ForeignKey("Klas")]
        public Guid KlasId { get; set; }
        [ForeignKey("Teacher")]
        public Guid TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual Course Course { get; set; }
        public virtual Klas Klas { get; set; }
    }
}
