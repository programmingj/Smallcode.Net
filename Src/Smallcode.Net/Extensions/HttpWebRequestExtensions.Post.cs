using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace Smallcode.Net.Extensions
{
    public static partial class HttpWebRequestExtensions
    {
        public static string Post(this HttpWebRequest request)
        {
            request.AllowWriteStreamBuffering = true; //���������ã���������쳣
            return request
                .Method(HttpWebRequestMethod.POST)
                .GetResponseString();
        }

        /// <summary>
        ///     �ύ��
        /// </summary>
        /// <param name="request">�������</param>
        /// <param name="formData">������</param>
        /// <returns>��Ӧ�ı�</returns>
        public static string Post(this HttpWebRequest request, FormData formData)
        {
            return request
                .Method(HttpWebRequestMethod.POST)
                .SetContentType("application/x-www-form-urlencoded;charset=utf-8")
                .Write(formData)
                .GetResponseString();
        }

        /// <summary>
        ///     �ύ�����ļ�
        /// </summary>
        /// <param name="request">�������</param>
        /// <param name="formData">������</param>
        /// <param name="files">�ļ�</param>
        /// <returns>��Ӧ�ı�</returns>
        public static string Post(this HttpWebRequest request, FormData formData,
                                  IDictionary<string, FileItem> files)
        {
            // ����ָ���
            var boundary = DateTime.Now.Ticks.ToString("X");
            return request
                .Method(HttpWebRequestMethod.POST)
                .SetContentType("multipart/form-data; boundary=" + boundary)
                .Write(formData, files, boundary)
                .GetResponseString();
        }

        /// <summary>
        ///     �ύ�ļ�
        /// </summary>
        /// <param name="request">�������</param>
        /// <param name="fileItem">�ļ�</param>
        /// <returns>��Ӧ�ı�</returns>
        public static string Post(this HttpWebRequest request, FileItem fileItem)
        {
            return request
                .Method(HttpWebRequestMethod.POST)
                .SetContentType("application/octet-stream")
                .Write(fileItem.GetContent())
                .GetResponseString();
        }

        /// <summary>
        ///     д���ύ����
        /// </summary>
        private static HttpWebRequest Write(this HttpWebRequest request, byte[] postData)
        {
            request.ContentLength = postData.Length;
            using (var stream = request.GetRequestStream())
                stream.Write(postData, 0, postData.Length);
            return request;
        }

        /// <summary>
        ///     д���ύ����
        /// </summary>
        private static HttpWebRequest Write(this HttpWebRequest request, FormData formData)
        {
            if (formData.Count > 0)
            {
                //var formDataString = formData.GetFormDataString();
                request.Write(Encoding.UTF8.GetBytes(formData.ToString()));
#if DEBUG
                request.ShowDebugInfo();
                Debug.WriteLine("Form Data:");
                var i = 0;
                foreach (var data in formData)
                {
                    Debug.WriteLine("[{0,-2}] {1,-15} : {2}", ++i, data.Key, data.Value);
                }
#endif
            }
            return request;
        }

        /// <summary>
        ///     д���ύ����
        /// </summary>
        private static HttpWebRequest Write(this HttpWebRequest request,
                                            Dictionary<string, string> formData,
                                            IEnumerable<KeyValuePair<string, FileItem>> files, string boundary)
        {
            request.SendChunked = true; //�ֶη������ݣ���δ�������飬��֪�Ƿ���Ч
            using (var stream = request.GetRequestStream())
            {
                var boundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
                // ��װ�ı��������
                var textPairs = formData.GetEnumerator();
                while (textPairs.MoveNext())
                {
                    var itemBytes =
                        Encoding.UTF8.GetBytes(
                            string.Format(
                                "Content-Disposition:form-data;name=\"{0}\"\r\nContent-Type:text/plain\r\n\r\n{1}",
                                textPairs.Current.Key, textPairs.Current.Value));
                    stream.Write(boundaryBytes, 0, boundaryBytes.Length);
                    stream.Write(itemBytes, 0, itemBytes.Length);
                }

                // ��װ�ļ��������
                var filePairs = files.GetEnumerator();
                while (filePairs.MoveNext())
                {
                    var fileItem = filePairs.Current.Value;
                    var itemBytes =
                        Encoding.UTF8.GetBytes(
                            string.Format(
                                "Content-Disposition:form-data;name=\"{0}\";filename=\"{1}\"\r\nContent-Type:{2}\r\n\r\n",
                                filePairs.Current.Key, fileItem.GetFileName(), fileItem.GetMimeType()));
                    stream.Write(boundaryBytes, 0, boundaryBytes.Length);
                    stream.Write(itemBytes, 0, itemBytes.Length);

                    var fileBytes = fileItem.GetContent();
                    stream.Write(fileBytes, 0, fileBytes.Length);
                }
                // ��װβ��
                var endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                stream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            }
            return request;
        }
    }
}