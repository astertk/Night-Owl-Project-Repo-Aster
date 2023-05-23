using DWC_BDD_Tests.Drivers;
using DWC_BDD_Tests.PageObjects;
using DWC_BDD_Tests.Shared;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using OpenQA.Selenium;

namespace DWC_BDD_Tests.StepDefinitions
{
    [Binding]
    public class ItemCreationStepDefinitions
    {
        private readonly LoginPageObject _loginPage;
        private readonly HomePageObject _homePage;
        private readonly ItemsPageObject _itemsPage; // Add the ItemsPageObject reference
        public ItemCreationStepDefinitions(BrowserDriver browserDriver)
        {
            _loginPage = new LoginPageObject(browserDriver.Current);
            _homePage = new HomePageObject(browserDriver.Current);
            _itemsPage = new ItemsPageObject(browserDriver.Current); // Instantiate the ItemsPageObject
        }
        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            string loginUrl = "https://localhost:7282/Identity/Account/Login";
            _loginPage.NavigateToLoginPage(loginUrl);
        }

        [When(@"I enter valid credentials")]
        public void WhenIEnterValidCredentials()
        {
            _loginPage.EnterEmail("oneilmagno@gmail.com");
            _loginPage.EnterPassword("Leafyear29!");
        }

        [Then(@"I should be at the Home Page")]
        public void ThenIShouldBeAtTheHomePage()
        {
            _homePage.NavigateTo();
        }

        [Given(@"I am on the Home Page")]
        public void GivenIAmOnTheHomePage()
        {
            _homePage.NavigateTo();
            
        }

        [When(@"I navigate to the  ""([^""]*)"" page")]
        public void WhenINavigateToThePage(string items)
        {
            _homePage.NavigateToItems(items);
        }


        [When(@"I choose the rarity ""([^""]*)""")]
        public void WhenIChooseTheRarity(string common)
        {
            _itemsPage.SelectRarity(common);
        }

        [When(@"I choose the type ""([^""]*)""")]
        public void WhenIChooseTheType(string weapon)
        {
            _itemsPage.SelectItemType(weapon);
        }

        [When(@"I enter the key trait/word as ""([^""]*)""")]
        public void WhenIEnterTheKeyTraitWordAs(string powerful)
        {
            _itemsPage.EnterKeyTrait(powerful);
        }

        [Then(@"I submit the form")]
        public void WhenISubmitTheForm()
        {
            _itemsPage.CreateItem();
        }

    }
}
