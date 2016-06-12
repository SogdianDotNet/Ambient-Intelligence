using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Commons.Shared.Validation
{
    /// <summary>
    /// class ErrorMessage
    /// </summary>
    public class ErrorMessage
    {
        public ErrorMessage()
        {
        }
        public ErrorMessage(string message)
        {
            Message = message;
            Description = message;
        }
        public ErrorMessage(string message, string description)
        {
            Message = message;
            Description = description;
        }
        public ErrorMessage(string message, string description, int linenumber)
        {
            Message = message;
            Description = description;
            Linenumber = linenumber;
        }

        public ErrorMessage(string message, int linenumber)
        {
            Message = message;
            Description = message;
            Linenumber = linenumber;
        }

        public int Linenumber { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
        public Exception Exception { get; set; }
    }
}
