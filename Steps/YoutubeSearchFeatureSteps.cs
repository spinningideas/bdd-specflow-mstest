using BoDi;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;
using TechTalk.SpecFlow;
using TestingBDDSpecFlowSelenium.Extensions;

namespace TestingBDDSpecFlowSelenium.Steps
{
    [Binding]
    public class YoutubeSearchFeatureSteps
    {
        private string searchKeyword;
        private static ChromeDriver driver;
        private const int DefaultWaitTime = 10;

        public YoutubeSearchFeatureSteps() {

            var options = new ChromeOptions();
            options.AddArguments(new string[]
            {
                        "--start-maximized",
                        //"--headless",
                        "--disable-popup-blocking"
            });
            driver = new ChromeDriver();
        }

        [Given(@"I have navigated to youtube website")]
        public void GivenIHaveNavigatedToYoutubeWebsite()
        {
            driver.Navigate().GoToUrl("https://www.youtube.com");
            Assert.IsTrue(driver.Title.ToLower().Contains("youtube"));
        }

        [Given(@"I have entered '(.*)' as a search keyword")]
        public void GivenIHaveEnteredAValueAsSearchKeyword(String searchString)
        {
            searchKeyword = searchString.ToLower();
            var searchInputBox = driver.WaitUntilFindElement(By.XPath("//input[@id='search']"));
            searchInputBox.SendKeys(searchKeyword);
        }

        [When(@"I press the search button")]
        public void WhenIPressTheSearchButton()
        {
            var searchButton = driver.WaitUntilFindElement(By.XPath("//button[@id='search-icon-legacy']"));
            searchButton.Click();
        }

        [Then(@"I should navigate to search results page and see my search keyword in the page")]
        public void ThenIShouldNavigateToSearchResultsPageAndSeeMySearchKeywordInThePage()
        {
            // TODO: Find more exact way to determine that search ran and produced results?
            var pageElementsWithSearchKeyword = driver.FindElementsByVisibleTextIgnoreCase(searchKeyword);
            var countPageElementsWithSearchKeyword = pageElementsWithSearchKeyword.Count;
            Assert.IsTrue(countPageElementsWithSearchKeyword > 0);
        }


        [AfterFeature]
        public static void CleanupAllTests()
        {
            if (driver != null)
            {
                driver.Dispose();
                driver = null;
            }
        }           

    }
}
