using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Data.Entities.Entities
{
    [Table("Klas")]
    public class Klas
    {
        [Key()]
        public Guid Id { get; set; }
        public string Group { get; set; }
        public string Study { get; set; }
        public int Year { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
