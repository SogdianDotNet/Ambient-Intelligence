using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Commons.Shared.Models
{
    public class StudentsAttentionModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Tijdstip")]
        public DateTime TimeStamp { get; set; }
        [Display(Name = "Aandachtsscore")]
        public float AttentionScore { get; set; }
        public Guid SessionId { get; set; }
    }
}
