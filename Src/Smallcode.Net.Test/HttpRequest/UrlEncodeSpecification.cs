using Machine.Specifications;

namespace Smallcode.Net.Test.HttpRequest
{
    [Tags("Net")]
    public class UrlEncodeSpecification
    {
        public It Ĭ�ϱ��� = () =>
        {
            new FormData().Set("name", "����").ToString().ShouldEqual("name=����");
        };

        public It ��ȷ����None = () =>
        {
            new FormData().Set("name", "����").Encoding(UrlEncoding.NONE).ToString().ShouldEqual("name=����");
        };

        public It ��ȷ����Utf8 = () =>
        {
            new FormData().Set("name", "����").Encoding(UrlEncoding.UTF8).ToString().ShouldEqual("name=%E5%90%8D%E5%AD%97");
        };

        public It ��ȷ����Gb2312 = () =>
            {
                new FormData().Set("name", "����").Encoding(UrlEncoding.GB2312).ToString().ShouldEqual("name=%C3%FB%D7%D6");
            };

    }
}