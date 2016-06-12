using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Data.Entities.Entities
{
    [Table("StudentsAttention")]
    public class StudentsAttention
    {
        [Key()]
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public float AttentionScore { get; set; }
        public Guid SessionId { get; set; }
    }
}
