using DWC_BDD_Tests.Drivers;
using DWC_BDD_Tests.PageObjects;
using System;
using TechTalk.SpecFlow;

namespace DWC_BDD_Tests.StepDefinitions
{
    [Binding]
    public class SongsStepDefinitions
    {
        private readonly HomePageObject _homePage;
        private readonly SongPageObject _songPage;

        public SongsStepDefinitions(BrowserDriver browserDriver)
        {
            _homePage = new HomePageObject(browserDriver.Current);
            _songPage = new SongPageObject(browserDriver.Current);
        }

        [Given(@"I am a user with the first name '([^']*)'")]
        public void GivenIAmAUserWithTheFirstName(string jade)
        {
            throw new PendingStepException();
        }

        [When(@"I click on the songs Nav-Bar Link")]
        public void WhenIClickOnTheSongsNav_BarLink()
        {
            _homePage.ClickNavBarSongs();
        }

        [When(@"I navigate to the songs index page")]
        public void WhenINavigateToTheSongsIndexPage()
        {
            _songPage.NavigateToSongs();
        }


        [When(@"I click the stop button")]
        public void WhenIClickTheStopButton()
        {
           _songPage.ClickStopButton();
        }

        [Then(@"the page should refresh")]
        public void ThenThePageShouldRefresh()
        {
            _songPage.PageRefresh();
        }

    }
}
