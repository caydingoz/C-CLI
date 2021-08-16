using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp.CLI.Args
{
    public static class CommandLineParser
    {
        public static CommandLineArgs Parse(string[] args)
        {
            var argumentList = args.ToList();

            //Command

            var command = argumentList[0];  // add-gateway -n myApi, new api -n myApi
            argumentList.RemoveAt(0);       // -n myGateway, api -n myApi

            if (!argumentList.Any())        // bossa
            {
                return new CommandLineArgs(command);
            }

            //TemplateName

            var projectType = argumentList[0];   // eger new commandi ise
            if (projectType.StartsWith("-"))
            {
                projectType = null;
            }
            else
            {
                argumentList.RemoveAt(0);   // -n myGateway, -n myApi
            }

            if (!argumentList.Any())        // bossa
            {
                return new CommandLineArgs(command, projectType);
            }

            //Options

            var commandLineArgs = new CommandLineArgs(command, projectType);
            while (argumentList.Any())      // bos olmadigi surece parsela
            {
                var optionName = ParseOptionName(argumentList[0]);  //Option name buluyoruz -n -s falan
                argumentList.RemoveAt(0);   // myGateway, myApi kaldi

                if (!argumentList.Any())    //bossa
                {
                    commandLineArgs.Options[optionName] = null;     // -n yazip birakmissak null
                    break;
                }

                if (IsOptionName(argumentList[0]))      // bir option mi? -s, -v gibi ardarda yazabildigimiz icin -s -n gibi
                {
                    commandLineArgs.Options[optionName] = null; //öyle ise null
                    continue;
                }

                commandLineArgs.Options[optionName] = argumentList[0];
                argumentList.RemoveAt(0);
            }
            return commandLineArgs;
        }
        private static bool IsOptionName(string argument)
        {
            return argument.StartsWith("-") || argument.StartsWith("--");
        }
        private static string ParseOptionName(string argument)
        {
            if (argument.StartsWith("--"))  //sadece -- ise 
            {
                if (argument.Length <= 2)
                {
                    throw new ArgumentException("Should specify an option name after '--' prefix!");
                }

                return argument.Remove(0, 2);
            }

            if (argument.StartsWith("-"))
            {
                if (argument.Length <= 1)   //sadece - ise 
                {
                    throw new ArgumentException("Should specify an option name after '-' prefix!");
                }

                return argument.Remove(0, 1);
            }

            throw new ArgumentException("Option names should start with '-' or '--'.");
        }
    }
}
