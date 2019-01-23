using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace FileRenamer
{
    public class Options
    {
        [Option('v', Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }

        [Option('r', Required = false, HelpText = "Scans current folder as well as sub folders recursively.")]
        public bool Recursive { get; set; }

        [Option('p', Required = true, HelpText = "Set input/source folder path.")]
        public string Path { get; set; }

        [Option('i', Required = true, HelpText = "Input file extension. e.g. \".js\".")]
        public string InputExtension { get; set; }

        [Option('o', Required = true, HelpText = "Output file extension. e.g. \".js.txt\".")]
        public string OutputExtension { get; set; }

        [Usage(ApplicationAlias = "FileRenamer")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                return new List<Example>()
                {
                    new Example("\nNormal Mode:", new Options { InputExtension=".js", OutputExtension=".js.txt", Path= @"C:\tmp\" } ),
                    new Example("\nRecursive Mode:", new Options { InputExtension=".js", OutputExtension=".js.txt", Path=@"C:\tmp\", Recursive=true })
                };
            }
        }
    }
}
