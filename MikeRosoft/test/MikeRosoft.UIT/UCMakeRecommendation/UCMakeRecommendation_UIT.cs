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

        [Fact]
        public void precondition_perform_login()
        {

            _driver.Navigate().GoToUrl(_URI + "Identity/Account/Login");

            _driver.FindElement(By.Id("Input_Email"))
                        .SendKeys("ms@uclm.com");
            _driver.FindElement(By.Id("Input_Password"))
            .SendKeys("Password1234%");

            _driver.FindElement(By.Id("login-submit"))
                    .Click();

        }

        private void first_step_accessing_recomendation()
        {
            _driver.FindElement(By.Id("Recomendations")).Click();
        }
        private void second_step_accessing_link_Create_New()
        {
            _driver.FindElement(By.Id("SelectProductsForRecommendation")).Click();
        }
        private void third_filter_product_ByTitle(string titleFilter)
        {
            _driver.FindElement(By.Id("ptitle")).SendKeys(titleFilter);
            _driver.FindElement(By.Id("filter")).Click();
        }
        private void fourth_filter_product_ByPrice(string titleFilter)
        {
            _driver.FindElement(By.Id("pprice")).SendKeys(titleFilter);
            _driver.FindElement(By.Id("filter")).Click();
        }
        private void fifthfilter_product_ByRate(string titleFilter)
        {
            _driver.FindElement(By.Id("prate")).SendKeys(titleFilter);
            _driver.FindElement(By.Id("filter")).Click();
        }
        
        [Fact]
        public void alternate_flow_1_NoProductsAvailable()
        {
            //Arrange
            string expectedText = "Select Products- MikeRosoft";
            //Act
            precondition_perform_login();
            first_step_accessing_recomendation();
            second_step_accessing_link_Create_New();
            //Assert
            var title = _driver.Title;
            Assert.NotNull(_driver.FindElement(By.Id("No_Products")));
            Assert.Equal(expectedText, title);
        }
        
        [Fact]
        public void alternate_flow_2_filteringbyTitle()
        {
            //Arrange
            string[] expectedText = { "Teclado", "10", "4", "HP" };
            //Act
            precondition_perform_login();
            first_step_accessing_recomendation();
            second_step_accessing_link_Create_New();
            third_filter_product_ByTitle(expectedText[0]);
            //Assert
            var eventoRow = _driver.FindElements(By.Id("Product_" + expectedText[0]));
            //Comprueba que la tupla esperada existe
            Assert.NotNull(eventoRow);
            //Comprueba que todas las columnas tienen los datos esperados
            foreach (string expected in expectedText)
                Assert.NotNull(eventoRow.First(l => l.Text.Contains(expected)));
        }
        [Fact]
        public void alternate_flow_2_filteringbyPrice()
        {
            //Arrange
            string[] expectedText = { "Teclado", "10", "4", "HP" };
            //Act
            precondition_perform_login();
            first_step_accessing_recomendation();
            second_step_accessing_link_Create_New();
            fourth_filter_product_ByPrice(expectedText[1]);
            //Assert
            var eventoRow = _driver.FindElements(By.Id("Product_" + expectedText[0]));
            //Comprueba que la tupla esperada existe
            Assert.NotNull(eventoRow);
            //Comprueba que todas las columnas tienen los datos esperados
            foreach (string expected in expectedText)
                Assert.NotNull(eventoRow.First(l => l.Text.Contains(expected)));
        }
        [Fact]
        public void alternate_flow_2_filteringbyRate()
        {
            //Arrange
            string[] expectedText = { "Teclado", "10", "4", "HP" };
            //Act
            precondition_perform_login();
            first_step_accessing_recomendation();
            second_step_accessing_link_Create_New();
            fifthfilter_product_ByRate(expectedText[2]);
            //Assert
            var eventoRow = _driver.FindElements(By.Id("Product_" + expectedText[0]));
            //Comprueba que la tupla esperada existe
            Assert.NotNull(eventoRow);
            //Comprueba que todas las columnas tienen los datos esperados
            foreach (string expected in expectedText)
                Assert.NotNull(eventoRow.First(l => l.Text.Contains(expected)));
        }

        public void third_No_select_product()
        {
            _driver.FindElement(By.Id("SaveButton")).Click();
        }

        [Fact]
        public void alternate_flow_3_No_Product_Selected()
        {
            //Arrange
            string expectedText = "Select Products- MikeRosoft";
            //Act
            precondition_perform_login();
            first_step_accessing_recomendation();
            second_step_accessing_link_Create_New();
            third_No_select_product();
            //Assert
            var title = _driver.Title;


            Assert.Equal(title, expectedText);
        }
        private void third_select_product(string prod)
        {
            _driver.FindElement(By.Id("check_" + prod)).Click();
            _driver.FindElement(By.Id("SaveButton")).Click();
        }
        private void fourth_No_Field_Completed()
        {
            _driver.FindElement(By.Id("CreateButton")).Click();
        }
        [Fact]
        public void alternate_flow_4_No_Field_Completed()
        {
            //Arrange
            string[] product = { "Teclado", "10", "4", "HP" };
            string expectedtitle = "Create- MikeRosoft";
            //Act
            precondition_perform_login();
            first_step_accessing_recomendation();
            second_step_accessing_link_Create_New();
            third_select_product(product[0]);
            fourth_No_Field_Completed();
            //Assert
            var title = _driver.Title;


            Assert.Equal(title, expectedtitle);
        }
        private void fourth_Field_Completed(string string1, string string2)
        {
            _driver.FindElement(By.Id("recomtext")).SendKeys(string1);
            _driver.FindElement(By.Id("recomDesc")).SendKeys(string2);
            _driver.FindElement(By.Id("CreateButton")).Click();
        }
        [Fact]
        public void basic_flow_Recommendation_Completed()
        {
            //Arrange
            string[] product = { "Teclado", "10", "4", "HP" };
            string expectedtitle = "Details- MikeRosoft";
            //Act
            precondition_perform_login();
            first_step_accessing_recomendation();
            second_step_accessing_link_Create_New();
            third_select_product(product[0]);
            fourth_Field_Completed("Rec1", "Besto filmo");
            
            //Assert
            Assert.Equal(expectedtitle, _driver.Title);

        }





    }
}
