using Machine.Specifications;
using Smallcode.Net.Extensions;

namespace Smallcode.Net.Test.Extensions
{
    [Tags("�ַ�", "��չ")]
    public class ������չ�淶
    {
        public static string Words;

        public Establish Content
            = () =>
                {
                    Words = @"123abcABC,.!";
                };
        public It ��ȷ��ȡ��gbk������ַ��� = () =>
            {
                @"1��a".EscapeDataStringBy().ShouldEqual("%31%CE%D2%61");
                Words.EscapeDataStringBy().ShouldEqual(Words);
            };

        public It ��ȷ��ȡ��Utf8������ַ��� =
            () =>
            {
                Words.EscapeDataStringBy(UrlEncoding.UTF8).ShouldEqual(Words);
                @"�����ٷ�ɱ��".EscapeDataStringBy(UrlEncoding.UTF8)
                          .ShouldEqual("%E7%8E%89%E6%A0%91%E4%B8%B4%E9%A3%8E%E6%9D%80%E7%8C%AA%E5%88%80");
            };
    }
}