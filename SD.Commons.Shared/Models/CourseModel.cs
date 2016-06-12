using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Commons.Shared.Models
{
    public class CourseModel
    {
        public Guid Id { get; set; }
        public string CourseName { get; set; }
        public string Coordinator { get; set; }
        public StudentsAttentionModel StudentAttention { get; set; }
        public ICollection<SessionModel> Sessions { get; set; }
    }
}
