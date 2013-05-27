using System.Text;
using Machine.Specifications;
using Smallcode.Net.Extensions;

namespace Smallcode.Net.Test.Extensions
{
    [Tags("�ַ�", "��չ")]
    public class UrlEncoding��չ�淶
    {
        public It �ܻ�ȡGb2312���� =
            () => UrlEncoding.GB2312.ToEncoding().ShouldEqual(Encoding.GetEncoding("gb2312"));
        public It �ܻ�ȡUtf8���� =
            () => UrlEncoding.UTF8.ToEncoding().ShouldEqual(Encoding.UTF8);
        public It �ܻ�ȡĬ�ϱ��� =
            () => UrlEncoding.NONE.ToEncoding().ShouldEqual(Encoding.Default);

        public It Gb2312�����gbk��������ͬ�� =
            () => (Encoding.GetEncoding("gb2312").Equals(Encoding.GetEncoding("gbk"))).ShouldBeTrue();

    }
}