using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections;


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
        
        public static string UrlEncode(this String s)
        {
            return HttpUtility.UrlEncode(s);
        }

        public static string HtmlEncode(this String s)
        {
            return HttpUtility.HtmlEncode(s);
        }

        public static bool IsPopulated(this String s)
        {
            return !String.IsNullOrWhiteSpace(s);
        }
        public static bool IsNotPopulated(this String s)
        {
            return String.IsNullOrWhiteSpace(s);
        }
        public static Int16 ToInt16(this String s)
        {
            if (String.IsNullOrEmpty(s)) return 0;
            Int16 val;
            Int16.TryParse(s, out val);
            return val;
        }
        public static UInt32 ToUInt32(this String s)
        {
            if (String.IsNullOrEmpty(s)) return 0;
            UInt32 val;
            UInt32.TryParse(s, out val);
            return val;
        }
        public static Byte ToByte(this String s)
        {
            if (String.IsNullOrEmpty(s)) return 0;
            Byte val;
            Byte.TryParse(s, out val);
            return val;
        }
        public static Boolean ToBoolean(this String s)
        {
            if (String.IsNullOrEmpty(s)) return false;
            Boolean val;
            Boolean.TryParse(s, out val);
            return val || s == "1";
        }
        public static Int32 ToInt32(this String s)
        {
            if (String.IsNullOrEmpty(s)) return 0;
            Int32 val;
            Int32.TryParse(s, out val);
            return val;
        }
        public static Int64 ToInt64(this String s)
        {
            if (String.IsNullOrEmpty(s)) return 0;
            Int64 val;
            Int64.TryParse(s, out val);
            return val;
        }
        public static DateTime ToDateTime(this String s)
        {
            if (String.IsNullOrEmpty(s)) return new DateTime();
            DateTime val;
            DateTime.TryParse(s, out val);
            return val;
        }


        /// <summary>
        /// Joins the specified items in the enumerable arrat
        /// </summary>
        /// <param name="items">An enumerable listing of string items.</param>
        /// <param name="separator">The separator to place between each.</param>
        /// <returns>A string of the joined list.</returns>
        public static string JoinToString(this IEnumerable items, string separator)
        {
            var sb = new StringBuilder();
            foreach (var item in items)
            {
                if (sb.Length > 0)
                {
                    sb.Append(separator);
                }
                sb.Append(item);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Appends the format line.
        /// </summary>
        /// <param name="sb">The sb.</param>
        /// <param name="str">The STR.</param>
        /// <param name="args">The args.</param>
        public static void AppendLine(this StringBuilder sb, string str, params object[] args)
        {
            if (args == null || args.Length == 0)
            {
                sb.AppendLine(str);
            }
            else
            {
                sb.AppendFormat(str, args);
                sb.AppendLine();
            }
        }

    }
}
