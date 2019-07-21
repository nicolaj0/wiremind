using System;

public class TripData
{
    private readonly string[] _args;

    public TripData(string[] args)
    {
        _args = args;
    }

    public TripData()
    {
        
    }


    public string Origin { get; set; }
    public string Destination { get; set; }
    public DateTime OutboundDate { get; set; }
    public DateTime InboundDate { get; set; }
}