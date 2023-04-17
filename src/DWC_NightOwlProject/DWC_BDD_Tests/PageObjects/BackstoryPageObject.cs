using DWC_BDD_Tests.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using DWC_BDD_Tests.Shared;
using System.Collections.ObjectModel;

namespace DWC_BDD_Tests.PageObjects
{
    public class BackstoryPageObject : PageObject
    {
        public BackstoryPageObject(IWebDriver webDriver) : base(webDriver) 
        {
            _pageName = "Backstory";
        }

        public IWebElement GetLogo() => _webDriver.FindElement(By.Id("dndLogo"));
    }
}
