using DWC_BDD_Tests.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using DWC_BDD_Tests.Shared;
using System.Collections.ObjectModel;

namespace DWC_BDD_Tests.PageObjects
{
    public class SongPageObject : PageObject
    {
        public SongPageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "songs";
        }

        public IWebElement stopButton => _webDriver.FindElement(By.CssSelector("f"));



        public void NavigateToSongs()
        {
            _webDriver.Navigate().GoToUrl("https://localhost:7282/Songs");
        }
        public string GetPageName()
        {
            return _pageName;
        }
        public void ClickStopButton()
        {
            stopButton.Click();
        }

        public void PageRefresh()
        {
            _webDriver.Navigate().Refresh();
        }

    }
}