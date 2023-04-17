using DWC_BDD_Tests.Drivers;
using DWC_BDD_Tests.PageObjects;
using System;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace DWC_BDD_Tests.StepDefinitions
{
    [Binding]
    public class RandomCharacterStepDefinitions
    {
        private readonly CharactersPageObject _charactersPage;
        private readonly HomePageObject _homePage;
        public RandomCharacterStepDefinitions(BrowserDriver browserDriver)
        {
            _charactersPage = new CharactersPageObject(browserDriver.Current);
            _homePage = new HomePageObject(browserDriver.Current);
        }


        [Given(@"I am logged in as a registered user")]
        public void GivenIAmLoggedInAsARegisteredUser(string home)
        {
            _homePage.GoTo(home);
        }

        [When(@"I select the ""([^""]*)"" link on the homepage")]
        public void WhenISelectTheLinkOnTheHomepage(string characters)
        {
            _homePage.SelectCharactersLink();
        }

        [Then(@"I am redirected to the Characters page")]
        public void ThenIAmRedirectedToTheCharactersPage()
        {
            Assert.True(_charactersPage.IsAtCharactersPage());
        }
    }
}
