using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Data.Entities.Entities
{
    [Table("ClientFileInstallation")]
    public class ClientFileInstallation
    {
        [Key()]
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public int ContentLength { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
        public string Path { get; set; }
    }
}
