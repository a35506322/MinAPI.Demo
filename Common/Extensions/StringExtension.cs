using System.Globalization;

namespace MinAPI.Demo.Common.Extensions;

public static class StringExtension
{
    public static string ToCamelCase(this string str)
    {
        if (string.IsNullOrEmpty(str) || !char.IsUpper(str[0]))
        {
            return str;
        }

        var chars = str.ToCharArray();

        for (var i = 0; i < chars.Length; i++)
        {
            if (i == 1 && !char.IsUpper(chars[i]))
            {
                break;
            }

            var hasNext = (i + 1 < chars.Length);
            if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
            {
                // if the next character is a space, which is not considered uppercase 
                // (otherwise we wouldn't be here...)
                // we want to ensure that the following:
                // 'FOO bar' is rewritten as 'foo bar', and not as 'foO bar'
                // The code was written in such a way that the first word in uppercase
                // ends when if finds an uppercase letter followed by a lowercase letter.
                // now a ' ' (space, (char)32) is considered not upper
                // but in that case we still want our current character to become lowercase
                if (char.IsSeparator(chars[i + 1]))
                {
                    chars[i] = char.ToLower(chars[i], CultureInfo.InvariantCulture);
                }

                break;
            }

            chars[i] = char.ToLower(chars[i], CultureInfo.InvariantCulture);
        }

        return new string(chars);
    }
}
