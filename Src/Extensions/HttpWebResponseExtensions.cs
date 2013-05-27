using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace Smallcode.Net.Extensions
{
    public static class HttpWebResponseExtensions
    {
        private static readonly IDictionary<string, Encoding> CharacterSetCache = new Dictionary<string, Encoding>();
        /// <summary>
        ///     ��ȡ��Ӧ�ַ���
        /// </summary>
        internal static string GetString(this HttpWebResponse response)
        {
            var bytes = response.GetBytes();
            return CharacterSetCache.ContainsKey(response.ResponseUri.Host) ? CharacterSetCache[response.ResponseUri.Host].GetString(bytes) : response.GetString(bytes);
        }

        private static string GetString(this HttpWebResponse response, byte[] bytes)
        {
            var encoding = response.GetEncoding();
            var result = encoding.GetString(bytes);
            if (response.IsEmptyCharacterSet())
            {
                string charset;
                if (result.TryGetMatchValue(@"charset\b\s*=\s*(?<charset>[^""]+)", out charset))
                {
                    var charsetEncoding = Encoding.GetEncoding(charset);
                    if (!charsetEncoding.Equals(encoding))
                    {
                        CharacterSetCache[response.ResponseUri.Host] = encoding;
                        return charsetEncoding.GetString(bytes);
                    }
                }
            }
            CharacterSetCache[response.ResponseUri.Host] = encoding;
            return result;
        }

        /// <summary>
        ///     ��ȡ��ת��ַ
        /// </summary>
        internal static string GetRedirectLocation(this HttpWebResponse response)
        {
            return response.Headers[HttpResponseHeader.Location] ?? string.Empty;
        }

        /// <summary>
        ///     ��ȡ��Ӧ�ֽ�����
        /// </summary>
        public static byte[] GetBytes(this HttpWebResponse response)
        {
            using (var stream = response.GetResponseStream())
            {
                if (response.ContentEncoding.ToLower().Contains("gzip"))
                {
                    using (var gzip = new GZipStream(stream, CompressionMode.Decompress))
                    {
                        return GetBytes(gzip);
                    }
                }
                if (response.ContentEncoding.ToLower().Contains("deflate"))
                {
                    using (var deflate = new DeflateStream(stream, CompressionMode.Decompress))
                    {
                        return GetBytes(deflate);
                    }
                }
                return GetBytes(stream);
            }
        }

        /// <summary>
        ///     ��ȡ��Ӧ�ֽ�����
        /// </summary>
        private static byte[] GetBytes(Stream stream)
        {
            var buffer = new byte[8192];
            using (var ms = new MemoryStream())
            {
                int bytesRead;
                while (stream != null && (bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, bytesRead);
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        ///     ��ȡ���룬ͨ����������Զ���ȡ���룬����Ҫ����UTF8��GBK������ͷ��Ϣ��ʵ��ҳ��Դ��Ľ��벻ͬ�����HTML��HEADͷ����ȡ
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        internal static Encoding GetEncoding(this HttpWebResponse response)
        {
            return response.IsEmptyCharacterSet() ? UrlEncoding.GB2312.ToEncoding() : Encoding.GetEncoding(response.CharacterSet);
        }

        /// <summary>
        /// �ж��Ƿ��ޱ���
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        internal static bool IsEmptyCharacterSet(this HttpWebResponse response)
        {
            return string.IsNullOrWhiteSpace(response.CharacterSet) || response.CharacterSet.Equals("ISO-8859-1", StringComparison.OrdinalIgnoreCase);
        }
    }
}