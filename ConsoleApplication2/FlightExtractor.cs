using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using TestProject1;

public  class FlightExtractor
{
    private HtmlDocument _htmlDoc = new HtmlDocument();
    
    public FlightExtractor(FileInfo path)
    {
        _htmlDoc.Load(path.FullName);
    }

    public FlightExtractor(string html)
    {
        _htmlDoc.Load(html);
    }
    
    public  List<Flight> ExtractFlight()
    {
        var flightData = _htmlDoc.DocumentNode.SelectNodes("//button[@class='flight-result-button']");

        var flights = new List<Flight>();
        flightData.ToList().ForEach(n =>
        {
            var flight = new Flight();

            var dep = n.SelectSingleNode("div[@class='times']/time[@class='departure']").InnerText;
            var arrival = n.SelectSingleNode("div[@class='times']/time[@class='arrival']").InnerText;
            var flightNumber = n.SelectSingleNode("div[@class='details']//li[@class='flight-number']").InnerText;
            var price = n.SelectSingleNode("div[@class='actions']/div[@class='price ']").InnerText;

            flight.DepartureTime = dep.Trim();
            flight.ArrivalTime = arrival.Trim();
            flight.Number = flightNumber.Split(
                new[] {Environment.NewLine},
                StringSplitOptions.None
            )[2].Trim();
            flight.Price = Regex.Replace(price, @"[^\d]", "");

            flights.Add(flight);
        });

        return flights;

    }
}