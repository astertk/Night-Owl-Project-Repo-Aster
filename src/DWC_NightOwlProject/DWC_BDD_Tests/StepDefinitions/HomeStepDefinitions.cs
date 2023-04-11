using DWC_BDD_Tests.Drivers;
using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using DWC_BDD_Tests.Drivers;
using DWC_BDD_Tests.PageObjects;
using System;
using TechTalk.SpecFlow;

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
    }
}
