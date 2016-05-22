using System.Diagnostics;
using EventSearch;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventSearchTests
{
    [TestClass()]
    public class GenerateEventsTests
    {
        [TestMethod()]
        public void CheckEventExistsTest_DoesExist()
        {
            GenerateEvents.EventArray = new Event[1];
            GenerateEvents.EventArray[0] = new Event(0, 1, 0, 0);
            GenerateEvents.CheckEventExists(0, 0);
            if (!GenerateEvents.EventExists)
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void CheckEventExistsTest_DoesNotExist()
        {
            GenerateEvents.EventArray = new Event[1];
            GenerateEvents.EventArray[0] = new Event(0, 1, 1, 0);
            GenerateEvents.CheckEventExists(0, 0);
            if (GenerateEvents.EventExists)
            {
                Assert.Fail();
            }
        }
    }
}