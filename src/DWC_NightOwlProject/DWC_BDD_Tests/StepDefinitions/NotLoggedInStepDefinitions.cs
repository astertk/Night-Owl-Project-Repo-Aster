using DWC_BDD_Tests.Drivers;
using DWC_BDD_Tests.PageObjects;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace DWC_BDD_Tests.StepDefinitions
{
    [Binding]
    public class NotLoggedInStepDefinitions
    {
        private readonly HomePageObject _homePage;
        private readonly MapsPageObject _mapsPage;
        public NotLoggedInStepDefinitions(BrowserDriver browserDriver)
        {
            _homePage = new HomePageObject(browserDriver.Current);
        }

        [When(@"I click on the ""([^""]*)"" NavBar Link")]
        public void WhenIClickOnTheNavBarLink(string p0)
        {
            _homePage.ClickNavBarWorld();
        }

        [When(@"I click on the Maps NavBar Link")]
        public void WhenIClickOnTheMapsNavBarLink()
        {
            _homePage.ClickNavBarMaps();
        }







    }
}