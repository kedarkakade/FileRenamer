using System;
using System.IO;
using CommandLine;

namespace FileRenamer
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(opts => RunOptionsAndReturnExitCode(opts));
        }

        private static void RunOptionsAndReturnExitCode(Options opts)
        {
            if(opts != null)
            {
                Console.WriteLine("Verbose: {0}", opts.Verbose);
                Console.WriteLine("Recursive: {0}", opts.Recursive);
                Console.WriteLine("Input Extension: {0}", opts.InputExtension);
                Console.WriteLine("Output Extension: {0}", opts.OutputExtension);
                Console.WriteLine("Path: {0}", opts.Path);

                CleanupParameters(ref opts);
                StartProcessingFiles(opts);
            }
        }

        private static void StartProcessingFiles(Options opts)
        {
            var dirInfo = new DirectoryInfo(opts.Path);
            var oldExtension = GetReplaceableFileExtension(opts.InputExtension);
            var files = opts.Recursive? dirInfo.EnumerateFiles(opts.InputExtension, SearchOption.AllDirectories) 
                : dirInfo.EnumerateFiles(opts.InputExtension, SearchOption.TopDirectoryOnly);

            foreach (var file in files)
            {
                var outputFile = file.FullName.Replace(oldExtension, opts.OutputExtension);
                file.MoveTo(outputFile);
            }
        }

        private static void CleanupParameters(ref Options opts)
        {
            opts.InputExtension = GetSearchableFileExtension(opts.InputExtension);

            opts.OutputExtension = GetReplaceableFileExtension(opts.OutputExtension);
        }

        private static string GetSearchableFileExtension(string extension)
        {
            var newExtension = extension;

            if (!newExtension.StartsWith("*") && newExtension.StartsWith("."))
            {
                newExtension = "*" + newExtension;
            }
            else if (!newExtension.StartsWith("*") && !newExtension.StartsWith("."))
            {
                newExtension = "*." + newExtension;
            }

            return newExtension;
        }

        private static string GetReplaceableFileExtension(string extension)
        {
            var newExtension = extension;

            if (newExtension.StartsWith("*") && !newExtension.StartsWith("."))
            {
                newExtension = newExtension.Substring(1);
            }
            else if (!newExtension.StartsWith("."))
            {
                newExtension = "." + newExtension;
            }

            return newExtension;
        }
    }
}
