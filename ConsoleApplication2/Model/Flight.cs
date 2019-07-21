namespace TestProject1
{
    public class Flight
    {
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public string Number { get; set; }
        public string Price { get; set; }

        public string Origin { get; set; }
        public string Destination { get; set; }

        public override string ToString()
        {
            return $"{nameof(DepartureTime)}: {DepartureTime}, {nameof(ArrivalTime)}: {ArrivalTime}, {nameof(Number)}: {Number}, {nameof(Price)}: {Price}, {nameof(Origin)}: {Origin}, {nameof(Destination)}: {Destination}";
        }
    }
}