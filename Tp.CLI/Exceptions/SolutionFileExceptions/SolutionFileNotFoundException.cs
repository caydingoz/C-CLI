using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp.CLI.Exceptions.SolutionFileExceptions
{
    [Serializable]
    public class SolutionFileNotFoundException : Exception
    {
        public SolutionFileNotFoundException() : base() { }
        public SolutionFileNotFoundException(string message) : base(message) { }
    }
}
