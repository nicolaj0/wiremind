using ConsoleApplication2.Scraping;

namespace ConsoleApplication2.Model
{
    public class FlightNotAvailable : Flight
    {
        public FlightNotAvailable(Leg leg) :base(leg){}

        public override string ToString()
        {
            return $"{Leg} - NO flight available";
        }
    }
}