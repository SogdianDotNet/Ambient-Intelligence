using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Data.Entities.Entities
{
    [Table("Log")]
    public class Log
    {
        [Key()]
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string Extra { get; set; }
        public string UserId { get; set; }
        public string Url { get; set; }
        [ForeignKey("LogType")]
        public Guid LogTypeId { get; set; }
        [ForeignKey("LogTypeSub")]
        public Guid LogTypeSubId { get; set; }
        public virtual LogType LogType { get; set; }
        public virtual LogTypeSub LogTypeSub { get; set; }
    }
}
