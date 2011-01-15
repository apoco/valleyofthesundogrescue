using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VOTSDR.Utils
{
    public static class StringExtensions
    {
        public static string Summarize(this string str, int? charLimit = null)
        {
            // Get the first "paragraph" of text
            var result = str.Replace("\r\n", "\n");
            var paraEnd = result.IndexOf("\n\n");
            if (paraEnd >= 0)
            {
                result = result.Substring(0, result.IndexOf("\n\n"));
            }

            // If there's a character limit, find the first word boundary before
            // that limit
            if (charLimit.HasValue && result.Length > charLimit)
            {
                result = result.Substring(
                    0, result.LastIndexOf(' ', charLimit.Value)) + "...";
            }
            return result;
        }
    }
}
