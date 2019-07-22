using ConsoleApplication2.Scraping;

namespace ConsoleApplication2.Model
{
    public class Flight
    {
        public  Leg Leg { get; set; }

        public Flight(Leg leg)
        {
            Leg = leg;
        }

        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string Number { get; set; }
        public string Price { get; set; }
        public override string ToString()
        {
            return $"{Number}|{Leg}|{DepartureTime}|{ArrivalTime}|{Price}€";
        }
    }
}