using System.Collections.Generic;
using TechTalk.SpecFlow;
using System.Linq;
using NUnit.Framework;

namespace TestingBDDSpecFlowSelenium.Steps
{
    [Binding]
    public class SimpleAdditionFeatureSteps
    {
        private readonly List<int> _numbers = new List<int>();
        private int _total;

        [Given(@"the first number is (.*)")]
        public void GivenTheFirstNumberIs(int p0)
        {
            _numbers.Add(p0);
        }
        
        [Given(@"the second number is (.*)")]
        public void GivenTheSecondNumberIs(int p0)
        {
            _numbers.Add(p0);
        }
        
        [When(@"the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            _total = _numbers.Sum();
        }
        
        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(int p0)
        {
            Assert.AreEqual(p0, _total);
        }
    }
}
