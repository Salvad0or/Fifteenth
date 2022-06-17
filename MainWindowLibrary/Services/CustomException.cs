using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWindowLibrary.Services
{
    public class CustomException : Exception
    {

        public override string Message { get; }
        public int ErrorCode { get; set; }


        public CustomException(string exceptionMessage, int error)
        {
            Message = exceptionMessage;
            ErrorCode = error;
        }
    }
}
