using System;
using Machine.Specifications;
using Smallcode.Net.Extensions;

namespace Smallcode.Net.Test.Extensions
{
    [Tags("�ַ�", "����")]
    public class Random��չ�淶
    {
        private enum TestEnum
        {
            Boy,
            Girl
        }
        public static Random Random;
        public Establish Content = () => Random = new Random();

        public It ������ȷ����ָ�����ȵ��漴С���ַ��� =
            () =>
            Random.NextDoubleString(10)
                         .ShouldMatch(
                             s =>
                             s.Substring(s.IndexOf(".", StringComparison.Ordinal) + 1).Length == 10 &&
                             s.StartsWith("0."));

        public It ������ȷ����ָ�����ȵ���������ַ��� = () => Random.NextIntString(10).ShouldMatch(s => s.Length == 10 && s.IsNumberic());
        public It ������ȷ����ָ�����ȵ������ĸ�� = () => Random.NextLetters(10).ShouldMatch(s => s.Length == 10);
        public It ������ȷ����ָ�����ȵ�����ַ��� = () => Random.NextString(10).ShouldMatch(s => s.Length == 10);

        public It �ܻ�ȡ������������� = () => Random.NextChineseBoyFullname().Length.ShouldEqual(3);
        public It �ܻ�ȡ����Ĳ���ֵ = () => Random.NextBool().ShouldBeOfType<bool>();
        public It �ܻ�ȡ�����ö��ֵ = () => Random.NextEnum<TestEnum>().ShouldBeOfType<TestEnum>();

        public It �ܻ�ȡ��������� = () => Random.NextDateTime().ShouldBeOfType<DateTime>();

        public It �ܻ�ȡ�����Χ�ڵ����� = () =>
            {
                var min = DateTime.Now.AddDays(-1);
                var max = DateTime.Now;
                Random.NextDateTime(min, max).ShouldMatch(date => date > min && date < max);
            };

        public It �ܻ�ȡ������� = () =>
            {
                var min = 20;
                var max = 30;
                var nextBirthday = Random.NextBirthday(min, max);
                nextBirthday.ShouldMatch(date => date > DateTime.Now.AddYears(-max) && date < DateTime.Now.AddYears(-min));
            };
    }
}