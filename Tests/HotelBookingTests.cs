using NUnit.Framework;
using System;
using System.Web;
using System.Text;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace NUnit.Web.Tests
{
    [TestFixture("chrome", "latest", "Windows 7", "", "")]
    public class Tests
    {
        private IWebDriver _driver;
        private String browser;
        private String version;
        private String os;
        private String deviceName;
        private String deviceOrientation;

        public string homeUrl = "http://hotel-test.equalexperts.io/";

        public Tests(String browser, String version, String os, String deviceName, String deviceOrientation)
        {
            this.browser = browser;
            this.version = version;
            this.os = os;
            this.deviceName = deviceName;
            this.deviceOrientation = deviceOrientation;
        }

        [SetUp]
        public void Init()
        {
            DesiredCapabilities caps = new DesiredCapabilities();
            caps.SetCapability("platform", "WIN10");
            caps.SetCapability("browserName", "chrome");
            caps.SetCapability("version", "79");
            caps.SetCapability("deviceName", deviceName);
            caps.SetCapability("deviceOrientation", deviceOrientation);
            caps.SetCapability("username", "ab220ca5b1b949f9b6b750b800fb6d2d");
            caps.SetCapability("accessKey", "6ef2e9f8186691f40bd886604e7b6e41");
            caps.SetCapability("name", TestContext.CurrentContext.Test.Name);

            _driver = new RemoteWebDriver(new Uri("https://hub.testingbot.com/wd/hub"), caps, TimeSpan.FromSeconds(600));

        }

        public void NavigatetToTheHomepage()
        {
            _driver.Navigate().GoToUrl(homeUrl);
        }

        [Test]
        public void Test_Case_001_CreateNewBookingRecord()
        {
            //Arrange
            NavigatetToTheHomepage();
            IWebElement firstname, lastname, totalprice, checkin, checkout, savebutton;
            SelectElement selectElement;
            InitializeElements(out firstname, out lastname, out totalprice, out selectElement, out checkin, out checkout, out savebutton);

            //Act
            EnterValidValues(firstname, lastname, totalprice, checkin, checkout, selectElement);
            SaveRecord(savebutton);


            WaitForDatabaseChanges();

            //Assert
            VerifyNewRecordIsPersisted();
        }

        [Test]
        public void Test_Case_002_DeleteExistingBookingRecord()
        { 
            // Arrange
            NavigatetToTheHomepage();

            //Act
            FindAndDeleteNewRecord();
            WaitForDatabaseChanges();

            //Assert
            VerifyNewRecordIsRemoved();
        }

        private void FindAndDeleteNewRecord()
        {
            var table = _driver.FindElement(By.TagName("table"));
            var rows = table.FindElements(By.TagName("tr"));

            foreach (var row in rows)
            {
                if (row.Text.Contains("Doctor"))
                {
                    //Console.WriteLine(row.Text);

                    var tds = row.FindElements(By.TagName("a"));
                    foreach (var entry in tds)
                    {
                        Console.WriteLine(entry.Text);
                        entry.Click();
                    }
                }
            }
        }                

        private void VerifyNewRecordIsPersisted()
        {
            Assert.IsTrue(_driver.PageSource.Contains("Doctor"));
        }

        private void VerifyNewRecordIsRemoved()
        {
            Assert.IsFalse(_driver.PageSource.Contains("Doctor"));
        }

        private static void WaitForDatabaseChanges()
        {
            Thread.Sleep(5000);
        }

        private static void SaveRecord(IWebElement savebutton)
        {
            savebutton.Click();
        }

        private static void EnterValidValues(IWebElement firstname, IWebElement lastname, IWebElement totalprice, IWebElement checkin, IWebElement checkout, SelectElement selectElement)
        {
            firstname.SendKeys("Doctor");
            lastname.SendKeys("Who");
            totalprice.SendKeys("1000");
            selectElement.SelectByText("false");
            checkin.SendKeys("2020-12-25");
            checkout.SendKeys("2020-12-25");
        }

        private void InitializeElements(out IWebElement firstname, out IWebElement lastname, out IWebElement totalprice, out SelectElement selectElement, out IWebElement checkin, out IWebElement checkout, out IWebElement savebutton)
        {
            firstname = _driver.FindElement(By.Id("firstname"));
            lastname = _driver.FindElement(By.Id("lastname"));
            totalprice = _driver.FindElement(By.Id("totalprice"));
            IWebElement depositpaid = _driver.FindElement(By.Id("depositpaid"));
            selectElement = new SelectElement(depositpaid);
            checkin = _driver.FindElement(By.Id("checkin"));
            checkout = _driver.FindElement(By.Id("checkout"));
            savebutton = _driver.FindElement(By.CssSelector("#form > div:nth-child(1) > div:nth-child(7) > input:nth-child(1)"));
           
        }

        [TearDown]
        public void CleanUp()
        {
            //bool passed = TestContext.CurrentContext.Result.Outcome == Framework.Interfaces.TestStatus.Passed;
            //try
            //{
            //    // Logs the result to TestingBot
            //    ((IJavaScriptExecutor)_driver).ExecuteScript("tb:test-result=" + (passed ? "passed" : "failed"));
            //}
            //finally
            //{
            //    // Terminates the remote webdriver session
            //    _driver.Quit();
            //}
            _driver.Quit();
        }
    }
}