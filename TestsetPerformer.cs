using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;


namespace SileniumTestIntro_2
{
    [TestClass]
    public class TestsetPerformer
    {
        TestSuiteGetter getter;
        ResultWriter writer;

        [TestInitialize]
        public void Setup()
        {
            getter = new TestSuiteGetter();
            getter.DefineTestSet("xml");
            writer = new ResultWriter(@"D:\testResults.txt");
        }

        [TestMethod]
        public void StackOverFlowLoginPage_TeamRefCheck()
        {
            //string uri = getter.TestUri;
            string uri = @"https://stackoverflow.com/users/login?ssrc=head&returnurl=https%3a%2f%2fstackoverflow.com%2f";
            IWebElement element;

            LoginPageModel loginPage = new LoginPageModel("chrome");

            loginPage.Navigate(uri);
            element = loginPage.FindElementByClass("js-gps-track s-btn s-btn__outlined ta-center grid--cell mt12");
            element.Click();

            if (loginPage.GetCurrentLocation() == @"https://stackoverflow.com/teams")
            {
                writer.WriteStatus("TEST #1", "DEMO", "PASSED");
            }
        }


        [TestMethod]
        public void StackOverFlowLoginPage_LoginCheck()
        {
            string name = "";
            string password = "";
            //string uri = getter.TestUri;
            string uri = @"https://stackoverflow.com/users/login?ssrc=head&returnurl=https%3a%2f%2fstackoverflow.com%2f";
            IWebElement element;

            LoginPageModel loginPage = new LoginPageModel("chrome");

            foreach (var item in getter.Testset)
            {
                if (item.Name == "IncorrectLogin")
                {
                    name = (string)item.Data[0];
                    password = (string)item.Data[1];
                }
            }

            loginPage.Navigate(uri);
            element = loginPage.FindElementById("email");
            loginPage.SendKeyToElement(element, name);

            element = loginPage.FindElementById("password");
            loginPage.SendKeyToElement(element, name);

            element = loginPage.FindElementById("submit-button");
            element.Click();

            element = loginPage.FindElementByXpath("/html/body/div[4]/div[3]/div/div[2]");
            string result = element.Text;

            if (result == "The email or password is incorrect.")
            {
                writer.WriteStatus("TEST #1", "DEMO", "PASSED");
            }
            else {
                writer.WriteStatus("TEST #1", "DEMO", "NOT PASSED");
            }
        }
    }
}
