using System;

namespace ConsoleApplication2.Scraping
{
    public class Leg
    {
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $" {From} | {To}";
        }
    }
}