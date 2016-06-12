using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Commons.Shared.Models
{
    public class SessionStartModelApiPost
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public string ClassRoom { get; set; }
        public string Course { get; set; }
        public string Klas { get; set; }
        public Guid CourseId { get; set; }
        public Guid KlasId { get; set; }
        public Guid TeacherId { get; set; }
        public string Command { get; set; }
    }

    public class SessionStopModelApiPost
    {
        public Guid Id { get; set; }
        public DateTime EndTime { get; set; }
        public string Command { get; set; }
    }
}
