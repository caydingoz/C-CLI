using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp.CLI.Exceptions.SolutionFileExceptions
{
    [Serializable]
    public class InvalidSolutionNameException : Exception
    {
        public InvalidSolutionNameException() : base() { }
        public InvalidSolutionNameException(string message) : base(message) { }
    }
}
