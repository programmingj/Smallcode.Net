using System.Net;
using Machine.Specifications;
using Smallcode.Net.Extensions;

namespace Smallcode.Net.Test.HttpRequest
{
    [Tags("Net")]
    public class DefaultRequestSpecification : RequestSpecification
    {
        public Because Of = () => { Request = new Url(UriString).CreateRequest(); };

        public It ����ȷ����Method = () => Request.Method.ToUpper().ShouldEqual("GET");
        public It ����ȷ����Uri = () => Request.RequestUri.ToString().ShouldEqual(UriString);
        public It ����ȷ����UserAgent = () => Request.UserAgent.ShouldEqual(HttpWebRequestUserAgent.IE);

        public It ����ȷ����AcceptEncoding =
            () => Request.Headers[HttpRequestHeader.AcceptEncoding].ShouldEqual("gzip,deflate");

        public It ����ȷ����Referer = () => Request.Referer.ShouldBeNull();
        public It CookiesΪnull = () => Request.CookieContainer.ShouldBeNull();
        public It ����Cookieʱ���׳��쳣 = () => Catch.Exception(() => Request.SetCookie("foo", "bar")).ShouldBeOfType<CookieException>();
        public It ��ȡCookieʱ���׳��쳣 = () => Catch.Exception(() => Request.SetCookie("foo", "bar")).ShouldBeOfType<CookieException>();
        public It ����ȷ����AllowAutoRedirect = () => Request.AllowAutoRedirect.ShouldBeTrue();
    }
}