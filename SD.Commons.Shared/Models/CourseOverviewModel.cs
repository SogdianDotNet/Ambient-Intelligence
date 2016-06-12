using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Commons.Shared.Models
{
    public class CourseOverviewModel
    {
        public Guid SessionId { get; set; }
        [Display(Name = "Begintijd")]
        public DateTime? StartTime { get; set; }
        [Display(Name = "Eindtijd")]
        public DateTime? EndTime { get; set; }
        [Display(Name = "Lokaal")]
        public string ClassRoom { get; set; }
        [Display(Name = "Vak")]
        public string Course { get; set; }
        public string Klas { get; set; }
        public IEnumerable<StudentsAttentionModel> StudentsAttentions { get; set; }
        public ChartModel Chart { get; set; }
    }
}
