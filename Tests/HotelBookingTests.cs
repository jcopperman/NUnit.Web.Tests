using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace NUnit.Web.Tests
{
    public class Tests
    {
        IWebDriver _driver;

        public string homeUrl = "http://hotel-test.equalexperts.io/";

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();

        }
        public void NavigatetToTheHomepage()
        {
            _driver.Navigate().GoToUrl(homeUrl);
        }

        [Test]
        public void Test_Case_001_CreateNewBookingRecord()
        {
            NavigatetToTheHomepage();
            IWebElement firstname, lastname, totalprice, checkin, checkout, savebutton;
            SelectElement selectElement;
            InitializeElements(out firstname, out lastname, out totalprice, out selectElement, out checkin, out checkout, out savebutton);
            EnterValidValues(firstname, lastname, totalprice, checkin, checkout, selectElement);
            SaveRecord(savebutton);
            WaitForRecordToPersist();

            Assert.Pass();
        }

        [Test]
        public void Test_Case_002_DeleteExistingBookingRecord()
        {
            //TODO: Find the row ID create by the create function to determine which delete button corresponds. 
            NavigatetToTheHomepage();

            Thread.Sleep(5000);
            Assert.Pass();
        }

        private static void WaitForRecordToPersist()
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
        public void CloseBrowser()
        {
            _driver.Close();
        }
    }
}