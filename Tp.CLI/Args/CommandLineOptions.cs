using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp.CLI.Args
{
    public class CommandLineOptions : Dictionary<string, string>
    {
        public string GetOrNull( string name, params string[] alternativeNames)
        {
            string obj;
            var value = TryGetValue(name, out obj) ? obj : default;
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            if (alternativeNames.Length != 0)
            {
                foreach (var alternativeName in alternativeNames)
                {
                    value = TryGetValue(alternativeName, out obj) ? obj : default;
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        return value;
                    }
                }
            }

            return null;
        }
    }
}
