namespace TestProject1
{
    public class FlightNotAvailable : Flight
    {
        public override string ToString()
        {
            return $"{nameof(Origin)}: {Origin}, {nameof(Destination)}: {Destination} - NO flight available";
        }
    }
}