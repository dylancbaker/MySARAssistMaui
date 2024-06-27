using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MySARAssist.Models
{
    public static class StringExt
    {
        public static string? removeBadChrsForQR(this string? str)
        {
            string bad = "~^;";
            if (!string.IsNullOrEmpty(str))
            {
                StringBuilder output = new StringBuilder(str.Length);

                foreach (char c in str)
                {
                    bool badchr = false;
                    foreach (char bc in bad)
                    {
                        if (c == bc) { badchr = true; }
                    }
                    if (!badchr) { output.Append(c); }

                }

                return output.ToString();
            }
            else { return null; }
        }

        public static bool Contains(this List<string> source, string toCheck, StringComparison comp)
        {
            return source.Any(o => o.Contains(toCheck, comp));
        }
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }

        public static bool EqualsWithNull(this string str1, string str2)
        {
            if (string.IsNullOrEmpty(str1) && string.IsNullOrEmpty(str2)) { return true; }
            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2)) { return false; }
            return str1.Equals(str2, StringComparison.InvariantCultureIgnoreCase);
        }

        public static object ValueOrDBNull(this string str)
        {
            if (str == null) { return DBNull.Value; }
            else { return str; }
        }
        public static bool IsNumeric(this string str)
        {
            decimal d;
            return decimal.TryParse(str, out d);
        }

        public static string ReplaceInvalidPathChars(this string filename, string replaceChar = "-")
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            return r.Replace(filename, replaceChar);
        }

        public static string? EscapeQuotes(this string? str)
        {
            if (string.IsNullOrEmpty(str)) { return str; }
            else { return str.Replace("\"", "\"\""); }
        }

        public static List<string> ListFromCSV(this string str)
        {
            List<string> list = new List<string>();
            string[] result = str.Split(',');
            foreach (string s in result)
            {
                if (!string.IsNullOrEmpty(s.Trim())) { list.Add(s.Trim()); }
            }
            return list;
        }

        public static string shortenFileName(this string text)
        {
            string shortText = null;
            if (text.Length > 100)
            {
                shortText = text.Substring(0, 3);
                shortText += "...";
                string[] parts = text.Split('\\');
                if (parts.Length > 2)
                {
                    for (int x = parts.Length - 2; x < parts.Length; x++)
                    {
                        shortText += "\\";
                        shortText += parts[x];
                    }
                }
                else
                {
                    shortText += "\\";
                    shortText += parts[parts.Length - 1];
                }

            }
            else
            {
                shortText = text;
            }


            return shortText;
        }
        public static string FormatPhone(this string str)
        {
            if (string.IsNullOrEmpty(str)) { return str; }

            string tmp = str.Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "");
            if (tmp.Length == 11 && tmp.Substring(0, 1) == "1") { tmp = tmp.Substring(1, 10); }
            if (tmp.Length == 10)
            {
                return "(" + tmp.Substring(0, 3) + ") " + tmp.Substring(3, 3) + "-" + tmp.Substring(6);
            }
            else { return str; }
        }

        public static string Sanitize(this string s, char subchar = ' ', bool skip = true)
        {
            string admitted = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890-_+ ";
            StringBuilder output = new StringBuilder(s.Length);
            bool found = false;

            foreach (char c in s)
            {
                found = false;
                foreach (char adm in admitted)
                {
                    if (c == adm)
                    {
                        found = true;
                        output.Append(c);
                    }
                }

                if (found == false)
                {
                    if (!skip)
                        output.Append(subchar);
                }
            }

            return output.ToString();
        }

        public static bool isValidEmail(this string str)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(str);
                return addr.Address == str;
            }
            catch
            {
                return false;
            }
        }

        public static bool isValidURL(this string str)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(str, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
        }

        public static string XmlSerializeToString(this object objectInstance)
        {
            var serializer = new XmlSerializer(objectInstance.GetType());
            var sb = new StringBuilder();

            using (TextWriter writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, objectInstance);
            }

            return sb.ToString();
        }

        public static T XmlDeserializeFromString<T>(this string objectData)
        {
            return (T)XmlDeserializeFromString(objectData, typeof(T));
        }

        public static object? XmlDeserializeFromString(this string objectData, Type type)
        {
            var serializer = new XmlSerializer(type);
            object? result;

            using (TextReader reader = new StringReader(objectData))
            {
                result = serializer.Deserialize(reader);
            }

            return result;
        }

        public static string LoremIpsum(int minWords, int maxWords,
     int minSentences, int maxSentences,
     int numParagraphs)
        {

            var words = new[]{"lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
        "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
        "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"};

            var rand = new Random();
            int numSentences = rand.Next(maxSentences - minSentences)
                + minSentences + 1;
            int numWords = rand.Next(maxWords - minWords) + minWords + 1;

            StringBuilder result = new StringBuilder();

            for (int p = 0; p < numParagraphs; p++)
            {
                //result.Append("<p>");
                for (int s = 0; s < numSentences; s++)
                {
                    for (int w = 0; w < numWords; w++)
                    {
                        if (w > 0) { result.Append(" "); }
                        result.Append(words[rand.Next(words.Length)]);
                    }
                    result.Append(". ");
                }
                //result.Append("</p>");
            }

            return result.ToString();
        }
    }
}
