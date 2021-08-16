using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp.CLI.Args;

namespace Tp.CLI.Commands
{
    public interface ICommandSelector
    {
        public Type Select(CommandLineArgs commandLineArgs);
    }
}
