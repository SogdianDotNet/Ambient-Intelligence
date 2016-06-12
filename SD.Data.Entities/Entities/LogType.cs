using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Data.Entities.Entities
{
    [Table("LogType")]
    public class LogType
    {
        [Key()]
        public Guid Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Log> Log { get; set; }
    }
}
