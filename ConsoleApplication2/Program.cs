using System;
using System.Threading.Tasks;
using ConsoleApplication2.Model;
using ConsoleApplication2.Scraping;

namespace ConsoleApplication2
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var res = await new ToScrapper(new TripDataExtractor().Extract(args[0])).Scrap();
            res.ForEach(Console.WriteLine);
        }
    }
}