using System;
using System.Collections.Specialized;
using System.Text;
using System.Text.RegularExpressions;

using Microsoft.Win32.SafeHandles;

namespace Com.BaiZe.SharpToolSet
{
    public static class StringExtension
    {
        #region 字符处理
        // 是否包含中文
        public static bool HasChinese(this string str)
        {
            return Regex.IsMatch(str, "[\u4e00-\u9fa5]");
        }

        // 是否只包含字母
        public static bool HasAlpha(this string str)
        {
            return Regex.IsMatch(str, "[a-zA-Z]");
        }

        // 是否包含数字
        public static bool HasDigit(this string str)
        {
            return Regex.IsMatch(str, "[0-9]");
        }

        // 是否全中文
        public static bool IsChinese(this string str)
        {
            foreach (char c in str)
            {
                if (!Regex.IsMatch(c.ToString(), "[\u4e00-\u9fa5]"))
                {
                    return false;
                }
            }
            return true;
        }

        // 是否全字母
        public static bool IsAlpha(this string str)
        {
            foreach (char c in str)
            {
                if (!Char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }

        // 是否全数字
        public static bool IsDigit(this string str)
        {
            foreach (char c in str)
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        // 剔除中文字符
        public static string RidChinese(this string str)
        {
            return Regex.Replace(str, "[\u4e00-\u9fa5]", "", RegexOptions.IgnoreCase);
        }

        // 保留字母、数字、下划线
        public static string ToVaildName(this string str)
        {
            var res = Regex.Replace(str, @"[\W+]", "", RegexOptions.IgnoreCase);
            // 剔除中文
            return res.RidChinese();
        }

        public static string Upper(this string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var c in str)
            {
                var cc = c;
                if (Char.IsLetter(c))
                {
                    cc = Char.ToUpper(c);
                }
                sb.Append(cc);
            }
            return sb.ToString();
        }
        # endregion

        #region 字符串格式化
        /// Pascal命名规则
        public static string PascalFormat(this string str)
        {
            return str.Substring(0, 1).ToUpper() + str.Substring(1);
        }

        // 格式化字符串扩展方法
        public static string Format(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        // 是否以xx格式为前缀
        public static bool IsPrefixWith(this string str, string prefix)
        {
            return str.IndexOf(prefix) == 0;
        }

        // 是否以xx格式为后缀
        public static bool IsSuffixWith(this string str, string suffix)
        {
            return str.LastIndexOf(suffix) == str.Length - suffix.Length;
        }

        // 提取前缀
        public static string Prefix(this string str, string sep)
        {
            int index = str.IndexOf(sep);
            return str.Substring(0, index < 0 ? str.Length : index);
        }

        // 提取后缀
        public static string Suffix(this string str, string sep)
        {
            int index = str.LastIndexOf(sep);
            return str.Substring(index < 0 ? str.Length : index + 1);
        }


        // 去除匹配前缀
        public static string TrimPrefix(this string str, string sep)
        {
            if (string.IsNullOrEmpty(str)) return str;
            int index = str.IndexOf(sep);
            string subPath = str.Substring(index + sep.Length);
            return subPath;
        }

        // 去除匹配后缀
        public static string TrimSuffix(this string str, string sep)
        {
            if (string.IsNullOrEmpty(str)) return str;
            int index = str.LastIndexOf(sep);
            string subPath = str.Substring(0, index);
            return subPath;
        }

        public static string Endl(this string str)
        {
            return str + "\n";
        }
        #endregion

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string TabFormat(this string str, int count = 1)
        {
            var temp = str;
            for (int i = 1; i <= count; ++i)
            {
                temp = "    " + temp;
            }
            return temp;
        }
    }
}