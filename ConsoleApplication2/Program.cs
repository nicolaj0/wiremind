using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Threading;
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
                var resDir = Directory.CreateDirectory("Results");
                var data = new List<Flight>();
                var timpeStamp = $"trips_{DateTime.UtcNow:yyyyMMddHHmmssfff}";

                foreach (var d in tripData.ToList())
                {
                    var trip = Trip.Build(d, allowedCodes);
                    Console.WriteLine($"launch query with {trip}");
                    var scrappedData = await scrapper.Scrap(trip);
                    data.AddRange(scrappedData);
                    scrappedData.ForEach(Console.WriteLine);
                    File.WriteAllLines(Path.Combine(resDir.FullName, timpeStamp),
                        data.Select(p => p.ToReport()).ToList());
                    Thread.Sleep(5000);
                }

                Console.WriteLine("Scraping session ended succesfully");
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
            finally
            {
                Console.ReadLine();
            }
        }
    }
}