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

namespace Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        ChromeDriver chromeDrive;

        [TestInitialize]
        public void Setup()
        {
            chromeDrive = new ChromeDriver(ConfigurationHelper.ChromeDrive);

            bool exists = System.IO.Directory.Exists(ConfigurationHelper.FolderPicture);
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(ConfigurationHelper.FolderPicture);
            }   
        }

        [TestMethod]
        public void RegisterSuccess()
        {   
            chromeDrive.Navigate().GoToUrl(ConfigurationHelper.RegisterUrl);
            var txtUserName = chromeDrive.FindElementById("UserName");
            var txtPassword = chromeDrive.FindElementById("Password");
            var txtConfirmPassword = chromeDrive.FindElementById("ConfirmPassword");
            var btnRegister = chromeDrive.FindElementById("Register");

            txtUserName.SendKeys(ConfigurationHelper.TestUserName);
            txtPassword.SendKeys(ConfigurationHelper.TestPassword);
            txtConfirmPassword.SendKeys(ConfigurationHelper.TestPassword);

            btnRegister.Submit();

            Screenshot screenshot = ((ITakesScreenshot)chromeDrive).GetScreenshot();
            bool result = true;
            try
            {
                var errMessage = chromeDrive.FindElementByClassName("validation-summary-errors");
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
            chromeDrive.Navigate().GoToUrl(ConfigurationHelper.LoginUrl);
            var txtUserName = chromeDrive.FindElementById("UserName");
            var txtPassword = chromeDrive.FindElementById("Password");
            var btnLogin = chromeDrive.FindElementById("Login");

            txtUserName.SendKeys(ConfigurationHelper.TestUserName);
            txtPassword.SendKeys(ConfigurationHelper.TestPassword);

            btnLogin.Submit();

            Screenshot screenshot = ((ITakesScreenshot)chromeDrive).GetScreenshot();
            bool result = true;
            try
            {
                var errMessage = chromeDrive.FindElementByClassName("validation-summary-errors");
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
            chromeDrive.Navigate().GoToUrl(ConfigurationHelper.LoginUrl);
            var txtUserName = chromeDrive.FindElementById("UserName");
            var txtPassword = chromeDrive.FindElementById("Password");
            var btnLogin = chromeDrive.FindElementById("Login");

            txtUserName.SendKeys(Guid.NewGuid().ToString());
            txtPassword.SendKeys(Guid.NewGuid().ToString());

            btnLogin.Submit();

            Screenshot screenshot = ((ITakesScreenshot)chromeDrive).GetScreenshot();
            bool result = false;
            try
            {
                var errMessage = chromeDrive.FindElementByClassName("validation-summary-errors");
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
            chromeDrive.Navigate().GoToUrl(ConfigurationHelper.RegisterUrl);
            var txtUserName = chromeDrive.FindElementById("UserName");
            var txtPassword = chromeDrive.FindElementById("Password");
            var txtConfirmPassword = chromeDrive.FindElementById("ConfirmPassword");
            var btnRegister = chromeDrive.FindElementById("Register");

            txtUserName.SendKeys(ConfigurationHelper.TestUserName);
            txtPassword.SendKeys(ConfigurationHelper.TestPassword);
            txtConfirmPassword.SendKeys(ConfigurationHelper.TestPassword);

            btnRegister.Submit();

            Screenshot screenshot = ((ITakesScreenshot)chromeDrive).GetScreenshot();
            bool result = false;
            try
            {
                var errMessage = chromeDrive.FindElementByClassName("validation-summary-errors");
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
            chromeDrive.Navigate().GoToUrl(ConfigurationHelper.LoginUrl);
            var txtUserName = chromeDrive.FindElementById("UserName");
            var txtPassword = chromeDrive.FindElementById("Password");
            var btnLogin = chromeDrive.FindElementById("Login");

            txtUserName.SendKeys(Guid.NewGuid().ToString());
            txtPassword.SendKeys(Guid.NewGuid().ToString());

            btnLogin.Submit();

            bool result = false;
            try
            {
                var errMessage = chromeDrive.FindElementByClassName("validation-summary-errors");
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
