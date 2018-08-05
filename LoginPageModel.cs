using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SileniumTestIntro_2
{
    class LoginPageModel:POMBase
    {
        public IWebDriver driver;

        public LoginPageModel(string driverSelector) : base (driverSelector)
        {
            driver = getDriver(driverSelector);
        }
    }
}
