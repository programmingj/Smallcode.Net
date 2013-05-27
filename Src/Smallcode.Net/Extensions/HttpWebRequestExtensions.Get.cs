using System;
using System.Net;

namespace Smallcode.Net.Extensions
{
    public static partial class HttpWebRequestExtensions
    {
        /// <summary>
        ///     ��ȡ�ı���ʽ��Get��Ӧ��
        /// </summary>
        /// <param name="request">�������</param>
        /// <returns>��Ӧ�ı�</returns>
        public static string Get(this HttpWebRequest request)
        {
            return request
                .Method(HttpWebRequestMethod.GET)
                .GetResponseString();
        }

        /// <summary>
        ///     ��ȡ�ֽ�������ʽ��Get��Ӧ��
        /// </summary>
        /// <param name="request">�������</param>
        /// <returns>�ֽ�����</returns>
        public static byte[] GetBytes(this HttpWebRequest request)
        {
            using (var response = request.Method(HttpWebRequestMethod.GET).GetHttpResponse())
            {
                return response.GetBytes();
            }
        }
    }
}