using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp.CLI.Exceptions.SolutionFileExceptions
{
    [Serializable]
    public class MultipleSolutionFileException : Exception
    {
        public MultipleSolutionFileException() : base() { }
        public MultipleSolutionFileException(string message) : base(message) { }
    }
}
