using DWC_BDD_Tests.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using DWC_BDD_Tests.Shared;
using System.Collections.ObjectModel;

namespace DWC_BDD_Tests.PageObjects
{
    public class ItemsPageObject : PageObject
    {
        public ItemsPageObject(IWebDriver webDriver) : base(webDriver)
        {
            _pageName = "Items";
        }
        public IWebElement RaritySelect => _webDriver.FindElement(By.Id("r0"));
        public IWebElement TypeSelect => _webDriver.FindElement(By.Id("r1"));
        public IWebElement KeyTraitInput => _webDriver.FindElement(By.Id("r2"));
        public IWebElement CreateItemButton => _webDriver.FindElement(By.Id("Create"));

        

        public void SelectRarity(string rarity)
        {
            SelectElement select = new SelectElement(RaritySelect);
            select.SelectByText(rarity);
        }
        public void SelectItemType(string itemType)
        {
            SelectElement select = new SelectElement(TypeSelect);
            select.SelectByText(itemType);
        }

        public void EnterKeyTrait(string keyTrait)
        {
            KeyTraitInput.Clear();
            KeyTraitInput.SendKeys(keyTrait);
        }

        public void CreateItem()
        {
            CreateItemButton.Click();
        }
        public string GetCurrentPage()
        {
            string currentUrl = _webDriver.Url;
            string[] segments = currentUrl.Split('/');
            return segments[segments.Length - 1];
        }

        public void NavigateToItems(string pageName)
        {
            string url = "https://localhost:7282/Items"; // Set the URL based on the pageName
            _webDriver.Navigate().GoToUrl(url);

        }
    }
}
