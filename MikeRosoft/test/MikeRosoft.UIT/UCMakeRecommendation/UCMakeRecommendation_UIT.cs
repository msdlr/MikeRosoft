using System;
using System.Collections.Generic;
using System.Text;
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

namespace MikeRosoft.UIT.UCMakeRecommendation
{
    public class UCMakeRecommendation_UIT : IDisposable
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

        

        public UCMakeRecommendation_UIT()
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
            _URI = "https://localhost:44325/";
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

    }
}
