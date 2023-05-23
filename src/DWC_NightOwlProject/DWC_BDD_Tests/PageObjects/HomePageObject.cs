using DWC_BDD_Tests.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using DWC_BDD_Tests.Shared;
using System.Collections.ObjectModel;

namespace DWC_BDD_Tests.PageObjects
{
    public class HomePageObject : PageObject
    {
        public HomePageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "Home";
        }

        public IWebElement RegisterButton => _webDriver.FindElement(By.Id("register-link"));
        public IWebElement NavBarHelloLink => _webDriver.FindElement(By.CssSelector("a[href=\"/Identity/Account/Manage\"]"));
        public IWebElement BackstoryButton => _webDriver.FindElement(By.Id("backstoryButton"));
        public IWebElement CharactersButton => _webDriver.FindElement(By.Id("Nav_Bar_Character"));

        public IWebElement NavbarTogglerButton => _webDriver.FindElement(By.CssSelector("button.navbar-toggler"));
        public IWebElement offcanvasNavbar => _webDriver.FindElement(By.Id("offcanvasNavbar"));

        public IWebElement offcanvasCloseButton => _webDriver.FindElement(By.CssSelector("button.btn-close"));


        public void NavigateTo()
        {
            _webDriver.Navigate().GoToUrl("https://localhost:7282/");
        }
        public void NavigateToItems(string pageName)
        {
            string url = "https://localhost:7282/Items"; // Set the URL based on the pageName
            _webDriver.Navigate().GoToUrl(url);
            
        }
        public string GetPageName()
        {
            return _pageName;
        }
        public string NavbarWelcomeText()
        {
            return NavBarHelloLink.Text;
        }

        public void SelectCharactersLink()
        {
            IWebElement charactersLink = _webDriver.FindElement(By.Id("Nav_Bar_Character"));
            charactersLink.Click();
        }

        public void Logout()
        {
            IWebElement navbarLogoutButton = _webDriver.FindElement(By.Id("logout-button"));
            navbarLogoutButton.Click();
        }
        public void ClickNavbarTogglerButton()
        {
            NavbarTogglerButton.Click();
        }
        public void ClickOffcanvasCloseButton()
        {
            offcanvasCloseButton.Click();
        }
    }
}