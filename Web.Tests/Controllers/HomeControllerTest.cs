using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web;
using Web.Controllers;
using OpenQA.Selenium.Chrome;

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

            chromeDrive.Navigate().GoToUrl(ConfigurationHelper.LoginUrl);
            bool result = false;
            try
            {
                var logoutForm = chromeDrive.FindElementById("logoutForm");
                result = logoutForm.Displayed;
            }
            catch
            {
                result = false;
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

            chromeDrive.Navigate().GoToUrl(ConfigurationHelper.LoginUrl);
            bool result = false;
            try
            {
                var logoutForm = chromeDrive.FindElementById("logoutForm");
                result = logoutForm.Displayed;
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

            chromeDrive.Navigate().GoToUrl(ConfigurationHelper.LoginUrl);
            bool result = false;
            try
            {
                var logoutForm = chromeDrive.FindElementById("logoutForm");
                result = logoutForm.Displayed;
            }
            catch
            {
                result = false;
            }

            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void LoginFailure()
        {
            chromeDrive.Navigate().GoToUrl(ConfigurationHelper.LoginUrl);
            var txtUserName = chromeDrive.FindElementById("UserName");
            var txtPassword = chromeDrive.FindElementById("Password");
            var btnLogin = chromeDrive.FindElementById("Login");

            txtUserName.SendKeys("linh.tran");
            txtPassword.SendKeys("123456");

            btnLogin.Submit();

            chromeDrive.Navigate().GoToUrl(ConfigurationHelper.LoginUrl);
            bool result = false;
            try
            {
                var logoutForm = chromeDrive.FindElementById("logoutForm");
                result = logoutForm.Displayed;
            }
            catch
            {
                result = false;
            }

            Assert.AreEqual(result, false);
        }
    }
}
