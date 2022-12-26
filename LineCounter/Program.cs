using System;
using System.IO;
using System.Text.RegularExpressions;
using CommandLine;
using LineCounter;

Parser.Default.ParseArguments<Options>(args).WithParsed(CountLines);

void CountLines(Options options)
{
    if (options.Verbose)
    {
        Console.Out.WriteLine("Ignoring directories:"u8);

        foreach (string ignore in options.Ignore)
        {
            Console.WriteLine($"- {Path.GetFullPath(ignore)}");
        }
    }

    var regex = new Regex(options.Pattern, RegexOptions.Compiled);
    var searchOption = options.Recurse ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
    var count = 0;

    ReadOnlySpan<char> ignoreChars = options.IgnoreChars.AsSpan();
    string path = Path.GetFullPath(options.Path);

    foreach (string file in Directory.EnumerateFiles(path, "*", searchOption))
    {
        count = CountLinesInFile(file, options, regex, ignoreChars, count);
    }

    Console.ResetColor();

    if (options.Verbose)
    {
        Console.Out.Write("Total line count: "u8);
    }

    Console.Out.WriteLineNoAlloc(count);
}

static int CountLinesInFile(ReadOnlySpan<char> file, Options options, Regex regex, ReadOnlySpan<char> ignoreChars, int count)
{
    ReadOnlySpan<char> directory = Path.GetDirectoryName(file);

    if (directory.IsWhiteSpace())
    {
        if (!options.Verbose)
        {
            return count;
        }

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Out.Write('[');

        Console.ForegroundColor = ConsoleColor.Red;
        Console.Out.Write("NULL"u8);

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Out.Write("] "u8);

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Out.WriteLine(file);
        Console.ResetColor();

        return count;
    }

    ReadOnlySpan<char> directoryFullPath = Path.GetFullPath(directory.ToString());
    var ignore = false;

    foreach (string i in options.Ignore)
    {
        if (!directoryFullPath.StartsWith(Path.GetFullPath(i)))
        {
            continue;
        }

        ignore = true;
        break;
    }

    if (ignore)
    {
        if (!options.Verbose)
        {
            return count;
        }

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Out.Write('[');

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Out.Write("IGNORE"u8);

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Out.Write("] "u8);

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Out.WriteLine(file);
        Console.ResetColor();

        return count;
    }

    if (!regex.IsMatch(file))
    {
        if (!options.Verbose)
        {
            return count;
        }

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Out.Write('[');

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Out.Write("NO_MATCH"u8);

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Out.Write("] "u8);

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Out.WriteLine(file);
        Console.ResetColor();

        return count;
    }

    var fileCount = 0;
    using (var reader = new StreamReader(file.ToString()))
    {
        while (reader.ReadLine() is { } line)
        {
            var lineSpan = line.AsSpan().Trim();
            if (lineSpan.Length == 0)
            {
                continue;
            }

            if (options.IgnoreChars.Length > 0 && ignoreChars.IndexOf(lineSpan[0]) != -1)
            {
                continue;
            }

            fileCount++;
        }
    }

    if (options.Verbose)
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Out.Write('[');

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Out.WriteNoAlloc(fileCount);

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.Out.Write("] "u8);

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Out.WriteLine(file);
        Console.ResetColor();
    }

    count += fileCount;
    return count;
}
