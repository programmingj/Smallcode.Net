using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Smallcode.Net.Extensions
{
    public static partial class StringExtensions
    {
        /// <summary>
        ///     ��ȡ�ַ���
        /// </summary>
        /// <param name="source">Դ�ַ���</param>
        /// <param name="startTag">��ʼ���</param>
        /// <param name="endTag">�������</param>
        /// <param name="isContainsStartTag">��ȡ��������Ƿ������ʼ���</param>
        /// <param name="isContainsEndTag">��ȡ��������Ƿ�����������</param>
        /// <returns>����ȡ��������</returns>
        public static string CutString(this string source, string startTag, string endTag,
                                       bool isContainsStartTag = false, bool isContainsEndTag = false)
        {
            var start = source.IndexOf(startTag, StringComparison.Ordinal);
            if (start < 0) return string.Empty;
            if (!isContainsStartTag) start += startTag.Length;
            var end = source.LastIndexOf(endTag, StringComparison.Ordinal);
            if (end < 0) return string.Empty;
            if (isContainsEndTag) end += endTag.Length;
            var length = end - start;
            return length > 0 ? source.Substring(start, length).Trim() : string.Empty;
        }

        /// <summary>
        ///     �Ƿ�Ϊ��
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        ///     �Ƿ�Ϊ��
        /// </summary>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        ///     �Ƿ�Ϊ����
        /// </summary>
        public static bool IsNumberic(this string text)
        {
            return new Regex(@"^\d+$").IsMatch(text);
        }

        ///// <summary>
        /////     �����ַ������ֽڳ��ȵ�һ�룬����������һ�������������ȫ����һ����
        ///// </summary>
        //public static int HalfOfByteCount(this string source)
        //{
        //    var isTwo = Encoding.Default.GetByteCount(source) % 2 == 0;
        //    var count = Encoding.Default.GetByteCount(source) / 2;
        //    return isTwo ? count : count + 1;
        //}

        /// <summary>
        /// ��ʽ���ַ���
        /// </summary>
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        //���������ǳ���Ч�ܿ���
        public static string FormatWith(this string format, object arg0)
        {
            return string.Format(format, arg0);
        }

        public static string FormatWith(this string format, object arg0, object arg1)
        {
            return string.Format(format, arg0, arg1);
        }

        public static string FormatWith(this string format, object arg0, object arg1, object arg2)
        {
            return string.Format(format, arg0, arg1, arg2);
        }
    }
}