using System;
using Machine.Specifications;

namespace Smallcode.Net.Test.FormParsers
{
    [Subject("������"), Tags("��", "����")]
    public class ��Դ������û�к�Form��ǩʱ : ���淶
    {
        public static Exception Exception;

        public Because Of = () =>
            {
                Exception = Catch.Exception(() => new FormParser(string.Empty));
            };

        public It �����׳��쳣 = () =>
                           Exception.ShouldBeOfType<Exception>();
    }
}