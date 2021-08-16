using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tp.CLI.Args;

namespace Tp.CLI.Commands
{
    public class CommandSelector : ICommandSelector
    {
        public Dictionary<string, Type> commands { get; }

        public CommandSelector()
        {
            commands = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
            setCommands();
        }
        public Type Select(CommandLineArgs commandLineArgs)
        {
            return commands[commandLineArgs.Command];
        }

        private void setCommands()
        {
            commands["new"] = typeof(NewCommand);
            commands["add-gateway"] = typeof(AddGatewayCommand);
        }
    }
}
