namespace LineCounter;

using System;
using System.Collections.Generic;
using CommandLine;

/// <summary>
///     Specifies options for the line counter to follow.
/// </summary>
internal sealed class Options
{
    /// <summary>
    ///     Gets or sets a value indicating whether empty lines or lines containing only whitespace are included in the final
    ///     count.
    /// </summary>
    /// <value><see langword="true" /> to include whitespace or empty lines; otherwise, <see langword="false" />.</value>
    [Option('w', "include-whitespace", Required = false, Default = false,
        HelpText = "Include empty lines or lines containing only whitespace in the final count.")]
    public bool Whitespace { get; set; } = false;

    /// <summary>
    ///     Gets or sets the list of directories to ignore.
    /// </summary>
    /// <value>An enumerable collection of strings representing the ignored directories.</value>
    [Option('i', "ignore", Required = false, Default = new string[0], Separator = ';',
        HelpText = "A list of directories to ignore separated by a ; character.")]
    public IEnumerable<string> Ignore { get; set; } = Array.Empty<string>();

    /// <summary>
    ///     Gets or sets the the characters to ignore.
    /// </summary>
    /// <value>A string containing the characters to ignore.</value>
    [Option('c', "ignore-chars", Required = false, Default = "", HelpText = "A list of characters to ignore.")]
    public string IgnoreChars { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the root path to search.
    /// </summary>
    /// <value>The root path.</value>
    [Value(0, Required = false, Default = ".", HelpText = "The path to search.")]
    public string Path { get; set; } = ".";

    /// <summary>
    ///     Gets or sets a regular expression to filter files.
    /// </summary>
    /// <value>The file pattern to filter.</value>
    [Option('p', "pattern", Required = false, Default = "^.+$", HelpText = "Regular expression for matching.")]
    public string Pattern { get; set; } = "^.+$";

    /// <summary>
    ///     Gets or sets a value indicating whether to recursively count lines in child directories.
    /// </summary>
    /// <value>
    ///     <see langword="true" /> to count files in child directories recursively; otherwise, <see langword="false" />.
    /// </value>
    [Option('r', "recurse", Required = false, Default = false, HelpText = "Include recursive file searching.")]
    public bool Recurse { get; set; } = false;

    /// <summary>
    ///     Gets or sets a value indicating whether to output verbose logging.
    /// </summary>
    /// <value><see langword="true" /> to enable verbose logging; otherwise, <see langword="false" />.</value>
    [Option('v', "verbose", Required = false, Default = false, HelpText = "Output detailed information.")]
    public bool Verbose { get; set; } = false;
}
