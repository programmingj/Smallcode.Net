using Machine.Specifications;
using Smallcode.Net.Extensions;

namespace Smallcode.Net.Test.Extensions
{
    [Tags("�ַ�", "��չ")]
    public class ������չ�淶
    {
        public It ������ȷ����md5 = () => { "ABCD1234".CalculateMd5Hash().ShouldEqual("361633153A464830A1FE85DEC5EFAB17"); };
    }
}