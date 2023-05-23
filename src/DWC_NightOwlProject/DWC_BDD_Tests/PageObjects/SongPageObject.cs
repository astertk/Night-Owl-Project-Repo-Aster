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

        public IWebElement createButton => _webDriver.FindElement(By.Id("createSong"));

        public IWebElement songPurpose => _webDriver.FindElement(By.Id("songPurpose"));

        public IWebElement songName => _webDriver.FindElement(By.Id("songName"));

        public IWebElement email => _webDriver.FindElement(By.Id("email"));

        public IWebElement password => _webDriver.FindElement(By.Id("password"));

        public IWebElement login => _webDriver.FindElement(By.Id("login-submit"));


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

        public void ClickCreateSongButton()
        {
            createButton.Click();
        }

        public void PageRefresh()
        {
            _webDriver.Navigate().Refresh();
        }

        public void enterSongPurpose()
        {
            songPurpose.SendKeys("Combat");
        }

        public string songDetails()
        {
            return songName.ToString();
        }

        public void enterCredentials()
        {
            email.SendKeys("Jmama@hotmail.com");
            password.SendKeys("Coolbean$1");
            
        }

        public void submitLogin()
        {
            login.Click();
        }

    }
}