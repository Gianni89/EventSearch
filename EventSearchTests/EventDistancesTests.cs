using Microsoft.VisualStudio.TestTools.UnitTesting;
using EventSearch;

namespace EventSearchTests
{
    [TestClass()]
    public class EventDistancesTests
    {
        [TestMethod()]
        public void CalculateDistanceTest()
        {
            var calculatedDistance1 = EventDistances.CalculateDistance(0, 0, 1, 1);
            var calculatedDistance2 = EventDistances.CalculateDistance(-1, -1, 0, 0);
            Assert.AreEqual(calculatedDistance1, 2);
            Assert.AreEqual(calculatedDistance2, 2);
        }
    }
}
    