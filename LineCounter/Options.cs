namespace LineCounter
{
    using System;
    using System.Collections.Generic;
    using CommandLine;

    internal class Options
    {
        /// <summary>
        /// Gets or sets the <c>include-whitespace</c> flag value.
        /// </summary>
        [Option('w', "include-whitespace", Required = false, Default = false,
            HelpText = "Include empty lines or lines containing only whitespace in the final count.")]
        public bool Whitespace { get; set; } = false;

        /// <summary>
        /// Gets or sets the <c>ignore</c> flag value.
        /// </summary>
        [Option('i', "ignore", Required = false, Default = new string[0], Separator = ';',
            HelpText = "A list of directories to ignore separated by a ; character.")]
        public IEnumerable<string> Ignore { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Gets or sets the <c>ignore-chars</c> flag value.
        /// </summary>
        [Option('c', "ignore-chars", Required = false, Default = "",
            HelpText = "A list of characters to ignore.")]
        public string IgnoreChars { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the path value.
        /// </summary>
        [Value(0, Required = false, Default = ".",
            HelpText = "The path to search.")]
        public string Path { get; set; } = ".";

        /// <summary>
        /// Gets or sets the <c>pattern</c> flag value.
        /// </summary>
        [Option('p', "pattern", Required = false, Default = "^.+$",
            HelpText = "Regular expression for matching.")]
        public string Pattern { get; set; } = "^.+$";

        /// <summary>
        /// Gets or sets a value indicating whether the <c>recurse</c> flag was set.
        /// </summary>
        [Option('r', "recurse", Required = false, Default = false,
            HelpText = "Include recursive file searching.")]
        public bool Recurse { get; set; } = false;

        [Option('v', "verbose", Required = false, Default = false,
            HelpText = "Output detailed information.")]
        public bool Verbose { get; set; } = false;
    }
}
