using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;

namespace SileniumTestIntro_2
{
    class POMBase
    {
        private string driverSelector;
        private IWebDriver driver;

        public string DriverSelector { get => driverSelector; set => driverSelector = value; }

        public POMBase(string driverSelector)
        {
            this.DriverSelector = driverSelector;
        }

        public IWebDriver getDriver(string driverSelector)
        {
            switch (driverSelector)
            {
                case "chrome":
                    driver = new ChromeDriver();
                    break;
                case "firefox":
                    driver = new FirefoxDriver(); 
                    break;
                case "opera":
                    driver = new OperaDriver();
                    break;
                default:
                    break;
            }
            return driver;
        }

        public void ElementClick(IWebElement element)
        {
            element.Click();
        }

        public void SendKeyToElement(IWebElement element, string key)
        {
            element.SendKeys(key);
        }

        public IWebElement FindElementById(string id)
        {
            IWebElement request = driver.FindElement(By.Id(id));
            return request;
        }

        public IWebElement FindElementByClass(string name)
        {
            IWebElement request = driver.FindElement(By.ClassName(name));
            return request;
        }

        public IWebElement FindElementByXpath(string xpath)
        {
            IWebElement request = driver.FindElement(By.Id(xpath));
            return request;
        }

        public IWebElement FindElementByName(string name)
        {
            IWebElement request = driver.FindElement(By.Id(name));
            return request;
        }

        public void Navigate(string uri)
        {
            driver.Navigate().GoToUrl(uri);
        }

        public string GetCurrentLocation()
        {
            return driver.Url;
        }
    }
}
