using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.CoreService.Infrastructure.Exceptions
{
    public class TemporaryPasswordException : Exception
    {
        public TemporaryPasswordException()
            : base("You are using a temporary password. Reset your password.") 
        {
        }
        public TemporaryPasswordException(string message)
            : base(message) 
        {
        }
        public TemporaryPasswordException(string message, Exception innerException)
            : base(message, innerException) 
        {
        }
    }
}
