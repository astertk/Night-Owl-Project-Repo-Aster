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
    public class NavigationMenuCanBeToggledStepDefinitions
    {
        private readonly HomePageObject _homePage;
        public NavigationMenuCanBeToggledStepDefinitions(BrowserDriver browserDriver)
        {
            _homePage = new HomePageObject(browserDriver.Current);
        }

        [Given(@"Given a visitor is on the homepage")]
        public void GivenGivenAVisitorIsOnTheHomepage()
        {
            _homePage.NavigateTo();
            Assert.AreEqual("Home", _homePage.GetPageName());
        }

        [When(@"When the vistor clicks the navbar-toggler button")]
        public void WhenWhenTheVistorClicksTheNavbar_TogglerButton()
        {
            _homePage.ClickNavbarTogglerButton();
        }

        [Then(@"the navigation menu should appear on the screen")]
        public void ThenTheNavigationMenuShouldAppearOnTheScreen()
        {
            var offcanvasNavbar = _homePage.offcanvasNavbar;
            Assert.IsTrue(offcanvasNavbar.Displayed);
        }

        [Given(@"the vistor has opened the navigation menu")]
        public void GivenTheVistorHasOpenedTheNavigationMenu()
        {
            var offcanvasNavbar = _homePage.offcanvasNavbar;
            Assert.IsTrue(offcanvasNavbar.Displayed);
            
        }

        
        [When(@"the vistor clicks the close button on the navigation menu")]
        public void WhenTheVistorClicksTheCloseButtonOnTheNavigationMenu()
        {
            _homePage.ClickOffcanvasCloseButton();
            
        }


        [Then(@"navigation menu should disappear from the screen")]
        public void ThenNavigationMenuShouldDisappearFromTheScreen()
        {
            var offcanvasNavbar = _homePage.offcanvasNavbar;
            Assert.IsTrue(offcanvasNavbar.Displayed);

        }
    }
}
