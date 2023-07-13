using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeOrganizerTest
{
    public class SeleniumTestBase
    {
        protected IWebDriver driver;

        [SetUp]
        public void Open()
        {
            driver = new ChromeDriver(); //Launch Chrome Brower
            driver.Manage().Window.Maximize(); //Maximizes Brower
            driver.Url = "https://localhost:7186/"; //
        }

        [TearDown]
        public void Close()
        {
            driver.Quit();
        }
    }
}
