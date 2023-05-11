using DWC_BDD_Tests.Drivers;
using DWC_BDD_Tests.PageObjects;
using System;
using TechTalk.SpecFlow;

namespace DWC_BDD_Tests.StepDefinitions
{
    [Binding]
    public class MapsStepDefinitions
    {
        private readonly HomePageObject _homePage;
        private readonly MapsPageObject _mapsPage;
        public MapsStepDefinitions(BrowserDriver browserDriver)
        {
            _mapsPage = new MapsPageObject(browserDriver.Current);
            _homePage = new HomePageObject(browserDriver.Current);
    }
        [When(@"I navigate to the ""([^""]*)"" page")]
        public void WhenINavigateToThePage(string p0)
        {
            _homePage.GoTo("Maps");
        }



        [When(@"I click on the ""([^""]*)"" Button")]
        public void WhenIClickOnTheButton(string template)
        {
            throw new PendingStepException();
        }

        [When(@"I fill out the ""([^""]*)""")]
        public void WhenIFillOutThe(string p0)
        {
            throw new PendingStepException();
        }

      
    }
}
