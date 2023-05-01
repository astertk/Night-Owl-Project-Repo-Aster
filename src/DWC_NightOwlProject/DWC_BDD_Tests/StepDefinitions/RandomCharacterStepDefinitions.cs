using DWC_BDD_Tests.Drivers;
using DWC_BDD_Tests.PageObjects;
using System;
using TechTalk.SpecFlow;

namespace DWC_BDD_Tests.StepDefinitions
{
    
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
        public void GivenIAmLoggedInAsARegisteredUser()
        {
            //Workshoppin
        }

        [When(@"I select the ""([^""]*)"" link on the homepage")]
        public void WhenISelectTheLinkOnTheHomepage(string characters)
        {
            _homePage.SelectCharactersLink();
        }
/*
        [Then(@"I am redirected to the Characters page")]
        public void ThenIAmRedirectedToTheCharactersPage()
        {
            Assert.True(_charactersPage.IsAtCharactersPage());
        }*/

        [Given(@"I select the ""([^""]*)"" button")]
        public void GivenISelectTheButton(string random)
        {
            throw new PendingStepException();
        }

        [Given(@"I am on the Characters page")]
        public void GivenIAmOnTheCharactersPage()
        {
            throw new PendingStepException();
        }

        [When(@"I click the Random button")]
        public void WhenIClickTheRandomButton()
        {
            throw new PendingStepException();
        }

        [Then(@"I am redirected to the Random page")]
        public void ThenIAmRedirectedToTheRandomPage()
        {
            throw new PendingStepException();
        }
    }
}
