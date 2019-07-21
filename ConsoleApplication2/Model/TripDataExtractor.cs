using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class TripDataExtractor
{
    public  TripData Extract(string input)
    {
        var extracted = input.Split('|');

        var origin = extracted[0];
        var destination = extracted[1];

        var outboundDate = extracted[2];
        var inboundDate = extracted[3];


        var iata = new Dictionary<string, string>
        {
            {"PARIS", "ORY"},
            {"AMSTERDAM", "AMS"},
        };

        var data = new TripData
        {
            Origin = iata[origin.Trim()],
            Destination = iata[destination.Trim()],
        };
        var matchInbound = Regex.Match(inboundDate, @"today\(\) \+ ([0-9\-]+) days");
        var matchOutbound = Regex.Match(outboundDate, @"today\(\) \+ ([0-9\-]+) days");

        ;
        if (matchInbound.Success)
        {
            data.InboundDate = DateTime.UtcNow.AddDays(Int32.Parse(matchInbound.Groups[1].Value));
        }

        if (matchOutbound.Success)
        {
            data.OutboundDate = DateTime.UtcNow.AddDays(Int32.Parse(matchOutbound.Groups[1].Value));
        }

        return data;
    }
}