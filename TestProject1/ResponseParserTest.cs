using System.IO;
using ConsoleApplication2.Model;
using NUnit.Framework;

namespace TestProject1
{
    [TestFixture]
    public class ResponseParserTest
    {
        [Test]
        public void ExtractFlights_ShouldReturnFlights_WhenHtmlContainsData()
        {
            var path =
                @"C:\dev\projects\perso\app1\ConsoleApplication2\TestProject1\Files\InboundFlight.html";
            var fileInfo = new FileInfo(path);
            var res =new  ResponseParser(fileInfo).ExtractFlights();

            Assert.That(res[0].Number, Is.EqualTo("TO4011"));
            Assert.That(res[1].Number, Is.EqualTo("HV5193"));

        }
        
        [Test]
        public void ExtractFlights_ShouldReturnNoData_WhenHtmlContainsNoData()
        {
            var path =
                @"C:\dev\projects\perso\app1\ConsoleApplication2\TestProject1\Files\InboundFlightNoData.html";
            var fileInfo = new FileInfo(path);
            var res =new  ResponseParser(fileInfo).ExtractFlights();
            
            Assert.That(res[0] is FlightNotAvailable);
           
        }
        
    }
}