using System.Globalization;
using System.Text;

namespace HouseWarehouse.Webapp.Models
{
    public class SlugCovert
    {
        public string UrlFriendly(string text, int maxLength = 0)
        {
            if (text == null) return "";

            var normalizedString = text
                .ToLowerInvariant()
                .Normalize(NormalizationForm.FormD);

            var stringBuilder = new StringBuilder();
            var stringLength = normalizedString.Length;
            var prevdash = false;
            var trueLength = 0;

            char c;

            for (int i = 0; i < stringLength; i++)
            {
                c = normalizedString[i];

                switch (CharUnicodeInfo.GetUnicodeCategory(c))
                {
                    case UnicodeCategory.LowercaseLetter:
                    case UnicodeCategory.UppercaseLetter:
                    case UnicodeCategory.DecimalDigitNumber:
                        if (c < 128)
                            stringBuilder.Append(c);
                        else
                            stringBuilder.Append(ConstHelper.RemapInternationalCharToAscii(c));

                        prevdash = false;
                        trueLength = stringBuilder.Length;
                        break;

                    case UnicodeCategory.SpaceSeparator:
                    case UnicodeCategory.ConnectorPunctuation:
                    case UnicodeCategory.DashPunctuation:
                    case UnicodeCategory.OtherPunctuation:
                    case UnicodeCategory.MathSymbol:
                        if (!prevdash)
                        {
                            stringBuilder.Append('-');
                            prevdash = true;
                            trueLength = stringBuilder.Length;
                        }
                        break;
                }

                if (maxLength > 0 && trueLength >= maxLength)
                    break;
            }

            var result = stringBuilder.ToString().Trim('-');

            return maxLength <= 0 || result.Length <= maxLength ? result : result.Substring(0, maxLength);
        }

        public class ConstHelper
        {
            public static string RemapInternationalCharToAscii(char c)
            {
                string s = c.ToString().ToLowerInvariant();
                if ("àåáâäãåą".Contains(s))
                {
                    return "a";
                }
                else if ("èéêëę".Contains(s))
                {
                    return "e";
                }
                else if ("ìíîïı".Contains(s))
                {
                    return "i";
                }
                else if ("òóôõöøőð".Contains(s))
                {
                    return "o";
                }
                else if ("ùúûüŭů".Contains(s))
                {
                    return "u";
                }
                else if ("çćčĉ".Contains(s))
                {
                    return "c";
                }
                else if ("żźž".Contains(s))
                {
                    return "z";
                }
                else if ("śşšŝ".Contains(s))
                {
                    return "s";
                }
                else if ("ñń".Contains(s))
                {
                    return "n";
                }
                else if ("ýÿ".Contains(s))
                {
                    return "y";
                }
                else if ("ğĝ".Contains(s))
                {
                    return "g";
                }
                else if (c == 'ř')
                {
                    return "r";
                }
                else if (c == 'ł')
                {
                    return "l";
                }
                else if (c == 'đ')
                {
                    return "d";
                }
                else if (c == 'ß')
                {
                    return "ss";
                }
                else if (c == 'þ')
                {
                    return "th";
                }
                else if (c == 'ĥ')
                {
                    return "h";
                }
                else if (c == 'ĵ')
                {
                    return "j";
                }
                else
                {
                    return "";
                }
            }
        }
    }
}