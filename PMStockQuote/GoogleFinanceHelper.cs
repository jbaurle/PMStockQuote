// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;

namespace PMStockQuote
{
    static class GoogleFinanceHelper
    {
        #region Fields

        static readonly JavaScriptSerializer _serializer;

        #endregion

        #region CTOR

        static GoogleFinanceHelper()
        {
            _serializer = new JavaScriptSerializer();
            _serializer.RegisterConverters(new[] { new GoogleFinanceDataConverter() });
        }

        #endregion

        public static IEnumerable<GoogleFinanceResultItem> SearchTicker(string query, int maxItems = 100)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException(nameof(query));

            var url = $"http://finance.google.com/finance?q={query}&output=json";

            using (var client = new WebClient())
            {
                using (var stream = client.OpenRead(url))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var result = reader.ReadToEnd();

                        if (result.StartsWith("\n// [") && result.EndsWith("}]\n"))
                        {
                            result = result.TrimStart('\n', '/', ' ', '[').TrimEnd(']', '\n');

                            var data = _serializer.Deserialize<GoogleFinanceData>(result);

                            return new List<GoogleFinanceResultItem> { new GoogleFinanceResultItem { Title = data.Name, Exchange = data.Exchange, Ticker = data.Ticker } };
                        }

                        return _serializer.Deserialize<GoogleFinanceResult>(result)?.SearchResults?.Where(i => !string.IsNullOrEmpty(i.Ticker) && !string.IsNullOrEmpty(i.Exchange)).Take(maxItems) ?? Enumerable.Empty<GoogleFinanceResultItem>();
                    }
                }
            }
        }

        public static GoogleFinanceData GetQuote(string ticker)
        {
            if (string.IsNullOrWhiteSpace(ticker))
                throw new ArgumentNullException(nameof(ticker));

            var url = $"http://finance.google.com/finance?q={ticker}&output=json";

            using (var client = new WebClient())
            {
                using (var stream = client.OpenRead(url))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var result = reader.ReadToEnd();

                        if (result.StartsWith("\n// [") && result.EndsWith("}]\n"))
                            result = result.TrimStart('\n', '/', ' ', '[').TrimEnd(']', '\n');

                        return _serializer.Deserialize<GoogleFinanceData>(result);
                    }
                }
            }
        }
    }
}