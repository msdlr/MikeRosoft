using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
//Needed to find elements in ICollection or IList
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace MikeRosoft.UIT.Products
{
    public class UC_BuyProducts_UIT : IDisposable
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
        public UC_BuyProducts_UIT()
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
            //initial_step_opening_the_web_page();
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
        }

        private void second_step_accessing_link_Create_New()
        {
            _driver.FindElement(By.Id("SelectMoviesForPurchase")).Click();
        }



    }
}