using System;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace Smallcode.Net.Extensions
{
    public static partial class HttpWebRequestExtensions
    {
        /// <summary>
        ///     ��ȡ��Ӧ�ַ��������ַ����ķ�ʽ��ȡHTTP��Ӧ
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static string GetResponseString(this HttpWebRequest request)
        {
            using (var response = request.GetHttpResponse())
                return request.AllowAutoRedirect ? response.GetString() : response.GetRedirectLocation();
        }

        /// <summary>
        ///     ��ȡHttpResponse
        /// </summary>
        /// <param name="request">request</param>
        /// <returns></returns>
        public static HttpWebResponse GetHttpResponse(this HttpWebRequest request)
        {
#if DEBUG
            if (request.Method != HttpWebRequestMethod.POST.ToString() || request.ContentType == null || !request.ContentType.StartsWith("application/x-www-form-urlencoded"))
                request.ShowDebugInfo();
#endif
            try
            {
                return (HttpWebResponse)request.GetResponse();
            }
            catch (WebException we)
            {
                if (we.Response != null)
                {
                    return (HttpWebResponse)we.Response;
                }
                if (we.Status == WebExceptionStatus.NameResolutionFailure)
                {
                    throw new WebException("�����޷�����", we);
                }
                if (we.Status == WebExceptionStatus.Timeout)
                {
                    throw new TimeoutException("�������ӳ�ʱ", we);
                }
                throw;
            }
        }

        /// <summary>
        /// ��ȡCookiesֵ
        /// </summary>
        public static string GetCookie(this HttpWebRequest request, string name)
        {
            if (request.CookieContainer == null) throw new CookieException();
            var cookie = request.CookieContainer.GetCookies(request.RequestUri)[name];
            return cookie != null ? cookie.Value : string.Empty;
        }

        /// <summary>
        /// ����Cookiesֵ
        /// </summary>
        public static HttpWebRequest SetCookie(this HttpWebRequest request, string name, string value)
        {
            if (request.CookieContainer == null) throw new CookieException();
            request.CookieContainer.Add(request.RequestUri, new Cookie(name, value));
            return request;
        }

        /// <summary>
        /// ��ʾ��ʽ��Ϣ
        /// </summary>
        /// <param name="request"></param>
        private static void ShowDebugInfo(this HttpWebRequest request)
        {
            Debug.WriteLine("{0}{1}:{2}{3}", Environment.NewLine, request.Method, request.RequestUri, string.IsNullOrWhiteSpace(request.Referer) ? String.Empty : string.Format("{0}{1}:{2}", Environment.NewLine, "Referer", request.Referer));
        }

        private static HttpWebRequest Method(this HttpWebRequest request, HttpWebRequestMethod method)
        {
            request.Method = method.ToString();
            return request;
        }

        private static HttpWebRequest SetContentType(this HttpWebRequest request, string contentType)
        {
            request.ContentType = contentType;
            return request;
        }

        public static HttpWebRequest WithCookies(this HttpWebRequest request, CookieContainer cookieContainer)
        {
            request.CookieContainer = cookieContainer;
            return request;
        }

        public static HttpWebRequest WithReferer(this HttpWebRequest request, string referer, params object[] args)
        {
            request.Referer = args.Length > 0 ? string.Format(referer, args) : referer;
            return request;
        }

        public static HttpWebRequest WithUserAgent(this HttpWebRequest request, string userAgent = HttpWebRequestUserAgent.IE)
        {
            request.UserAgent = userAgent;
            return request;
        }

        public static HttpWebRequest SetTimeout(this HttpWebRequest request, int milliseconds)
        {
            request.Timeout = request.ReadWriteTimeout = milliseconds;
            return request;
        }

        public static HttpWebRequest SetAcceptEncoding(this HttpWebRequest request, string encoding = "gzip,deflate")
        {
            //ע�⣺�����HttpRequestHeader.AcceptEncoding������string����������޷����ѹ������ҳ
            return request.WithHeader(HttpRequestHeader.AcceptEncoding, encoding);
        }

        public static HttpWebRequest ByAjax(this HttpWebRequest request)
        {
            return request.WithHeader("X-Requested-With", "XMLHttpRequest");
        }

        public static HttpWebRequest NotRedirect(this HttpWebRequest request)
        {
            request.AllowAutoRedirect = false;
            return request;
        }

        public static HttpWebRequest NotKeepAlive(this HttpWebRequest request)
        {
            request.KeepAlive = false;
            return request;
        }

        public static HttpWebRequest WithHeader(this HttpWebRequest request, HttpRequestHeader header, string value)
        {
            request.Headers[header] = value;
            return request;
        }

        public static HttpWebRequest WithoutHeader(this HttpWebRequest request, HttpRequestHeader header)
        {
            request.Headers.Remove(header);
            return request;
        }

        public static HttpWebRequest WithHeader(this HttpWebRequest request, string header, string value)
        {
            request.Headers[header] = value;
            return request;
        }

        public static HttpWebRequest WithoutHeader(this HttpWebRequest request, string header)
        {
            request.Headers.Remove(header);
            return request;
        }

        public static HttpWebRequest WithBasicCredentials(this HttpWebRequest request, string url, string username,
                                                          string password)
        {
            return request.WithHeader(HttpRequestHeader.Authorization,
                                      "Basic " +
                                      Convert.ToBase64String(Encoding.Default.GetBytes(username + ":" + password)));
        }

        public static HttpWebRequest WithoutCredentials(this HttpWebRequest request)
        {
            return request.WithoutHeader(HttpRequestHeader.Authorization);
        }

        public static HttpWebRequest WithBearerTokenAuthorization(this HttpWebRequest request, string token)
        {
            return request.WithHeader(HttpRequestHeader.Authorization, "Bearer " + token);
        }



    }
}