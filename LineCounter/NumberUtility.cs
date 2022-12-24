using System;

namespace LineCounter;

internal static class NumberUtility
{
    public static int CountDigits(int value)
    {
        if (value == 0)
        {
            return 1;
        }

        var count = 0;
        var isNegative = false;

        if (value < 0)
        {
            isNegative = true;
            value = -value;
        }

        while (value > 0)
        {
            count++;
            value /= 10;
        }

        return isNegative ? count + 1 : count;
    }

    public static void ToChars(int value, Span<char> buffer)
    {
        if (value == 0)
        {
            buffer[0] = '0';
            return;
        }

        var i = 0;
        var isNegative = false;

        if (value < 0)
        {
            isNegative = true;
            value = -value;
        }

        while (value > 0)
        {
            buffer[i++] = (char)(value % 10 + '0');
            value /= 10;
        }

        if (isNegative)
        {
            buffer[i++] = '-';
        }

        buffer[..i].Reverse();
    }
}
