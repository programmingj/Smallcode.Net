using Machine.Specifications;

namespace Smallcode.Net.Test.FormParsers
{
    [Subject("������"), Tags("��", "����")]
    public class ��Դ�����к���Form��ǩʱ : ���淶
    {
        public Establish Content = () =>
            {
                Html = @"<form node-type=""form"" method=""post"" name=""vForm"" action=""/signup/signup1.php?a=1"">
                                    <input type=""hidden"" name=""act"" value=""2"">
                                    <input type=""hidden"" name=""entry"" value=""sso"">
                                    <input type=""hidden"" name=""returnURL"" value="""">
                                    <input type=""hidden"" name=""username"" value="""">
                                    <input type=""hidden"" name=""corp"" value="""">
                                    <input type=""hidden"" name=""mcheck"" value="""">
                                    <input type=""hidden"" name=""referer"" value=""980efe521296927968a67d0c98c2efa1"">
                                    <input type=""hidden"" name=""src"" value="""">                                    <div><span class=""veri_img""><img src=""/cgi/pin.php?r=1358446642033&amp;lang=zh&amp;type=newwave"" node-type=""door_img"" width=""100"" height=""40""></span></div>                                  </form>";
            };
        public Because Of = () => Form = new FormParser(Html);


        public It �����ȡ��ȷ��Action = () =>
                                  Form.GetAction(BaseUri).ShouldEqual("https://login.sina.com.cn/signup/signup1.php?a=1");

        public It �����ȡ��ȷ��Parameters = () =>
            {
                Form.FormData.Count.ShouldEqual(8);
                Form.FormData["referer"] = "980efe521296927968a67d0c98c2efa1";
            };

        public It �����ȡ��ȷ��ImgSrc = () =>
                                  Form.GetImageUrl(BaseUri, "//img[@node-type='door_img']")
                                      .ShouldEqual(
                                          "https://login.sina.com.cn/cgi/pin.php?r=1358446642033&amp;lang=zh&amp;type=newwave");
    }
}