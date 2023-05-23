using DWC_BDD_Tests.Drivers;
using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using DWC_BDD_Tests.PageObjects;
using System;
using TechTalk.SpecFlow;
using System.Security.Policy;

namespace DWC_BDD_Tests.StepDefinitions
{
    [Binding]
    public class HomeStepDefinitions
    {
        private readonly HomePageObject _homePage;
        public HomeStepDefinitions(BrowserDriver browserDriver)
        {
            _homePage = new HomePageObject(browserDriver.Current);
        }

        [Given(@"I am a visitor")]
        public void GivenIAmAVisitor()
        {
            // nothing to do!!
        }

        [Given(@"I am not logged in")]
        public void GivenIAmNotLoggedIn()
        {
            //nothing here...
        }


        [When(@"I am on the ""([^""]*)"" page")]
        public void WhenIAmOnThePage(string pageName)
        {
            _homePage.GoTo(pageName);
        }

        [Then(@"The page footer contains ""([^""]*)""")]
        public void ThenThePageFooterContains(string p0)
        {
            _homePage.GetLogo().ToString().Contains(p0);
        }

        [When(@"I click on the ""([^""]*)"" Nav-Bar Link")]
        public void WhenIClickOnTheNav_BarLink(string p0)
        {
            _homePage.ClickNavBarHome();
            _homePage.GoTo("Home");
        }

        [Then(@"I should be redirected to ""([^""]*)""")]
        public void ThenIShouldBeRedirectedTo(string p0)
        {
           Assert.AreEqual(_homePage.GetURL(), p0);
        }


        [Then(@"The page title contains ""([^""]*)""")]
        public void ThenThePageTitleContains(string p0)
        {
            _homePage.GetTitle().Should().ContainEquivalentOf(p0, AtLeast.Once());
        }

        [Then(@"the page header contains ""([^""]*)""")]
        public void ThenThePageHeaderContains(string p0)
        {
            _homePage.GetHeader().ToString().Contains(p0);
        }

        [When(@"I click the navbar-toggler button")]
        public void WhenIClickTheNavbar_TogglerButton()
        {
            _homePage.ClickNavbarTogglerButton();
        }


    }
}
