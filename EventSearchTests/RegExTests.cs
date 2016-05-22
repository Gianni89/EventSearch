using EventSearch;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventSearchTests
{
    [TestClass()]
    public class EventTests
    {
        [TestMethod()]
        public void RegExTest_ShouldNotMatch()
        {
            string[] shouldNotMatch = {"efe", "23,4", "4,34", "t,t", "5,,5", ",5,5", "5,5,", "4,4-"};

            foreach (var _string in shouldNotMatch)
            {
                var regExMatch = RegEx.CheckCurrentLocationFormat(_string);
                if (regExMatch)
                {
                    Assert.Fail();
                }
            }
        }

        [TestMethod()]
        public void RegExTest_ShouldMatch()
        {
            string[] shouldMatch = { "2,3", "-2,3", "2,-3", "-2,-3", "-10,-10", "10,-10", "-10,10", "10,10" };

            foreach (var _string in shouldMatch)
            {
                var regExMatch = RegEx.CheckCurrentLocationFormat(_string);
                if (!regExMatch)
                {
                    Assert.Fail();
                }
            }
        }
    }
}