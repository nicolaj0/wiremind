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
using TestProject1;

namespace ConsoleApplication2
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var data = new TripData
            {
                Destination = "AMS",
                Origin = "ORY",
                InboundDate = DateTime.Now.AddMonths(3),
                OutboundDate = DateTime.Now.AddMonths(3).AddDays(20)
            };
            await new ToScrapper(data).Scrap();
        }
    }
}