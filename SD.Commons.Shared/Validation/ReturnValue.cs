using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD.Commons.Shared.Validation
{
    /// <summary>
    /// class ReturnValue<T>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReturnValue<T>
    {
        public ReturnValue()
        {
            ErrorMessages = new List<ErrorMessage>();
        }

        public T Value { get; set; }
        public List<ErrorMessage> ErrorMessages { get; set; }

        public Exception exception { get; set; }

        public bool IsValid
        {
            get { return (ErrorMessages.Count == 0); }
        }

        public String ErrorsMerged
        {
            get
            {
                if (ErrorMessages.Any())
                {
                    return string.Join(",", (from e in ErrorMessages select e.Description).ToArray());
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
