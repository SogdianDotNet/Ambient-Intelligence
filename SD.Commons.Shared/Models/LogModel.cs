using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Commons.Shared.Models
{
    public class LogModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Datum")]
        public DateTime Date { get; set; }
        [Display(Name = "Opmerking")]
        public string Message { get; set; }
        [Display(Name = "Exception")]
        public string Exception { get; set; }
        public string Extra { get; set; }
        [Display(Name = "UserID")]
        public string UserId { get; set; }
        public string Url { get; set; }
        [Display(Name = "Type log")]
        public string LogType { get; set; }
        [Display(Name = "Type sublog")]
        public string LogTypeSub { get; set; }
        public Guid LogTypeId { get; set; }
        public Guid LogTypeSubId { get; set; }
    }
}
