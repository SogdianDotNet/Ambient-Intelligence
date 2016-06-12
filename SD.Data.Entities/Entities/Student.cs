using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Data.Entities.Entities
{
    [Table("Student")]
    public class Student
    {
        [Key()]
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
        [ForeignKey("Klas")]
        public Guid KlasId { get; set; }
        [Required]
        public virtual Klas Klas { get; set; }
    }
}
