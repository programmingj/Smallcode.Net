using System;
using Machine.Specifications;
using Smallcode.Net.Extensions;

namespace Smallcode.Net.Test.Extensions
{
    [Tags("����", "��չ")]
    public class ������չ�淶
    {
        public It ��ת��Ϊ10λUnixʱ��� = () => DateTime.Now.ToUnixTimeString().ShouldMatch(s => s.Length == 10);
        public It ��ת��Ϊ����10λUnixʱ��� = () => DateTime.Now.ToUnixTimeString(13).ShouldMatch(s => s.Length == 13);
        public It ��ת��ΪС��10λUnixʱ��� = () => DateTime.Now.ToUnixTimeString(8).ShouldMatch(s => s.Length == 8);
    }
}