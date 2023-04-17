using DWC_BDD_Tests.Drivers;
using DWC_BDD_Tests.PageObjects;
using System;
using TechTalk.SpecFlow;

namespace DWC_BDD_Tests.StepDefinitions
{
    [Binding]
    public class BackstoryStepDefinitions
    {
        private readonly HomePageObject _homePage;
        private readonly BackstoryPageObject _backstoryPage;
        private readonly LoginPageObject _loginPage;
        public BackstoryStepDefinitions(BrowserDriver browserDriver)
        {
            _homePage = new HomePageObject(browserDriver.Current);
            _backstoryPage = new BackstoryPageObject(browserDriver.Current);
            _loginPage = new LoginPageObject(browserDriver.Current);
        }


        [Given(@"I am on the ""([^""]*)"" page")]
        public void GivenIAmOnThePage(string home)
        {
            _homePage.GoTo(home);
        }

        [When(@"I click the ""([^""]*)"" button")]
        public void WhenIClickTheButton(string pageName)
        {
            _backstoryPage.GoTo(pageName);
        }

        [Then(@"I should be shown the ""([^""]*)"" partial")]
        public void ThenIShouldBeShownThePartial(string p0)
        {
            _backstoryPage.GetLogo().ToString().Contains(p0);
        }

        [When(@"I am on the ""([^""]*)"" Page")]
        public void WhenIAmOnThePage(string backstory)
        {
            _homePage.GoTo(backstory);
        }

        [Then(@"I should be shown the ""([^""]*)"" Button")]
        public void ThenIShouldBeShownTheButton(string login)
        {
            _backstoryPage.GetLoginButton().ToString().Contains(login);
        }

        [Then(@"I should be shown the ""([^""]*)"" text")]
        public void ThenIShouldBeShownTheText(string p0)
        {
            _backstoryPage.GetForgotPasswordButton().ToString().Contains(p0);
        }

    }
}
