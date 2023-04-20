using DWC_BDD_Tests.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using DWC_BDD_Tests.Shared;
using System.Collections.ObjectModel;

namespace DWC_BDD_Tests.PageObjects
{
    public class CharactersPageObject : PageObject
    {
        public CharactersPageObject(IWebDriver webDriver) : base(webDriver)
        {
            _pageName = "Characters";
        }

        public IWebElement RandomButton => _webDriver.FindElement(By.Id("random-character"));

        public bool IsAtCharactersPage()
        {
            try
            {
                // Find a unique element on the characters page and verify it is displayed
                IWebElement element = _webDriver.FindElement(By.XPath("//h2[text()='Character Creation']"));
                return element.Displayed;
            }
            catch (NoSuchElementException)
            {
                // The element is not found, so we are not on the characters page
                return false;
            }
        }
    }
}
