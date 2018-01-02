// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Collections.Generic;
using ExcelDna.Integration;

namespace PMStockQuote
{
    public static class UserDefinedFunctions
    {
        #region Fields

        static object _cacheLock = new object();
        static TimeoutWatch _cacheTimeout;
        static Dictionary<string, GoogleFinanceData> _cache = new Dictionary<string, GoogleFinanceData>();

        #endregion

        [ExcelFunction(Description = "Retrieves stock data for the passed symbol. The InfoCode defines the type of data that will be returned. InfoCode values: NAME, LAST (default), OPEN, LOW, HIGH and many more. See Help button for more information.")]
        public static object PSQ(string Symbol, string InfoCode)
        {
            return ExcelAsyncUtil.Run("PSQ", new object[] { Symbol, InfoCode }, () => PSQFunction(Symbol, InfoCode));
        }

        #region Helper Methods

        static object PSQFunction(string Symbol, string InfoCode)
        {
            if (string.IsNullOrWhiteSpace(Symbol))
                throw new Exception("Symbol must be defined.");
            if (string.IsNullOrWhiteSpace(InfoCode))
                InfoCode = "PRICE";

            var symbol = Symbol.Trim();
            var data = default(GoogleFinanceData);

            try
            {
                if (_cacheTimeout == null)
                {
                    lock (_cacheLock)
                        _cacheTimeout = new TimeoutWatch(TimeSpan.FromMinutes(1));
                }
                else if (_cacheTimeout.IsExpired)
                {
                    lock (_cacheLock)
                    {
                        _cache.Clear();
                        _cacheTimeout = new TimeoutWatch(TimeSpan.FromMinutes(1));
                    }
                }

                if (_cache.ContainsKey(symbol))
                    data = _cache[symbol];
                else
                {
                    data = GoogleFinanceHelper.GetQuote(symbol);

                    lock (_cacheLock)
                    {
                        if (_cache.ContainsKey(symbol))
                            _cache[symbol] = data;
                        else
                            _cache.Add(symbol, data);
                    }
                }

                if (data == null)
                    return ExcelError.ExcelErrorNA;

                switch (InfoCode.Trim().ToUpper())
                {
                    case "PRICE":
                    case "CLOSE":
                    case "LAST":
                    case "RATE":
                        return data.Last;
                    case "NAME":
                        return data.Name ?? string.Empty;
                    case "DATE":
                    case "DATELOCAL":
                        if (data.Date == DateTime.MinValue || data.Date == DateTime.MaxValue)
                            return ExcelError.ExcelErrorNA;
                        return data.Date.ToShortDateString();
                    case "TIME":
                    case "TIMELOCAL":
                        if (data.Date == DateTime.MinValue || data.Date == DateTime.MaxValue)
                            return ExcelError.ExcelErrorNA;
                        return data.Date.ToShortTimeString();
                    case "OPEN":
                        return data.Open;
                    case "LOW":
                        return data.Low;
                    case "HIGH":
                        return data.High;
                    case "LOW52":
                        return data.Low52;
                    case "HIGH52":
                        return data.High52;
                    case "VOL":
                    case "VOLUME":
                        return data.Volume;
                    case "CHG":
                    case "CHANGE":
                        return data.Change;
                    case "CP":
                    case "CHANGEIN%":
                    case "CHANGEPERCENTAGE":
                        return data.ChangePercentage;
                    case "SYMBOL":
                    case "TICKER":
                        return data.Ticker ?? string.Empty;
                    case "EXCHANGE":
                        return data.Exchange ?? string.Empty;
                    default:
                        return data.Last;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't retrieve and process data from Google Finance service. Internal message: " + e.Message);
            }
        }

        #endregion
    }
}