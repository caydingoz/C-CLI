using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp.CLI.Exceptions.APIExceptions
{
    [Serializable]
    public class HttpRequestFailedException : Exception
    {
        public HttpRequestFailedException() : base() { }
        public HttpRequestFailedException(string message) : base(message) { }
    }
}
