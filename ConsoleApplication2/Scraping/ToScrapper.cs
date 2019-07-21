using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TestProject1;

internal class ToScrapper
{
    private readonly TripData _tripData;
    private BrowserSession _browserSession = new BrowserSession();
    private List<Flight> _flights= new List<Flight>();
    private HttpClient _client;

    public ToScrapper(TripData tripData)
    {
        _tripData = tripData;
    }
    public  async Task Scrap()
    {
        _browserSession.Get("https://www.transavia.com/fr-FR/accueil");
        
        TripRequest();
        InitFlightAvailibilityHttpHandler();
        await GetFlightDetails(JourneyType.OutboundFlight, _tripData.OutboundDate.ToString("yyyy-MM-dd"));
        await GetFlightDetails(JourneyType.InboundFlight, _tripData.InboundDate.ToString("yyyy-MM-dd"));

        _flights.ForEach(f => Console.WriteLine(f));
    }

    private void InitFlightAvailibilityHttpHandler()
    {
        var handler = new HttpClientHandler();
        handler.CookieContainer = new CookieContainer();
        handler.CookieContainer.Add(_browserSession.Cookies);
        _client = new HttpClient(handler);
        handler.UseCookies = true;
        // client.DefaultRequestHeaders.Accept = ""
        _client.DefaultRequestHeaders.Add("Accept", "*/*");
        var ua =
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.142 Safari/537.36";
        _client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
        _client.DefaultRequestHeaders.UserAgent.ParseAdd(ua);
        _client.DefaultRequestHeaders.Add("Accept-Language", "fr-FR,fr;q=0.9,en-US;q=0.8,en;q=0.7,de;q=0.6");
    }

    private void TripRequest()
    {
        
        Thread.Sleep(1000);
        _browserSession.FormElements["routeSelection.DepartureStation"] = _tripData.Origin;
        _browserSession.FormElements["routeSelection.ArrivalStation"] = _tripData.Destination;
        _browserSession.FormElements["dateSelection.OutboundDate.Day"] = _tripData.OutboundDate.Day.ToString();
        _browserSession.FormElements["dateSelection.OutboundDate.Month"] = _tripData.OutboundDate.Month.ToString();
        _browserSession.FormElements["dateSelection.OutboundDate.Year"] = _tripData.OutboundDate.Year.ToString();
        _browserSession.FormElements["dateSelection.IsReturnFlight"] = "true";
        _browserSession.FormElements["dateSelection.InboundDate.Day"] = _tripData.InboundDate.Day.ToString();
        _browserSession.FormElements["dateSelection.InboundDate.Month"] = _tripData.InboundDate.Month.ToString();
        _browserSession.FormElements["dateSelection.InboundDate.Year"] = _tripData.InboundDate.Year.ToString();
        _browserSession.FormElements["selectPassengersCount.AdultCount"] = "1";
        _browserSession.FormElements["selectPassengersCount.ChildCount"] = "0";
        _browserSession.FormElements["selectPassengersCount.InfantCount"] = "0";
        _browserSession.FormElements["flyingBlueSearch.FlyingBlueSearch"] = "false";
        
        _browserSession.Post("https://www.transavia.com/fr-FR/reservez-un-vol/vols/deeplink");
        
        Thread.Sleep(1000);
    }

    private async Task GetFlightDetails(JourneyType journeyType, string date)
    {
        var url = "https://www.transavia.com/fr-FR/reservez-un-vol/vols/SingleDayAvailability/";
        var dict = new Dictionary<string, string>();
        dict.Add("selectSingleDayAvailability.JourneyType", journeyType.ToString());
        dict.Add("selectSingleDayAvailability.Date.DateToParse", date);
        dict.Add("selectSingleDayAvailability.AutoSelect", "true");
        dict.Add("__RequestVerificationToken", _browserSession.Cookies["__RequestVerificationToken"].Value);
        var req = new HttpRequestMessage(HttpMethod.Post, url) {Content = new FormUrlEncodedContent(dict)};
        var res = await _client.SendAsync(req);
        var html = await res.Content.ReadAsStringAsync();
        var jObject = JObject.Parse(html);

        string ddd = String.Empty;
        switch (journeyType)
        {
            case JourneyType.OutboundFlight:
                ddd = (string) jObject["SingleDayOutbound"];
                break;
            case JourneyType.InboundFlight:
                ddd = (string) jObject["SingleDayInbound"];
                break;
        }
        
        _flights.AddRange(new FlightExtractor(ddd).ExtractFlight());
        
    }
}