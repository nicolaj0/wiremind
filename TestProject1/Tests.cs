﻿using System;
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
    public class Tests
    {
        [Test]
        public void ExtractionTest()
        {
            var path =
                @"C:\dev\projects\perso\app1\ConsoleApplication2\TestProject1\Files\InboundFlight.html";
            var fileInfo = new FileInfo(path);
            var res =new  FlightExtractor(fileInfo).ExtractFlight();

            Assert.That(res[0].Number, Is.EqualTo("TO4011"));
            Assert.That(res[1].Number, Is.EqualTo("HV5193"));

        }
    }
}