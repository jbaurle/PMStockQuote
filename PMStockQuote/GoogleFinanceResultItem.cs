// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System.Diagnostics;

namespace PMStockQuote
{
    [DebuggerDisplay("{Exchange}:{Ticker} - {Title}")]
    class GoogleFinanceResultItem
    {
        public string Title { get; set; }
        public string Ticker { get; set; }
        public string Exchange { get; set; }
    }
}