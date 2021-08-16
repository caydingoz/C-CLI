using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp.CLI.Args
{
    public class CommandLineArgs
    {
        public string Command { get; }
        public string TemplateName { get; }
        public CommandLineOptions Options { get; }  // dictionary yeterli olmuyor long ve short opt nameler oldugundan

        public CommandLineArgs(string command = null, string templateName = null)
        {
            Command = command;
            TemplateName = templateName;
            Options = new CommandLineOptions();
        }
    }
}
