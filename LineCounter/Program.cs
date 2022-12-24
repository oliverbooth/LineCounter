using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Colorful;
using CommandLine;
using LineCounter;
using Console = Colorful.Console;

await Parser.Default.ParseArguments<Options>(args).WithParsedAsync(CountLinesAsync).ConfigureAwait(true);

static async Task CountLinesAsync(Options options)
{
    if (options.Verbose)
    {
        Console.WriteLine("Ignoring directories:");
        foreach (string ignore in options.Ignore.Select(Path.GetFullPath))
        {
            Console.WriteLine($"- {ignore}");
        }
    }

    var regex = new Regex(options.Pattern, RegexOptions.Compiled);
    string path = Path.GetFullPath(options.Path);
    var searchOption = options.Recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
    string[] files = Directory.GetFiles(path, "*", searchOption);
    var count = 0;

    foreach (string file in files)
    {
        string directory = Path.GetDirectoryName(file);
        if (directory is null)
        {
            if (options.Verbose)
            {
                var formatter = new[] {new Formatter("NULL", Color.Red), new Formatter(file, Color.LightGray)};
                Console.WriteLineFormatted("[{0}] {1}", Color.Gray, formatter);
            }

            continue;
        }

        if (options.Ignore.Select(Path.GetFullPath).Any(i => directory.StartsWith(i)))
        {
            if (options.Verbose)
            {
                var formatter = new[] {new Formatter("IGNORE", Color.Orange), new Formatter(file, Color.LightGray)};
                Console.WriteLineFormatted("[{0}] {1}", Color.Gray, formatter);
            }

            continue;
        }

        if (!regex.IsMatch(file))
        {
            if (options.Verbose)
            {
                var formatter = new[] {new Formatter("NO_MATCH", Color.Yellow), new Formatter(file, Color.LightGray)};
                Console.WriteLineFormatted("[{0}] {1}", Color.Gray, formatter);
            }

            continue;
        }

        var lines = (await File.ReadAllLinesAsync(file).ConfigureAwait(false)) as IEnumerable<string>;

        if (!options.Whitespace)
        {
            lines = lines.Where(line => !string.IsNullOrWhiteSpace(line));
        }

        if (!string.IsNullOrWhiteSpace(options.IgnoreChars))
        {
            lines = lines.Where(line => line.Trim().Length > 0 && options.IgnoreChars.IndexOf(line.Trim()[0]) != 0);
        }

        int fileCount = lines.Count();
        if (options.Verbose)
        {
            var formatter = new[] {new Formatter(fileCount, Color.Cyan), new Formatter(file, Color.LightGray)};
            Console.WriteLineFormatted("[{0}] {1}", Color.Gray, formatter);
        }

        count += fileCount;
    }

    Console.ResetColor();

    if (options.Verbose)
    {
        Console.Write("Total line count: ");
    }

    Console.WriteLine(count);
}
