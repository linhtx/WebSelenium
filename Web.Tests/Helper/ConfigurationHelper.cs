using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Tests
{
    public static class ConfigurationHelper
    {
        public static string SiteUrl
        {
            get { return ConfigurationManager.AppSettings["SiteUrl"]; }
        }

        public static string RegisterUrl
        {
            get { return string.Format("{0}{1}", SiteUrl, ConfigurationManager.AppSettings["RegisterUrl"]); }
        }

        public static string LoginUrl
        {
            get { return string.Format("{0}{1}", SiteUrl, ConfigurationManager.AppSettings["LoginUrl"]); }
        }

        public static string ChromeDrive
        {
            get { return ConfigurationManager.AppSettings["ChromeDrive"]; }
        }

        public static string TestUserName
        {
            get { return ConfigurationManager.AppSettings["TestUserName"]; }
        }

        public static string TestPassword
        {
            get { return ConfigurationManager.AppSettings["TestPassword"]; }
        }
    }
}
