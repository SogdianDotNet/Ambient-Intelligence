using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Data.Entities.Entities
{
    [Table("Report")]
    public class Report
    {
        [Key()]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
        [ForeignKey("Session")]
        public Guid SessionId { get; set; }
        public Guid CourseId
        {
            get { return Session.CourseId; }
            set { Session.CourseId = value; }
        }
        [Required]
        public virtual Session Session { get; set; }
    }
}
