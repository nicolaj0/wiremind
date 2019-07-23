using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI;
using ConsoleApplication2.Browser;
using ConsoleApplication2.Model;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication2.Scraping
{
    internal class Scrapper
    {
        private Trip _trip;
        private BrowserSession _browserSession;
        private HttpClient _client;
        private List<Flight> _result = new List<Flight>();
        private Cookie _token;
        private string _sessionDetectedAsBotMessage = "Session detected as bot - try changing IP";

        public Scrapper(BrowserSession browserSession)
        {
            _browserSession = browserSession;
        }

        public async Task<List<Flight>> Scrap(Trip trip)
        {
            _trip = trip;

            Init();
            TryExtractToken();

            PostTripData();
            await GetFlightData();

            return _result;
        }


        private void Init()
        {
            _browserSession.Get("https://www.transavia.com/fr-FR/accueil");
        }

        private void TryExtractToken()
        {
            try
            {
                _token = _browserSession.Cookies["__RequestVerificationToken"];
            }
            catch (Exception ex)
            {
                throw new AuthenticationException(_sessionDetectedAsBotMessage, ex);
            }
        }


        private List<Flight> ExtractFlights(Leg leg, string payload, string key)
        {
            try
            {
                var jObject = JObject.Parse(payload);
                return new ResponseParser((string) jObject[key], leg).ExtractFlights();
            }
            catch (Exception e)
            {
                throw new AuthenticationException(_sessionDetectedAsBotMessage, e);
            }
        }

        private void PostTripData()
        {
            _browserSession.FormElements["routeSelection.DepartureStation"] = _trip.Origin;
            _browserSession.FormElements["routeSelection.ArrivalStation"] = _trip.Destination;
            _browserSession.FormElements["dateSelection.OutboundDate.Day"] = _trip.OutboundDate.Day.ToString();
            _browserSession.FormElements["dateSelection.OutboundDate.Month"] = _trip.OutboundDate.Month.ToString();
            _browserSession.FormElements["dateSelection.OutboundDate.Year"] = _trip.OutboundDate.Year.ToString();
            _browserSession.FormElements["dateSelection.IsReturnFlight"] = "true";
            _browserSession.FormElements["dateSelection.InboundDate.Day"] = _trip.InboundDate.Day.ToString();
            _browserSession.FormElements["dateSelection.InboundDate.Month"] = _trip.InboundDate.Month.ToString();
            _browserSession.FormElements["dateSelection.InboundDate.Year"] = _trip.InboundDate.Year.ToString();
            _browserSession.FormElements["selectPassengersCount.AdultCount"] = "1";
            _browserSession.FormElements["selectPassengersCount.ChildCount"] = "0";
            _browserSession.FormElements["selectPassengersCount.InfantCount"] = "0";
            _browserSession.FormElements["flyingBlueSearch.FlyingBlueSearch"] = "false";

            _browserSession.Post("https://www.transavia.com/fr-FR/reservez-un-vol/vols/deeplink");
        }

        private async Task GetFlightData()
        {
            _client = _browserSession.InitHttpHandler();

            var outBound = await GetFlightAvailabilityReponse(JourneyType.OutboundFlight);
            var inBound = await GetFlightAvailabilityReponse(JourneyType.InboundFlight);

            _result.AddRange(ExtractFlights(_trip.Outbound, outBound, "SingleDayOutbound"));
            _result.AddRange(ExtractFlights(_trip.Inbound, inBound, "SingleDayInbound"));
        }

        private async Task<string> GetFlightAvailabilityReponse(JourneyType journeyType)
        {
            var date = _trip.GetLegDate(journeyType).ToString("yyyy-MM-dd");
            var url = "https://www.transavia.com/fr-FR/reservez-un-vol/vols/SingleDayAvailability/";
            var requestPayload = new Dictionary<string, string>
            {
                {"selectSingleDayAvailability.JourneyType", journeyType.ToString()},
                {"selectSingleDayAvailability.Date.DateToParse", date},
                {"selectSingleDayAvailability.AutoSelect", "true"},
                {"__RequestVerificationToken", _token.Value}
            };

            var request = new HttpRequestMessage(HttpMethod.Post, url)
                {Content = new FormUrlEncodedContent(requestPayload)};
            var response = await _client.SendAsync(request);
            var responsePayload = await response.Content.ReadAsStringAsync();

            return responsePayload;
        }
    }
}