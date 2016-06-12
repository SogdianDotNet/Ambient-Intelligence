using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Commons.Shared.Models
{
    public class KlasModel
    {
        public Guid Id { get; set; }
        public string Group { get; set; }
        public string Study { get; set; }
        public int Year { get; set; }
        public ICollection<StudentModel> Students { get; set; }
        public ICollection<SessionModel> Sessions { get; set; }
    }
}
