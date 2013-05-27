using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Machine.Specifications;
using Smallcode.Net.Exceptions;

namespace Smallcode.Net.Test.Extensions
{
    [Tags("�ַ�", "��չ")]
    public class Html������չ�淶
    {
        public static Html EmptyHtml;
        public static Html Html;
        public Establish Content = () =>
            {
                EmptyHtml = Html.Parse(string.Empty);
                Html = Html.Parse("<div class=\"class\" id=\"id\" name=\"name\"><a href=\"http://www.test.com\">click me</a></div><div class=\"class other\" id=\"other\"><a href=\"http://www.test1.com\">click me</a></div>");
            };

        #region ���ڵ�
        public It �޷�����ʱ���׳��쳣 = () => Catch.Exception(() => EmptyHtml.FindElement(By.CssSelector("div"))).ShouldBeOfType<NotFoundException>();

        public It ��������TagName���� = () => Html.FindElement(By.TagName("div")).ShouldMatch(ConditionId());

        public It ��������id���� = () => Html.FindElement(By.Id("id")).ShouldMatch(ConditionId());

        public It ��������ClassName���� = () => Html.FindElement(By.ClassName("class")).ShouldMatch(ConditionId());

        public It ��������xPath���� = () => Html.FindElement(By.XPath("//div")).ShouldMatch(ConditionId());

        public It ��������LinkText���� = () => Html.FindElement(By.LinkText("click me")).ShouldMatch(ConditionHref());

        public It ��������PartialLinkText���� = () => Html.FindElement(By.PartialLinkText("click")).ShouldMatch(ConditionHref());

        public It ��������Css���� = () =>
            {
                Html.FindElement(By.CssSelector(".class")).ShouldMatch(ConditionId());
                Html.FindElement(By.CssSelector("#id")).ShouldMatch(ConditionId());
                Html.FindElement(By.CssSelector("div")).ShouldMatch(ConditionId());
                Html.FindElement(By.CssSelector("div.class")).ShouldMatch(ConditionId());
                Html.FindElement(By.CssSelector("div#id")).ShouldMatch(ConditionId());
                Html.FindElement(By.CssSelector("div.class.other")).ShouldMatch(h => h.GetAttribute("id") == "other");//����ͬʱƥ������class
                Html.FindElement(By.CssSelector("div#other.other")).ShouldMatch(h => h.GetAttribute("id") == "other");//����ͬʱƥ��id��class
                Html.FindElement(By.CssSelector("div.class > a")).ShouldMatch(ConditionHref());//ѡ��div.class��ֱ���ӱ�ǩa
                Html.FindElement(By.CssSelector("div[name=name].class")).ShouldMatch(ConditionId());//����ͬʱƥ������name����class
            };
        #endregion

        #region ��ڵ�
        public It �޷���������ʱ���ؿռ��� = () => EmptyHtml.FindElements(By.CssSelector("div")).Count.ShouldEqual(0);

        public It ��������TagName�������� = () => Html.FindElements(By.TagName("div")).ShouldMatch(ConditionIds());

        public It ��������ClassName�������� = () => Html.FindElements(By.ClassName("class")).ShouldMatch(ConditionIds());

        public It ��������xPath�������� = () => Html.FindElements(By.XPath("//div")).ShouldMatch(ConditionIds());

        public It ��������LinkText�������� = () => Html.FindElements(By.LinkText("click me")).ShouldMatch(ConditionHrefs());

        public It ��������PartialLinkText�������� = () => Html.FindElements(By.PartialLinkText("click")).ShouldMatch(ConditionHrefs());

        public It ��������Css�������� = () =>
        {
            Html.FindElements(By.CssSelector("div")).ShouldMatch(ConditionIds());
            Html.FindElements(By.CssSelector("div.class")).ShouldMatch(ConditionIds());
        };
        #endregion

        private static Expression<Func<Html, bool>> ConditionId()
        {
            return h => h["class"] == "class" && h["id"] == "id" && h["name"] == "name"; ;
        }

        private static Expression<Func<ReadOnlyCollection<Html>, bool>> ConditionIds()
        {
            return list => list.Count == 2 && list[0].GetAttribute("id") == "id" && list[1].GetAttribute("id") == "other";
        }

        private static Expression<Func<Html, bool>> ConditionHref()
        {
            return h => h.GetAttribute("href") == "http://www.test.com";
        }

        private static Expression<Func<ReadOnlyCollection<Html>, bool>> ConditionHrefs()
        {
            return list => list.Count == 2 && list[0].GetAttribute("href") == "http://www.test.com" && list[1].GetAttribute("href") == "http://www.test1.com";
        }

    }
}