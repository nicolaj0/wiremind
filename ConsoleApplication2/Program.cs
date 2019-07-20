using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using ScrapySharp.Html.Forms;
using ScrapySharp.Network;

namespace ConsoleApplication2
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var b = new BrowserSession();
            b.Get("https://www.transavia.com/fr-FR/accueil");

            Thread.Sleep(1000);

            b.FormElements["routeSelection.DepartureStation"] = "ORY";
            b.FormElements["routeSelection.ArrivalStation"] = "AMS";
            b.FormElements["dateSelection.OutboundDate.Day"] = "17";
            b.FormElements["dateSelection.OutboundDate.Month"] = "10";
            b.FormElements["dateSelection.OutboundDate.Year"] = "2019";
            b.FormElements["dateSelection.IsReturnFlight"] = "true";
            b.FormElements["dateSelection.InboundDate.Day"] = "24";
            b.FormElements["dateSelection.InboundDate.Month"] = "10";
            b.FormElements["dateSelection.InboundDate.Year"] = "2019";
            b.FormElements["selectPassengersCount.AdultCount"] = "1";
            b.FormElements["selectPassengersCount.ChildCount"] = "0";
            b.FormElements["selectPassengersCount.InfantCount"] = "0";
            b.FormElements["flyingBlueSearch.FlyingBlueSearch"] = "false";

            var response = b.Post("https://www.transavia.com/fr-FR/reservez-un-vol/vols/deeplink");

            File.WriteAllText($"res{Guid.NewGuid()}.html", response);

            Thread.Sleep(1000);


            var handler = new HttpClientHandler();
            handler.CookieContainer = new CookieContainer();
            handler.CookieContainer.Add(b.Cookies);
            var client = new HttpClient(handler);
            handler.UseCookies = true;
            // client.DefaultRequestHeaders.Accept = ""
            client.DefaultRequestHeaders.Add("Accept", "*/*");
            var ua =
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.142 Safari/537.36";
            client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");

            client.DefaultRequestHeaders.UserAgent.ParseAdd(ua);

            client.DefaultRequestHeaders.Add("Accept-Language", "fr-FR,fr;q=0.9,en-US;q=0.8,en;q=0.7,de;q=0.6");
            var url = "https://www.transavia.com/fr-FR/reservez-un-vol/vols/SingleDayAvailability/";


            var dict = new Dictionary<string, string>();
            dict.Add("selectSingleDayAvailability.JourneyType", "OutboundFlight");
            dict.Add("selectSingleDayAvailability.Date.DateToParse", "2019-10-17");
            dict.Add("selectSingleDayAvailability.AutoSelect", "true");
            dict.Add("__RequestVerificationToken", b.Cookies["__RequestVerificationToken"].Value);
            var req = new HttpRequestMessage(HttpMethod.Post, url) {Content = new FormUrlEncodedContent(dict)};
            var res = await client.SendAsync(req);
            var html = await res.Content.ReadAsStringAsync();
            var jObject = Newtonsoft.Json.Linq.JObject.Parse(html);
            var ddd = (string) jObject["SingleDayOutbound"];
            File.WriteAllText($"OutboundFlight_{Guid.NewGuid()}.html", ddd);


            dict = new Dictionary<string, string>();
            dict.Add("selectSingleDayAvailability.JourneyType", "InboundFlight");
            dict.Add("selectSingleDayAvailability.Date.DateToParse", "2019-10-24");
            dict.Add("selectSingleDayAvailability.AutoSelect", "true");
            dict.Add("__RequestVerificationToken", b.Cookies["__RequestVerificationToken"].Value);
            req = new HttpRequestMessage(HttpMethod.Post, url) {Content = new FormUrlEncodedContent(dict)};
            res = await client.SendAsync(req);
            html = await res.Content.ReadAsStringAsync();
            jObject = Newtonsoft.Json.Linq.JObject.Parse(html);
            ddd = (string) jObject["SingleDayInbound"];
            File.WriteAllText($"InboundFlight_{Guid.NewGuid()}.html", ddd);

            
            // request.Headers["X-Requested-With"] = "XMLHttpRequest";

            //  request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";


            //var flight = b.PostXhr("https://www.transavia.com/fr-FR/reservez-un-vol/vols/SingleDayAvailability/");

            //https: //www.transavia.com/fr-FR/reservez-un-vol/vols/SingleDayAvailability/

            // File.WriteAllText($"res2{Guid.NewGuid()}.html", flight);
            /*ScrapingBrowser Browser = new ScrapingBrowser();
            Browser.AllowAutoRedirect = true; // Browser has settings you can access in setup
            Browser.AllowMetaRedirect = true;
            WebPage PageResult = Browser.NavigateToPage(new Uri("https://www.transavia.com/fr-FR"));

            PageWebForm form = PageResult.FindFormById("desktop");

            form["routeSelection.DepartureStation"] = "ORY";
            form["routeSelection.ArrivalStation"] = "AGA";
            form["dateSelection.OutboundDate.Day"] = "17";
            form["dateSelection.OutboundDate.Month"] = "7";
            form["dateSelection.OutboundDate.Year"] = "2019";
            form["dateSelection.IsReturnFlight"] = "true";
            form["dateSelection.InboundDate.Day"] = "24";
            form["dateSelection.InboundDate.Month"] = "7";
            form["dateSelection.InboundDate.Year"] = "2019";
            form["selectPassengersCount.AdultCount"] = "1";
            form["selectPassengersCount.ChildCount"] = "0";
            form["selectPassengersCount.InfantCount"] = "0";
            form["flyingBlueSearch.FlyingBlueSearch"] = "false";

            form.Method = HttpVerb.Post;
            WebPage resultsPage = form.Submit();

            File.WriteAllText("res.html", resultsPage.Content);*/
        }
    }
}