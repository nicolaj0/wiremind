using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using NUnit.Framework;

namespace TestProject1
{
    [TestFixture]
    public class TripDataExtractorTest
    {
        [Test]
        public void ExtractionTest()
        {
            var input = "PARIS | AMSTERDAM | today() + 15 days | today() + 22 days";

           var res = new TripDataExtractor().Extract(input);
        }


    }
}