using ConsoleApplication2.Model;
using NUnit.Framework;

namespace TestProject1
{
    [TestFixture]
    public class TripDataExtractorTest
    {
        [Test]
        public void ExtractionTest()
        {
            var input = "PARIS | AMSTERDAM | today() + 15 days | today() + 22 days";

           var res = new TripDataExtractor().Extract(input);
        }


    }
}