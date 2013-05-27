using Machine.Specifications;
using Smallcode.Net.Extensions;

namespace Smallcode.Net.Test.HttpRequest
{
    [Tags("Net")]
    public class PostSpecification : RequestSpecification
    {
        public static FormData Data;
        public static string PostUri;
        public static string Result;

        public Establish PostContent = () =>
            {
                PostUri = "http://passport.baidu.com/v2/api/?login";
                Data = new FormData().Set("username", "aa").Set("password", "bb");
            };

        public Because Of = () =>
            {
                Request = new Url(PostUri).CreateRequest();
                Result = Request.Post(Data);
            };

        public It MethodΪPost = () => Request.Method.ToUpper().ShouldEqual("POST");

        public It ��ȷ����ContentType =
            () => Request.ContentType.ShouldEqual("application/x-www-form-urlencoded;charset=utf-8");

        public It �ܻ�÷����ַ��� = () => Result.ShouldContain("baidu");
    }
}