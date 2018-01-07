// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System.Diagnostics;

namespace PMStockQuote
{
    [DebuggerDisplay("{Exchange}:{Symbol} - {Name}")]
    class GoogleFinanceResult
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Exchange { get; set; }
    }
}