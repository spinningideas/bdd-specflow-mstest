using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;

namespace TestingBDDSpecFlowSelenium.Extensions
{
    internal static class WebDriverExtensions
    {
        private const int DefaultWaitTime = 10;

        private static readonly TimeSpan DefaultWaitTimeSpan = TimeSpan.FromSeconds(DefaultWaitTime);

        public static IWait<IWebDriver> Wait(this IWebDriver driver) => Wait(driver, DefaultWaitTimeSpan);
        public static IWait<IWebDriver> Wait(this IWebDriver driver, int waitTime) => Wait(driver, TimeSpan.FromSeconds(waitTime));
        public static IWait<IWebDriver> Wait(this IWebDriver driver, TimeSpan waitTimeSpan) => new WebDriverWait(driver, waitTimeSpan);

        public static IWebElement WaitUntilFindElement(this IWebDriver driver, By locator)
        {
            driver.Wait().Until(condition => ExpectedConditions.ElementIsVisible(locator));
            return driver.FindElement(locator);
        }

        public static IWebElement WaitUntilFindElement(this IWebDriver driver, By locator, Func<IWebDriver, IWebElement> condition)
        {
            driver.Wait().Until(condition);
            return driver.FindElement(locator);
        }

        public static IWebElement WaitUntilInitialPageLoad(this IWebDriver driver, string titleOnNewPage)
        {
            driver.Wait().Until(ExpectedConditions.TitleIs(titleOnNewPage));
            return driver.WaitUntilFindElementForPageLoadCheck();
        }

        public static IWebElement WaitUntilPageLoad(this IWebDriver driver, string titleOnNewPage, IWebElement elementOnOldPage)
        {
            driver.Wait().Until(ExpectedConditions.StalenessOf(elementOnOldPage));
            driver.Wait().Until(ExpectedConditions.TitleIs(titleOnNewPage));
            return driver.WaitUntilFindElementForPageLoadCheck();
        }

        private static IWebElement WaitUntilFindElementForPageLoadCheck(this IWebDriver driver) => driver.WaitUntilFindElement(By.XPath("html"));

        public static IReadOnlyCollection<IWebElement> FindElementsByVisibleTextIgnoreCase(this IWebDriver driver, string text)
        {
            return driver.FindElements(By.XPath(string.Format("//*[contains(translate(.,'ABCDEFGHIJKLMNOPQRSTUVWXYZ','abcdefghijklmnopqrstuvwxyz'),'{0}')]", text)));
        }

        public static void ScrollIntoView(this IWebDriver driver, IWebElement element)
        {
            // Assumes IWebDriver can be cast as IJavaScriptExecuter.
            ScrollIntoView((IJavaScriptExecutor)driver, element);
        }

        private static void ScrollIntoView(IJavaScriptExecutor driver, IWebElement element)
        {
            driver.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }
    }
}
