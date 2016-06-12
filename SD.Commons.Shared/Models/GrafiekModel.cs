using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Commons.Shared.Models
{
    public class GrafiekModel
    {
        public Guid SessionId { get; set; }
        public long TimeStamp { get; set; }
        public float AttentionScore { get; set; }
    }
}
