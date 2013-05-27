using Machine.Specifications;
using Smallcode.Net.Extensions;

namespace Smallcode.Net.Test.Extensions
{
    [Tags("�ַ�", "��չ")]
    public class ������չ�淶
    {
        public It ������ȷ��ȡ�ַ��� = () =>
            {
                "you are the best one".CutString("are", "best").ShouldEqual("the");
                "you the best one".CutString("are", "best").ShouldBeEmpty();
                "you are the one".CutString("are", "best").ShouldBeEmpty();
                "you the one".CutString("are", "best").ShouldBeEmpty();

                "you are the best one".CutString("you", "one", true, true).ShouldEqual("you are the best one");
                "you are the best one".CutString("you", "one", false, true).ShouldEqual("are the best one");
                "you are the best one".CutString("you", "one").ShouldEqual("are the best");
            };

        public It ������ȷ�ж��Ƿ�Ϊ���� = () =>
            {
                "187".IsNumberic().ShouldBeTrue();
                "&#187;".IsNumberic().ShouldBeFalse();
            };

        //public It ������ȷ�����ַ������� = () =>
        //    {
        //        "��".HalfOfByteCount().ShouldEqual(1);
        //        "����".HalfOfByteCount().ShouldEqual(2);
        //        "aa".HalfOfByteCount().ShouldEqual(1);
        //        "aaa".HalfOfByteCount().ShouldEqual(2);
        //        "��".HalfOfByteCount().ShouldEqual(1);
        //        "..".HalfOfByteCount().ShouldEqual(1);
        //    };
    }
}