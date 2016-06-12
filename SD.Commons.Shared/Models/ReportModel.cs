using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Commons.Shared.Models
{
    public class ReportModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
        public Guid SessionId { get; set; }
        public Guid CourseId { get; set; }
        public virtual SessionModel Session { get; set; }
    }
}
