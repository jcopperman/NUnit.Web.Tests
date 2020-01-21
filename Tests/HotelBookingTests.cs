using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
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
            //Arrange
            NavigatetToTheHomepage();
            IWebElement firstname, lastname, totalprice, checkin, checkout, savebutton;
            SelectElement selectElement;
            InitializeElements(out firstname, out lastname, out totalprice, out selectElement, out checkin, out checkout, out savebutton);

            //Act
            EnterValidValues(firstname, lastname, totalprice, checkin, checkout, selectElement);
            SaveRecord(savebutton);

            // Todo: Capture and store the row ID of the persisted record

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
        public void CloseBrowser()
        {
            _driver.Close();
        }
    }
}