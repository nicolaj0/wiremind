using System;
using ConsoleApplication2.Model;
using NUnit.Framework;

namespace TestProject1
{
    [TestFixture]
    public class TripTest
    {
        [Test]
        public void BuildTrip_ShouldReturnValidTrip_WhenValidInput()
        {
            var input = "PARIS | AGADIR | today() + 15 days | today() + 22 days";

            var res = Trip.Build(input, new IataCodes());

            Assert.IsNotNull(res);
        }

        [Test]
        public void BuildTrip_ShouldThrow_WhenInvalidInput()
        {
            var input = "PARIS | AMSTERDAM | today() + 15 days";

            Assert.Throws<ArgumentException>(() => Trip.Build(input, new IataCodes()));
        }
        
        [Test]
        public void BuildTrip_ShouldThrow_WhenInvalidDate()
        {
            var input = "PARIS | AMSTERDAM | today() + 15 days | today()";

            Assert.Throws<ArgumentException>(() => Trip.Build(input, new IataCodes()));
        }
        
        [Test]
        public void BuildTrip_ShouldThrow_WhenInvalidOrigin()
        {
            var input = "XXX | AMSTERDAM | today() + 15 days | today()";

            Assert.Throws<ArgumentException>(() => Trip.Build(input, new IataCodes()));
        }
    }
}