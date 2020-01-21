using System;
using System.IO;
using System.Net;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections.Specialized;
using OpenQA.Selenium;

namespace NUnit.Web.Tests

{
    [SetUpFixture]
    public class CBTAPI
    {
        protected RemoteWebDriver driver;
        protected string browser;
        protected string session_id;
        public string BaseURL = "https://crossbrowsertesting.com/api/v3/selenium";
        public string username = "jonathan@outeniquastudios.com";
        public string authkey = "u4570c2a8a24425a";

        public CBTAPI()
        {

        }
        public CBTAPI(string browser)
        {
            this.browser = browser;
        }

        [OneTimeSetUp]
        public void Initialize()
        {
            var caps = new RemoteSessionSettings();

            caps.AddMetadataSetting("name", "NUnit Test");
            caps.AddMetadataSetting("username", username);
            caps.AddMetadataSetting("password", authkey);
            caps.AddMetadataSetting("platform", "Windows 10");

            switch (browser)
            {
                // These all pull the latest version by default
                // To specify version add SetCapability("version", "desired version")
                case "chrome":
                    caps.AddMetadataSetting("browserName", "Chrome");
                    break;
                case "ie":
                    caps.AddMetadataSetting("browserName", "Internet Explorer");
                    break;
                case "edge":
                    caps.AddMetadataSetting("browserName", "MicrosoftEdge");
                    break;
                case "firefox":
                    caps.AddMetadataSetting("browserName", "Firefox");
                    break;
                default:
                    caps.AddMetadataSetting("browserName", "Chrome");
                    break;
            }


            driver = new RemoteWebDriver(new Uri("http://hub.crossbrowsertesting.com:80/wd/hub/"), caps);
        }

        [Test]
        public void TestTodos()
        {
            driver.Navigate().GoToUrl("http://crossbrowsertesting.github.io/todo-app.html");
            // Check the title
            driver.FindElement(By.Name("todo-4")).Click();
            driver.FindElement(By.Name("todo-5")).Click();

            // If both clicks worked, then the following List should have length 2
            IList<IWebElement> elems = driver.FindElements(By.ClassName("done-true"));
            // so we'll assert that this is correct.
            Assert.AreEqual(2, elems.Count);

            driver.FindElement(By.Id("todotext")).SendKeys("run your first selenium test");
            driver.FindElement(By.Id("addbutton")).Click();

            // lets also assert that the new todo we added is in the list
            string spanText = driver.FindElement(By.XPath("/html/body/div/div/div/ul/li[6]/span")).Text;
            Assert.AreEqual("run your first selenium test", spanText);
            driver.FindElement(By.LinkText("archive")).Click();

            elems = driver.FindElements(By.ClassName("done-false"));
            Assert.AreEqual(4, elems.Count);
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            var session_id = driver.SessionId.ToString();
            driver.Quit();
            setScore(session_id, "pass");
        }

        public void setScore(string sessionId, string score)
        {
            string url = BaseURL + "/" + sessionId;
            // encode the data to be written
            ASCIIEncoding encoding = new ASCIIEncoding();
            string data = "action=set_score&score=" + score;
            byte[] putdata = encoding.GetBytes(data);
            // Create the request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "PUT";
            request.Credentials = new NetworkCredential(username, authkey);
            request.ContentLength = putdata.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "HttpWebRequest";
            // Write data to stream
            Stream newStream = request.GetRequestStream();
            newStream.Write(putdata, 0, putdata.Length);
            WebResponse response = request.GetResponse();
            newStream.Close();

        }
    }
}