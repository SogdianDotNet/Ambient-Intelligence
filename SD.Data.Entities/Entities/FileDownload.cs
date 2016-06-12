using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Data.Entities.Entities
{
    [Table("FileDownload")]
    public class FileDownload
    {
        [Key()]
        public Guid Id { get; set; }
        public string IpAddress { get; set; }
        public DateTime Date { get; set; }
        public string FileName { get; set; }
    }
}
