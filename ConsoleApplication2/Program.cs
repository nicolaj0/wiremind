using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Threading.Tasks;
using ConsoleApplication2.Browser;
using ConsoleApplication2.Model;
using ConsoleApplication2.Scraping;

namespace ConsoleApplication2
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var allowedCodes = new IataCodes();
                var tripData = File.Exists("Scrap.txt") ? File.ReadAllLines("Scrap.txt") : args;
                var scrapper = new Scrapper(new BrowserSession());

                foreach (var d in tripData.ToList())
                {
                    var trip = Trip.Build(d, allowedCodes);
                    Console.WriteLine($"launch query with {trip}");
                    var scrappedData = await scrapper.Scrap(trip);
                    scrappedData.ForEach(Console.WriteLine);
                    var resDir = Directory.CreateDirectory("Results");
                    File.WriteAllLines(Path.Combine(resDir.FullName, $"{trip}_{DateTime.UtcNow:yyyyMMddHHmmssfff}"),
                        scrappedData.Select(p => p.ToString()).ToList());
                }
            }

            catch (Exception ex) when (ex is AuthenticationException || ex is ArgumentException)
            {
                Console.WriteLine(ex.Message);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Unhandled exception");
                Console.WriteLine(ex);
            }
        }
    }
}