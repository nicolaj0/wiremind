using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ConsoleApplication2.Scraping;

namespace ConsoleApplication2.Model
{
    public class Trip
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime OutboundDate { get; set; }
        public DateTime InboundDate { get; set; }
        public Leg Inbound=> new Leg {From = Destination, To = Origin, Date = InboundDate};

        public Leg Outbound => new Leg {From = Origin, To = Destination, Date = OutboundDate};

        private Trip()
        {
        }
        
        

        public static Trip Build(string input, IataCodes iataCode)
        {
            var extracted = input.Split('|');

            string origin;
            string destination;
            string outboundDate;
            string inboundDate;
            try
            {
                origin = extracted[0].ToUpper();
                destination = extracted[1].ToUpper();
                outboundDate = extracted[2];
                inboundDate = extracted[3];
            }
            catch (Exception)
            {
                throw new ArgumentException("query must be of type : PARIS | AMSTERDAM | today() + 15 days | today() + 22 days");
            }

            var trip = new Trip();

            if (iataCode.TryGetValue(origin.Trim(), out var originCode))
            {
                trip.Origin = originCode;
            }
            else
            {
                throw new ArgumentException("unhandled origin city");
            }

            if (iataCode.TryGetValue(destination.Trim(), out var destinationCode))
            {
                trip.Destination = destinationCode;
            }
            else
            {
                throw new ArgumentException("unhandled destination city");
            }
            
            var matchInbound = Regex.Match(inboundDate, @"today\(\) \+ ([0-9\-]+) days");
            var matchOutbound = Regex.Match(outboundDate, @"today\(\) \+ ([0-9\-]+) days");

            
            if (matchInbound.Success)
            {
                trip.InboundDate = DateTime.UtcNow.Date.AddDays(Int32.Parse(matchInbound.Groups[1].Value));
            }
            else
            {
                throw new ArgumentException("could not parse outbound date");
            }

            if (matchOutbound.Success)
            {
                trip.OutboundDate = DateTime.UtcNow.Date.AddDays(Int32.Parse(matchOutbound.Groups[1].Value));
            }
            else
            {
                throw new ArgumentException("could not parse inbound date");
            }

            return trip;
        }

        public override string ToString()
        {
            return $"{Origin}_{Destination}_{OutboundDate:yy-MM-dd}_{InboundDate:yy-MM-dd}";
        }

        public DateTime GetLegDate(JourneyType journeyType)
        {
            return journeyType == JourneyType.InboundFlight ? InboundDate : OutboundDate;
        }
    }
}