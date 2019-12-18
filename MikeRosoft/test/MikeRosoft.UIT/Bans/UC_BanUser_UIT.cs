using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
//Needed to find elements in ICollection or IList
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace MikeRosoft.UIT.Products
{
    public class UC_BanUser_UIT : IDisposable
    {
        //Webdriver: A reference to the browser
        IWebDriver _driver;
        //A reference to the URI of the web page to test
        string _URI;
        //The code for your test Methods goes here
        void IDisposable.Dispose()
        {
            //To close and release all the resources allocated by the web driver
            _driver.Close();
            _driver.Dispose();
        }

        /* CONTSTRUCTOR */
        public UC_BanUser_UIT()
        {
            //Options to load the page and accept insecure certificates
            var optionsc = new ChromeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal,
                AcceptInsecureCertificates = true
            };
            //Instantiate the Chrome driver
            // Note: for other browser check the Project example
            _driver = new ChromeDriver(optionsc);
            //Maximum time the driver will wait for an element to appear.
            //See CreditCard in example
            _driver.Manage().Timeouts().ImplicitWait =
            TimeSpan.FromSeconds(50);
            //Application URI substitute by yours
            //_URI = "https://localhost:44327/";
            _URI = "https://localhost:5001/";
            //First actions needed by every test case
            initial_step_opening_the_web_page();
        }

        [Fact]
        public void initial_step_opening_the_web_page()
        {
            //Arrange
            string expectedTitle = "Home Page - MikeRosoft";
            string expectedText = "Register";
            //Act
            //The web driver Will navigate to the specified URI
            _driver.Navigate().GoToUrl(_URI);
            //Assert
            //Checks whether the title coincides with the expected one
            Assert.Equal(expectedTitle, _driver.Title);
            //Checks whether the page contains the given string
            Assert.Contains(expectedText, _driver.PageSource);
        }

        [Fact]
        public void precondition_perform_login()
        {

            _driver.Navigate().GoToUrl(_URI+"Identity/Account/Login");
            _driver.FindElement(By.Id("Input_Email")).SendKeys("ms@uclm.es");
            _driver.FindElement(By.Id("Input_Password")).SendKeys("Password1234%");
            _driver.FindElement(By.Id("login-submit")).Click();

            //Assert page title -> login is successful
            string expectedTitle = "Home Page - MikeRosoft";
            Assert.Equal(expectedTitle, _driver.Title);
        }

        [Fact]
        public void selectBanUserFromIndex()
        {
            //Go to the page, login and go to SelectUsersToBan
            _driver.Navigate().GoToUrl(_URI);
            precondition_perform_login();
            _driver.FindElement(By.Id("BansLink")).Click();

            string expectedTitle = "Select User(s) to ban - MikeRosoft";
            Assert.Equal(expectedTitle, _driver.Title);
        }
        private void logout()
        {
            //Go to the page, login and go to SelectUsersToBan
            _driver.Navigate().GoToUrl(_URI + "/Identity/Account/Logout?returnUrl=%2F");

        }

        [Fact]
        public void select_ClickFilterWithoutFilter()
        {
            //Open page, login and go to Select webpage
            this.initial_step_opening_the_web_page();
            this.precondition_perform_login();
            this.selectBanUserFromIndex();

            //Check that we correctly get to select users
            Assert.Equal("Select User(s) to ban - MikeRosoft", _driver.Title);

            string[] expectedText= { "48484848B","Elena","Navarro", "Martínez" };

            var row = _driver.FindElements(By.Id("User_48484848B"));
            Assert.NotNull(row);

            //checks every column has those data expected
            foreach (string expected in expectedText)
                Assert.NotNull(row.First(l => l.Text.Contains(expected)));


            //Click on the filter button
            _driver.FindElement(By.Id("FilterButton")).Click();

            var rowAfterFilter = _driver.FindElements(By.Id("User_48484848B"));
            Assert.NotNull(row);

            //checks every column has those data expected
            foreach (string expected in expectedText)
                Assert.NotNull(rowAfterFilter.First(l => l.Text.Contains(expected)));

            this.logout();

        }

        [Fact]
        public void select_ClickFilterWithName()
        {

        }

        [Fact]
        public void select_ClickFilter1stSurname()
        {

        }

        [Fact]
        public void select_ClickFilter2ndSurname()
        {

        }

        [Fact]
        public void select_ClickFilterDNI()
        {

        }

        [Fact]
        public void create_NoDataInput()
        {

        }

        [Fact]
        public void create_MissingReason()
        {

        }

        [Fact]
        public void create_MissingStartDate()
        {

        }

        [Fact]
        public void create_MissingEndDate()
        {

        }

        [Fact]
        public void create_withComment()
        {

        }

        [Fact]
        public void create_withoutComment()
        {

        }

        [Fact]
        public void details_Null()
        {

        }

        [Fact]
        public void details_NotNull()
        {

        }

    }
}