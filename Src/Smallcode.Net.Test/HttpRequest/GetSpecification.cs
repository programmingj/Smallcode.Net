using System.Net;
using Machine.Specifications;
using Smallcode.Net.Extensions;

namespace Smallcode.Net.Test.HttpRequest
{
    [Tags("Net")]
    public class GetSpecification : RequestSpecification
    {
        public Establish Content = () =>
        {
            Request = new Url(UriString).CreateRequest();
            Request.Get();
        };
        public It MethodΪGet = () =>
            {
                Request.Method.ShouldEqual(HttpWebRequestMethod.GET.ToString());
            };

        public It �ܻ�ȡgzipѹ����ʽ����ҳ =
            () => new Url("http://www.sina.com").CreateRequest().GetHttpResponse().ContentEncoding.ShouldEqual("gzip");

        public It �ܷ������� = () => new Url("http://www.sina.com").CreateRequest().Get().ShouldContain("����");
        public It �ܷ����Ա� = () =>
            {
                const string url = "http://www.taobao.com/";
                var cookie = new CookieContainer();
                var request = new Url(url).CreateRequest().WithCookies(cookie);
                var result = request.Get();//��һ�λ�ȡ���ܻ᷵����תҳ�����ٴλ�ȡ
                if (!result.Contains("�Ա�"))
                {
                    result = new Url(url).CreateRequest().WithCookies(cookie).Get();
                }
                result.ShouldContain("�Ա�");
            };
        public It �ܷ������� = () => new Url("http://www.163.com").CreateRequest().Get().ShouldContain("����");
        public It �ܷ����Ѻ� = () => new Url("http://www.sohu.com").CreateRequest().Get().ShouldContain("�Ѻ�");
        public It �ܷ��ʰٶ� = () => new Url("http://www.baidu.com").CreateRequest().Get().ShouldContain("�ٶ�");
        public It �ܷ���QQ�ռ� = () => new Url("http://i.qq.com").CreateRequest().Get().ShouldContain("QQ�ռ�");

        public It �ܻ�ȡ�ֽ����� =
            () => new Url("http://login.sina.com.cn/cgi/pin.php").CreateRequest()
                                .GetBytes()
                                .Length.ShouldBeGreaterThan(0);

        public It �ܻ�ȡ��ת��ַ = () => new Url("http://www.google.com").CreateRequest().NotRedirect().Get().ShouldStartWith("http://www.google.com.hk/url");

    }
}