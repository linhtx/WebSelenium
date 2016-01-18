using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web;
using Web.Controllers;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Drawing.Imaging;
using System.IO;
using OpenQA.Selenium.Firefox;

namespace Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private IWebDriver webDriver;

        [TestInitialize]
        public void Setup()
        {
            if (ConfigurationHelper.BrowserType == (int)BrowserType.Firefox)
            {
                webDriver = new FirefoxDriver();
            }
            else
            {
                webDriver = new ChromeDriver(ConfigurationHelper.ChromeDrive);
            }
            bool exists = System.IO.Directory.Exists(ConfigurationHelper.FolderPicture);

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(ConfigurationHelper.FolderPicture);
            }
        }

        [TestMethod]
        public void RegisterSuccess()
        {
            webDriver.Navigate().GoToUrl(ConfigurationHelper.RegisterUrl);
            webDriver.FindElement(By.Id("UserName")).SendKeys(ConfigurationHelper.TestUserName);
            webDriver.FindElement(By.Id("Password")).SendKeys(ConfigurationHelper.TestPassword);
            webDriver.FindElement(By.Id("ConfirmPassword")).SendKeys(ConfigurationHelper.TestPassword);
            webDriver.FindElement(By.Id("Register")).Submit();
            Screenshot screenshot = ((ITakesScreenshot)webDriver).GetScreenshot();
            bool result = true;

            try
            {
                var errMessage = webDriver.FindElement(By.ClassName("validation-summary-errors"));
                result = !errMessage.Displayed;
                SaveScreenshot(screenshot, string.Format("{0}_register_fail.png", ConfigurationHelper.TestUserName));
            }
            catch
            {
                result = true;
            }

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void LoginSuccess()
        {
            webDriver.Navigate().GoToUrl(ConfigurationHelper.LoginUrl);
            webDriver.FindElement(By.Id("UserName")).SendKeys(ConfigurationHelper.TestUserName);
            webDriver.FindElement(By.Id("Password")).SendKeys(ConfigurationHelper.TestPassword);
            webDriver.FindElement(By.Id("Login")).Submit();
            Screenshot screenshot = ((ITakesScreenshot)webDriver).GetScreenshot();
            bool result = true;

            try
            {
                var errMessage = webDriver.FindElement(By.ClassName("validation-summary-errors"));
                result = !errMessage.Displayed;
                SaveScreenshot(screenshot, string.Format("{0}_login_fail.png", ConfigurationHelper.TestUserName));
            }
            catch
            {
                result = true;
            }

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void LoginFailureInvalid()
        {
            webDriver.Navigate().GoToUrl(ConfigurationHelper.LoginUrl);
            webDriver.FindElement(By.Id("UserName")).SendKeys(Guid.NewGuid().ToString());
            webDriver.FindElement(By.Id("Password")).SendKeys(Guid.NewGuid().ToString());
            webDriver.FindElement(By.Id("Login")).Submit();
            Screenshot screenshot = ((ITakesScreenshot)webDriver).GetScreenshot();
            bool result = false;

            try
            {
                var errMessage = webDriver.FindElement(By.ClassName("validation-summary-errors"));
                result = errMessage.Text.Equals("Invalid username or password.");

                SaveScreenshot(screenshot, string.Format("{0}_login_fail.png", ConfigurationHelper.TestUserName));
            }
            catch
            {
                result = false;
            }

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void RegisterFailure()
        {
            webDriver.Navigate().GoToUrl(ConfigurationHelper.RegisterUrl);
            webDriver.FindElement(By.Id("UserName")).SendKeys(ConfigurationHelper.TestUserName);
            webDriver.FindElement(By.Id("Password")).SendKeys(ConfigurationHelper.TestPassword);
            webDriver.FindElement(By.Id("ConfirmPassword")).SendKeys(ConfigurationHelper.TestPassword);
            webDriver.FindElement(By.Id("Register")).Submit();
            Screenshot screenshot = ((ITakesScreenshot)webDriver).GetScreenshot();
            bool result = false;

            try
            {
                var errMessage = webDriver.FindElement(By.ClassName("validation-summary-errors"));
                result = errMessage.Displayed;
                SaveScreenshot(screenshot, string.Format("{0}_register_fail.png", ConfigurationHelper.TestUserName));
            }
            catch
            {
                result = false;
            }

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void LoginFailure()
        {
            webDriver.Navigate().GoToUrl(ConfigurationHelper.LoginUrl);
            webDriver.FindElement(By.Id("UserName")).SendKeys(Guid.NewGuid().ToString());
            webDriver.FindElement(By.Id("Password")).SendKeys(Guid.NewGuid().ToString());
            webDriver.FindElement(By.Id("Login")).Submit();
            bool result = false;

            try
            {
                var errMessage = webDriver.FindElement(By.ClassName("validation-summary-errors"));
                result = errMessage.Displayed;
            }
            catch
            {
                result = false;
            }

            Assert.AreEqual(result, true);
        }

        private void SaveScreenshot(Screenshot screenshot, string fileName)
        {
            screenshot.SaveAsFile(string.Format("{0}{1}", ConfigurationHelper.FolderPicture, fileName), ImageFormat.Png);
        }
    }
}
