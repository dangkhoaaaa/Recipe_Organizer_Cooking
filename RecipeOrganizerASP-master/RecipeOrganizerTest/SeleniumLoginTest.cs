using NUnit.Framework;
using OpenQA.Selenium;

namespace RecipeOrganizerTest
{
    [TestFixture]
    public class SeleniumLoginTest : SeleniumTestBase
    {
        [Test, Category("SeleniumLoginTest")]
        public void LoginSucessfulTest_ShouldReturnWelcomeUsername()
        {
            ITakesScreenshot ts = driver as ITakesScreenshot;
            Screenshot sc;
            try
            { 
                //Open login page
                driver.Url = "https://localhost:7186/login";

                //Input username and password
                string UserName = "creep2404";
                string Password = "Cong123";

                driver.FindElement(By.Id("txtUserNameOrEmail")).SendKeys(UserName);
                driver.FindElement(By.Id("txtPassword")).SendKeys(Password);

                sc = ts.GetScreenshot();
                sc.SaveAsFile("Login.png", ScreenshotImageFormat.Png);
                Thread.Sleep(3000);

                //submit form
                driver.FindElement(By.Id("btnLogin")).Click();

                var welcomeMessage = driver.FindElement(By.Id("welcomeName"));

                //Assert
                Assert.AreEqual(UserName, welcomeMessage.Text);
                sc = ts.GetScreenshot();
                sc.SaveAsFile("login_successful.png", ScreenshotImageFormat.Png);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                sc = ts.GetScreenshot();
                sc.SaveAsFile("Login_fail.png", ScreenshotImageFormat.Png);
            }
        }

        [Test, Category("SeleniumLoginTest")]
        public void LoginFailTest_ShouldReturnInvalidMessage()
        {
            ITakesScreenshot ts = driver as ITakesScreenshot;
            Screenshot sc;
            try
            {

                //Open login page
                driver.Url = "https://localhost:7186/login";

                //Input username and password
                string UserName = "creep2404";
                string Password = "eeeeeeeeee";

                driver.FindElement(By.Id("txtUserNameOrEmail")).SendKeys(UserName);
                driver.FindElement(By.Id("txtPassword")).SendKeys(Password);

                Thread.Sleep(3000);

                //submit form
                driver.FindElement(By.Id("btnLogin")).Click();
                Thread.Sleep(3000);

                var errorMessage = driver.FindElement(By.Id("invalidMessage"));

                //Assert
                Assert.AreEqual("Invalid login attempt.", errorMessage.Text);
                sc = ts.GetScreenshot();
                sc.SaveAsFile("Login_fail.png", ScreenshotImageFormat.Png);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
