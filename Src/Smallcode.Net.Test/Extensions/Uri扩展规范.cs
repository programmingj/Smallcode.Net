using Machine.Specifications;
using Smallcode.Net.Extensions;

namespace Smallcode.Net.Test.Extensions
{
    [Tags("Uri", "��չ"), Ignore("��Ҫ�������")]
    public class Uri��չ�淶
    {
        public It ����������д����� = () => "http://www.baidu.com/".OpenInBrower();
    }
}