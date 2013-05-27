using Machine.Specifications;

namespace Smallcode.Net.Test.HttpRequest
{
    [Tags("Net")]
    public class FormDataSpecification
    {
        public static FormData Data;

        public Establish Content = () =>
            {
                Data = new FormData();
            };

        public Because Of =
            () => Data.Set("foo", "ball")
                      .Set("int", 1)
                      .Set("long", 5786724301)
                      .SetZero("zero")
                      .SetTrue("true")
                      .SetFalse("false")
                      .SetEmpty("empty")
                      .Set(string.Empty, "none");

        public It �������ַ��� =
            () => { Data.ToString().ShouldEqual("foo=ball&int=1&long=5786724301&zero=0&true=1&false=0&empty=&none"); };

        public It ���ȱ�����ȷ = () => { Data.Count.ShouldEqual(8); };

        public It �����ظ�����key = () => { new FormData().Set("key", "value").Set("key", "value").Count.ShouldEqual(1); };

        public It ��������������� = () => { new FormData().SetRandomInteger("key", 8)["key"].Length.ShouldEqual(8); };

        public It �����������С�� = () =>
            {
                var data = new FormData().SetRandomFloat("key", 8)["key"];
                data.ShouldStartWith("0.");
                data.Length.ShouldEqual(10);
            };

        public It �����������Unixʱ��� = () => { new FormData().SetUnixTimestamp("key", 8)["key"].Length.ShouldEqual(8); };
    }
}