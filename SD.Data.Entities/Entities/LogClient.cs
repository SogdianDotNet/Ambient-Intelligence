using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Data.Entities.Entities
{
    [Table("LogClient")]
    public class LogClient
    {
        [Key()]
        public Guid Id { get; set; }
        public string Exception { get; set; }
        public DateTime Date { get; set; }
    }
}
