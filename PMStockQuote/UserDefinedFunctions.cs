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
        static Dictionary<string, JObjectData> _cache = new Dictionary<string, JObjectData>();

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
            {
                throw new Exception("Symbol must be defined.");
            }

            if (string.IsNullOrWhiteSpace(InfoCode))
            {
                InfoCode = "PRICE";
            }

            var symbol = Symbol.Trim();
            var data = default(JObjectData);

            try
            {
                if (_cacheTimeout == null)
                {
                    lock (_cacheLock)
                    {
                        _cacheTimeout = new TimeoutWatch(TimeSpan.FromMinutes(1));
                    }
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
                {
                    data = _cache[symbol];

                    if (data == null)
                    {
                        return ExcelError.ExcelErrorNA;
                    }
                }
                else
                {
                    data = GoogleFinanceHelper.GetQuote(symbol);

                    if (data == null)
                    {
                        return ExcelError.ExcelErrorNA;
                    }

                    lock (_cacheLock)
                    {
                        if (_cache.ContainsKey(symbol))
                        {
                            _cache[symbol] = data;
                        }
                        else
                        {
                            _cache.Add(symbol, data);
                        }
                    }
                }

                InfoCode = InfoCode.Trim();

                switch (InfoCode.ToLowerInvariant())
                {
                    case "price":
                    case "close":
                    case "l":
                    case "last":
                    case "rate":
                        return data.GetValueAsDouble("l");
                    case "name":
                        return data.GetValueAsString("name");
                    case "date":
                    case "datelocal":
                        // Google Finance API is not returning the last trade date
                        return DateTime.Now.ToShortDateString();
                    case "time":
                    case "timelocal":
                        // Google Finance API is not returning the last trade time
                        return DateTime.Now.ToShortTimeString();
                    case "op":
                    case "open":
                        return data.GetValueAsDouble("op");
                    case "lo":
                    case "low":
                        return data.GetValueAsDouble("lo");
                    case "hi":
                    case "high":
                        return data.GetValueAsDouble("hi");
                    case "lo52":
                    case "low52":
                        return data.GetValueAsDouble("lo52");
                    case "hi52":
                    case "high52":
                        return data.GetValueAsDouble("hi52");
                    case "v":
                    case "vo":
                    case "volume":
                        return data.GetValueAsString("vo");
                    case "change":
                        return data.GetValueAsDouble("c");
                    case "cp":
                    case "changein%":
                    case "changepercentage":
                        return data.GetValueAsDouble("cp");
                    case "t":
                    case "symbol":
                    case "ticker":
                        return data.GetValueAsString("t", "symbol");
                    case "e":
                    case "exchange":
                        return data.GetValueAsString("e", "exchange");
                    default:
                        return data.GetValueFromPath(InfoCode);
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