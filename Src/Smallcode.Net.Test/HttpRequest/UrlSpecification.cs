using Machine.Specifications;

namespace Smallcode.Net.Test.HttpRequest
{
    [Tags("Net")]
    public class UrlSpecification : RequestSpecification
    {
        public static Url Url;

        public Establish Content = () =>
            {
                Url = new Url(UriString);
            };

        public Because Of = () => Url.Append("foo", "bar").Append("int", 1).Append("long", 5786724301).AppendTrue("true").AppendEmpty("empty").Append(string.Empty, "none");

        public It ��ȷ��ȡ��������Url��ַ = () => Url.ToString().ShouldEqual(UriString + "?foo=bar&int=1&long=5786724301&true=1&empty=&none");

        public It ��ȷƴ�Ӵ��ʺŵ�Url��ַ = () => new Url(Url.ToString()).Append("a", "b").ToString().ShouldEqual(Url + "&a=b");

        public It ��ȷƴ�Ӵ�б�ܵ�·�� = () => new Url(UriString).Combine("/a.php").ToString().ShouldEqual(UriString + "a.php");

        public It ��ȷƴ����б�ܵ�·�� = () => new Url(UriString).Combine("a.php").ToString().ShouldEqual(UriString + "a.php");

    }
}