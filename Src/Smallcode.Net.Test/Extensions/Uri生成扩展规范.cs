using System;
using Machine.Specifications;
using Smallcode.Net.Extensions;

namespace Smallcode.Net.Test.Extensions
{
    [Tags("�ַ�", "��չ")]
    public class Uri������չ�淶
    {
        public It ������ȷ��ȡ���Ե�ַ =
            () => "/b.php".MakeAbsoluteUri(HttpWwwBaiduComAPhp).ShouldEqual(HttpWwwBaiduComBPhp);

        public It ������ȷ��Uri��ȡ���Ե�ַ =
            () => "/b.php".MakeAbsoluteUri(new Uri(HttpWwwBaiduComAPhp))
                          .ShouldEqual(HttpWwwBaiduComBPhp);

        public static string HttpWwwBaiduComAPhp = "http://www.baidu.com/a.php";
        public static string HttpWwwBaiduComBPhp = "http://www.baidu.com/b.php";
    }
}