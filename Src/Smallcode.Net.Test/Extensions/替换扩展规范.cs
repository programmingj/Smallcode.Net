using Machine.Specifications;
using Smallcode.Net.Extensions;

namespace Smallcode.Net.Test.Extensions
{
    [Tags("�ַ�", "��չ")]
    public class �滻��չ�淶
    {
        public It ������ȷ������滻���ַ� = () =>
            {
                @"   hello   
            world  ".ReplaceBlankSpaces().ShouldEqual("hello world");

                @"   hello   
            world  ".ReplaceBlankSpaces("").ShouldEqual("helloworld");
            };

        public It ������ȷ������滻���� = () =>
            {
                @"\r\n hello \r\n world \r\n".RepalceLine().ShouldEqual(" hello  world ");
                @"hello\r\nworld".RepalceLine("").ShouldEqual("helloworld");
            };

        public It ������ȷ������滻������ַ��ʽ = () =>
            {
                @"hello http://www.baidu.com world".RepalceUrl().ShouldEqual("hello world");
                @"hello http://www.baidu.com world".RepalceUrl("the").ShouldEqual("hello the world");
            };

        public It ������ȷ������滻a��ǩ�е�href���� =
            () => { @"<a href=""http://www.baidu.com"">baidu</a>".ReplaceHref().ShouldEqual("<a >baidu</a>"); };

        public It ������ȷ������滻����html��ǩ =
            () => { @"<a href=""http://www.baidu.com"">baidu</a>".ClearHtmlTags().ShouldEqual("baidu"); };

        public It ������ȷ���ת���ַ� = () =>
            {
                var str = @"<a class=\""hi\""><\/a>";
                str.ClearEscape().ShouldEqual(@"<a class=""hi""></a>");
            };

        public It ������ȷ�滻ƥ���� =
            () => { "......aa11����.....".RegexReplace(@"(\W)\1+", "$1").ShouldEqual(".aa11����."); //�滻��������ͬ�ķ��ַ���������֮��ģ�
            };
    }
}