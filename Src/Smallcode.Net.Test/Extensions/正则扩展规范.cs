using System;
using System.Text.RegularExpressions;
using Machine.Specifications;
using Rhino.Mocks;
using Smallcode.Net.Extensions;

namespace Smallcode.Net.Test.Extensions
{
    [Tags("�ַ�", "��չ")]
    public class ������չ�淶
    {
        public It ������ȷ�ж��Ƿ�ƥ�� = () =>
            {
                var successAction = MockRepository.GenerateMock<Action<GroupCollection>>();
                var failAction = MockRepository.GenerateMock<Action>();
                "name=hello".Match(@"name=([A-Za-z0-9-]+)", successAction, failAction).Success.ShouldBeTrue();
                successAction.AssertWasCalled(a => a.Invoke(Arg<GroupCollection>.Is.Anything));
                failAction.AssertWasNotCalled(a => a.Invoke());

                successAction = MockRepository.GenerateMock<Action<GroupCollection>>();
                failAction = MockRepository.GenerateMock<Action>();
                "hello world".Match(@"name=([A-Za-z0-9-]+)", successAction, failAction).Success.ShouldBeFalse();
                successAction.AssertWasNotCalled(a => a.Invoke(Arg<GroupCollection>.Is.Anything));
                failAction.AssertWasCalled(a => a.Invoke());
            };

        public It ������ȷƥ����� = () =>
            {
                Func<GroupCollection, string> func = match => match["value"].Value;
                "name=hello,name=world".Matches(@"name=(?<value>[A-Za-z0-9-]+)", func).ShouldMatch(
                    l => l.Count == 2 && l[0] == "hello" && l[1] == "world");
            };
    }
}