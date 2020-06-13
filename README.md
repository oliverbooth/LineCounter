# 💻 LineCounter
[<img src="https://img.shields.io/github/workflow/status/oliverbooth/LineCounter/.NET Core">](https://github.com/oliverbooth/LineCounter/actions?query=workflow%3A%22.NET+Core%22)
[<img src="https://img.shields.io/github/downloads/oliverbooth/LineCounter/total">](https://github.com/oliverbooth/LineCounter/releases)

LineCounter (`lc`) is a command-line utility for counting lines in files.

## Installation
### Windows
Download the [latest release](https://github.com/oliverbooth/LineCounter/releases/atest), and ensure `lc.exe` is somewhere in `%PATH%`

### Linux / macOS
Download the [latest release](https://github.com/oliverbooth/LineCounter/releases/atest), and ensure `lc` is somewhere in `$PATH` or create a symbolic link in `/usr/bin`

## Usage
To count lines in all files in the current directory, just run the command!
```bash
$ lc
```
To count lines in all files in the current directory and subdirectories, add the recurse flag (or `r` for short)
```bash
$ lc --recurse
# or
$ lc -r
```
You can also optionally specify a regular expression as a file matching pattern. For example, recursively counting all the lines in every `.cs` file can be done like so:
```bash
$ lc --recurse --pattern "\.cs$"
# or
$ lc -rp "\.cs$"
```

### Counting files in other directories
You can optionally specify a path to search, instead of the current directory
```bash
$ lc ./src
```

### Including whitespace-only lines
By default, this tool does not count every line. Lines that are empty or contain only whitespace are ignored. You can bypass this with the `--include-whitespace` or `-w` flag
```bash
$ lc ./src --include-whitespace --recurse --pattern "\.cs$"
# or
$ lc ./src -wrp "\.cs$"
```

### Ignoring directories
To ignore directories in the count, specify a semicolon-separated list of folder names to be ignored by using the `--ignore` or `-i` flag
```bash
$ lc ./src --include-whitespace --recurse --pattern "\.cs$" --ignore "Resources;Properties"
# or
$ lc ./src -wrp "\.cs$" -i "Resources;Properties"
```

### Ignoring single-character lines
To ignore lines that contain only specific characters (for example, ignoring lines which contain only `{` or `}`), use the `--ignore-chars` or `-c` flag
```bash
$ lc ./src --include-whitespace --recurse --pattern "\.cs$" --ignore "Resources;Properties" --ignore-chars "{}"
# or
$ lc ./src -wrp "\.cs$" -i "Resources;Properties" -c "{}"
```

## Summary
```
-w, --include-whitespace    (Default: false) Include empty lines or lines containing only whitespace in the final
                            count.

-i, --ignore                (Default: ) A list of directories to ignore separated by a ; character.

-c, --ignore-chars          (Default: ) A list of characters to ignore.

-p, --pattern               (Default: ^.+$) Regular expression for matching.

-r, --recurse               (Default: false) Include recursive file searching.

-v, --verbose               (Default: false) Output detailed information.

--help                      Display the help screen.

--version                   Display version information.
```

## Disclaimer
This tool is provided as-is and by running it you accept any and all liability should the tool behave unexpectedly.