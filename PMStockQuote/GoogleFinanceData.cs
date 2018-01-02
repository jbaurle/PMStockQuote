// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Diagnostics;

namespace PMStockQuote
{
    [DebuggerDisplay("{Exchange}:{Ticker} - {Name}")]
    class GoogleFinanceData
    {
        public string Ticker { get; set; }
        public string Exchange { get; set; }
        public string Name { get; set; }
        public double Open { get; set; }
        public double Last { get; set; }
        public double Low { get; set; }
        public double High { get; set; }
        public double Low52 { get; set; }
        public double High52 { get; set; }
        public double Volume { get; set; }
        public double Change { get; set; }
        public double ChangePercentage { get; set; }
        public DateTime Date { get; set; }
    }
}