using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Commons.Shared.Models
{
    public class DownloadFile
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public int ContentLength { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
