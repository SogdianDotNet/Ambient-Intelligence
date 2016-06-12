using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SD.Data.Entities.Entities
{
    [Table("MediaMetaData")]
    public class MediaMetaData
    {
        [Key()]
        public Guid Id { get; set; }
        public bool VideoOnly { get; set; }
        public bool AudioOnly { get; set; }
        public bool VideoAndAudio { get; set; }
        public int FileContentLength { get; set; }
        public string FileContentType { get; set; }
        public string FileName { get; set; }
        public string SourcePath { get; set; }
        public byte[] InputStream { get; set; }
    }
}
