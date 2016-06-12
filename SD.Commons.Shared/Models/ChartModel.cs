using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Commons.Shared.Models
{
    public class ChartModel
    {
        public IEnumerable<DateTime> TimeStamps { get; set; }
        public IEnumerable<float> Scores { get; set; }
    }
}
