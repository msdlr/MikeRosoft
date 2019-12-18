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

namespace MikeRosoft.UIT.Bans
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

        /* Testing of the pre-condition */

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
            _driver.Navigate().GoToUrl(_URI + "Identity/Account/Login");
            _driver.FindElement(By.Id("Input_Email")).SendKeys("ms@uclm.es");
            _driver.FindElement(By.Id("Input_Password")).SendKeys("Password1234%");
            _driver.FindElement(By.Id("login-submit")).Click();

            //Assert page title -> login is successful
            string expectedTitle = "Home Page - MikeRosoft";
            Assert.Equal(expectedTitle, _driver.Title);
        }

        /* Scenario 1 - Main Flow, Successful ban */
        [Fact]
        public void UC1_30_1() //Successful ban
        {

        }

        /* Scenario 2 - No users in the database */
        [Fact]
        public void UC1_30_2() //No users in db
        {

        }

        /* Scenario 3 - Filters */

        [Fact]
        public void UC1_30_3() //Filter by name
        {
            //Open page, login and go to Select webpage
            this.initial_step_opening_the_web_page();
            this.precondition_perform_login();
            this.selectBanUserFromIndex();

            //Check that we correctly get to select users
            Assert.Equal("Select User(s) to ban - MikeRosoft", _driver.Title);

            string[] expectedText = { "48484848B", "Elena", "Navarro", "Martínez" };

            var row = _driver.FindElements(By.Id("User_48484848B"));
            Assert.NotNull(row);

            //checks every column has those data expected
            foreach (string expected in expectedText)
                Assert.NotNull(row.First(l => l.Text.Contains(expected)));

            //Write the name in the filter
            this.selectUsersToBan_filter("A", null, null, null);

            //Check the one wit that name is displayed
            var rowAfterFilter = _driver.FindElements(By.Id("User_"+expectedText[0]));
            Assert.NotNull(row);

            //checks the row of the table
            foreach (string expected in expectedText)
                Assert.NotNull(rowAfterFilter.First(l => l.Text.Contains(expected)));

            this.logout();
        }

        [Fact]
        public void UC1_30_4() //Filter by 1st surname
        {

        }

        [Fact]
        public void UC1_30_5() //Filter by 2nd surname 
        {

        }

        [Fact]
        public void UC1_30_6() //Filter by DNI
        {

        }

        /* Scenario 3 - AF2, no users selected */

        [Fact]
        public void UC1_30_7() //No users selected
        {

        }

        /* Scenario 3 - Missing mandatory data in create view */

        [Fact]
        public void UC1_30_8() //No ban type selected
        {

        }

        [Fact]
        public void UC1_30_9() //Start date not selected
        {

        }

        [Fact]
        public void UC1_30_10() //End date not selected
        {

        }

        [Fact]
        public void UC1_30_11() //Start date < Today
        {

        }

        [Fact]
        public void UC1_30_12() //End date > Start date
        {

        }


        private void logout()
        {
            //Go to the page, login and go to SelectUsersToBan
            _driver.Navigate().GoToUrl(_URI + "/Identity/Account/Logout?returnUrl=%2F");

        }

        private void selectUsersToBan_filter(string NameSelected, string userSurname1, string userSurname2, string userDNI)
        {
            if (NameSelected != null)
            {
                _driver.FindElement(By.Id("NameSelected")).SendKeys(NameSelected);
                _driver.FindElement(By.Id("FilterButton")).Click();

            }

            if (userSurname1 != null)
            {
                _driver.FindElement(By.Id("userSurname1")).SendKeys(userSurname1);
                _driver.FindElement(By.Id("FilterButton")).Click();
            }

            if (userSurname2 != null)
            {
                _driver.FindElement(By.Id("userSurname2")).SendKeys(userSurname2);
                _driver.FindElement(By.Id("FilterButton")).Click();
            }

            if (userDNI != null)
            {
                _driver.FindElement(By.Id("userDNI")).SendKeys(userDNI);
                _driver.FindElement(By.Id("FilterButton")).Click();
            }

            //Click on the filter button
            _driver.FindElement(By.Id("FilterButton")).Click();
        }

        private void selectBanUserFromIndex()
        {
            //Go to the page, login and go to SelectUsersToBan
            _driver.Navigate().GoToUrl(_URI);
            precondition_perform_login();
            _driver.FindElement(By.Id("BansLink")).Click();

            string expectedTitle = "Select User(s) to ban - MikeRosoft";
            Assert.Equal(expectedTitle, _driver.Title);

            //This are the users expected on the database
            List<string[]> expectedUserList = new List<string[]>
            {
                new string[]{ "48484848B" ,"Elena","Navarro","Martínez" },
                new string[]{ "48484848C","Name","Surname","SurSurname" },
                new string[]{ "12345678J", "A","B","C"}
            };

            //Check that these users appear in the table
            foreach (var row in expectedUserList)
            {
                var r = _driver.FindElements(By.Id("User_" + row[0]));
                //Check that it is not null
                Assert.NotNull(r);
            }
        }

    }
}