using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Commons.Shared.Models
{
    public class MediaMetaDataModel
    {
        public Guid Id { get; set; }
        public bool VideoOnly { get; set; }
        public bool AudioOnly { get; set; }
        public bool VideoAndAudio { get; set; }
        public int FileContentLength { get; set; }
        public string FileContentType { get; set; }
        public string FileName { get; set; }
        public string SourcePath { get; set; }
        public byte[] InputStream { get; set; }
        public SessionModel Session { get; set; }
    }
}
