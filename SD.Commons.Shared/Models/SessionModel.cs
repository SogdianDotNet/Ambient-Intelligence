using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Commons.Shared.Models
{
    public class SessionModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Begintijd")]
        public DateTime? StartTime { get; set; }
        [Display(Name = "Eindtijd")]
        public DateTime? EndTime { get; set; }
        [Display(Name = "Lokaal")]
        public string ClassRoom { get; set; }
        public string Command { get; set; }
        [Display(Name = "Vak")]
        public string Course { get; set; }
        public string Klas { get; set; }
        [Display(Name = "Docent email")]
        public string TeacherEmail { get; set; }
        public Guid CourseId { get; set; }
        public Guid KlasId { get; set; }
        public Guid TeacherId { get; set; }
        public KlassenEnum KlasEnum { get; set; }
        public LokalenEnum LokalenEnum { get; set; }
        public CoursesEnum CoursesEnum { get; set; }
    }
}
