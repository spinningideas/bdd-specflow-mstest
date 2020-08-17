using OpenQA.Selenium;
using System;

namespace TestingBDDSpecFlowSelenium
{
    public class WebPage
    {
        public Uri BaseUrl { get; }

        public Uri PageUrl { get; }

        protected IWebDriver Driver { get; }

        public WebPage(IWebDriver driver, Uri baseUrl, string path)
        {
            string pathVal;

            Driver = driver ?? throw new ArgumentNullException(nameof(driver));
            BaseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
            pathVal = path ?? throw new ArgumentNullException(nameof(path));

            var builder = new UriBuilder(baseUrl);
            builder.Path = pathVal == string.Empty ? "/" : path;
            PageUrl = builder.Uri;
        }

        public virtual void Navigate()
        {
            Driver.Navigate().GoToUrl(PageUrl);
        }
    }
}
