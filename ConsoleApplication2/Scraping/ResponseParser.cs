using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ConsoleApplication2.Scraping;
using HtmlAgilityPack;

namespace ConsoleApplication2.Model
{
    public  class ResponseParser
    {
        private readonly Leg _leg;
        private readonly HtmlDocument _htmlDoc = new HtmlDocument();
        private readonly List<Flight> _extractFlight = new List<Flight>();

        public ResponseParser(FileInfo path)
        {
            _htmlDoc.Load(path.FullName);
        }

        public ResponseParser(string html, Leg leg)
        {
            _leg = leg;
            _htmlDoc.LoadHtml(html);
        }
    
        public  List<Flight> ExtractFlights()
        {
            var flightData = _htmlDoc.DocumentNode.SelectNodes("//button[@class='flight-result-button']");

            if (flightData == null)
            {
                return new List<Flight>{new FlightNotAvailable(_leg)};
            }
        
            flightData.ToList().ForEach(n =>
            {
                var flight = new Flight(_leg);

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

                _extractFlight.Add(flight);
            });

            return _extractFlight;

        }
    }
}