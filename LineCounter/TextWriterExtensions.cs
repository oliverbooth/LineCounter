using System;
using System.IO;
using System.Text;

namespace LineCounter;

internal static class TextWriterExtensions
{
    public static void Write(this TextWriter writer, ReadOnlySpan<byte> bytes)
    {
        Span<char> chars = stackalloc char[bytes.Length];
        Encoding.UTF8.GetChars(bytes, chars);
        writer.Write(chars);
    }

    public static void WriteLine(this TextWriter writer, ReadOnlySpan<byte> bytes)
    {
        Span<char> chars = stackalloc char[bytes.Length];
        Encoding.UTF8.GetChars(bytes, chars);
        writer.WriteLine(chars);
    }

    public static void WriteNoAlloc(this TextWriter writer, int value)
    {
        int digits = NumberUtility.CountDigits(value);
        Span<char> chars = stackalloc char[digits];
        NumberUtility.ToChars(value, chars);
        writer.Write(chars);
    }

    public static void WriteLineNoAlloc(this TextWriter writer, int value)
    {
        int digits = NumberUtility.CountDigits(value);
        Span<char> chars = stackalloc char[digits];
        NumberUtility.ToChars(value, chars);
        writer.WriteLine(chars);
    }
}
