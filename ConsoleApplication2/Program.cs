using System;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
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
                var allowedCodes = new IataCodes
                {
                    {"PARIS", "ORY"},
                    {"AMSTERDAM", "AMS"},
                };
                var tripData = Trip.Build(args[0], allowedCodes);
                var scrappedData = await new Scrapper(tripData).Scrap();
                scrappedData.ForEach(Console.WriteLine);
                File.WriteAllLines($"{tripData}_{DateTime.UtcNow:yyyyMMddHHmmssfff}",
                    scrappedData.Select(p => p.ToString()).ToList());
                Console.ReadLine();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unhadled exeption");
                
            }
        }
    }
}