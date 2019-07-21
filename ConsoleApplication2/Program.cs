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
            //var cl = "PARIS | AMSTERDAM | today() + 15 days | today() + 22 days";
            var res = await new ToScrapper(new TripDataExtractor().Extract(args[0])).Scrap();
        }
    }
}