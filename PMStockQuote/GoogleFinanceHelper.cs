// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PMStockQuote
{
    static class GoogleFinanceHelper
    {
        public static IEnumerable<GoogleFinanceResult> SearchTicker(string query, int maxItems = 100)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new ArgumentNullException(nameof(query));
            }

            var url = string.Format(Constants.GoogleFinanceUrl, query);

            using (var client = new WebClient())
            {
                using (var stream = client.OpenRead(url))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var jObject = CreateJsonObject(reader);

                        if (jObject == null)
                        {
                            return Enumerable.Empty<GoogleFinanceResult>();
                        }

                        if (jObject.SelectToken("searchresults") is JArray searchResultsArray)
                        {
                            if (searchResultsArray.Count == 0)
                            {
                                return Enumerable.Empty<GoogleFinanceResult>();
                            }

                            var tickerList = new List<GoogleFinanceResult>();

                            foreach (var searchResult in searchResultsArray)
                            {
                                if (maxItems <= 0)
                                {
                                    break;
                                }

                                var name = (searchResult["title"] as JValue)?.Value as string ?? string.Empty;
                                var ticker = (searchResult["ticker"] as JValue)?.Value as string ?? string.Empty;
                                var exchange = (searchResult["exchange"] as JValue)?.Value as string ?? string.Empty;

                                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(ticker) && !string.IsNullOrEmpty(exchange))
                                {
                                    maxItems--;
                                    tickerList.Add(new GoogleFinanceResult { Name = name, Symbol = ticker, Exchange = exchange });
                                }
                            }

                            return tickerList;
                        }

                        var data = new GoogleFinanceResult
                        {
                            Name = (jObject["name"] as JValue)?.Value as string ?? string.Empty,
                            Symbol = (jObject["t"] as JValue)?.Value as string ?? string.Empty,
                            Exchange = (jObject["e"] as JValue)?.Value as string ?? string.Empty
                        };

                        if (string.IsNullOrEmpty(data.Symbol))
                        {
                            data.Symbol = (jObject["symbol"] as JValue)?.Value as string ?? string.Empty;
                        }

                        if (string.IsNullOrEmpty(data.Exchange))
                        {
                            data.Exchange = (jObject["exchange"] as JValue)?.Value as string ?? string.Empty;
                        }

                        if (string.IsNullOrEmpty(data.Name) || string.IsNullOrEmpty(data.Symbol) || string.IsNullOrEmpty(data.Exchange))
                            return Enumerable.Empty<GoogleFinanceResult>();

                        return new List<GoogleFinanceResult> { data };
                    }
                }
            }
        }

        public static string GetTickerData(string ticker)
        {
            if (string.IsNullOrWhiteSpace(ticker))
            {
                throw new ArgumentNullException(nameof(ticker));
            }

            var url = string.Format(Constants.GoogleFinanceUrl, ticker);

            using (var client = new WebClient())
            {
                using (var stream = client.OpenRead(url))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        var jResult = GetResult(reader);

                        if (jResult.Length > 0)
                        {
                            try
                            {
                                var json = JsonConvert.DeserializeObject(jResult);

                                return json.ToString();
                            }
                            catch (Exception e)
                            {
                                return $"An error occuried during processing the request.\n\nError:\n{e.Message}";
                            }
                        }

                        return string.Empty;
                    }
                }
            }
        }

        public static JObjectData GetQuote(string ticker)
        {
            if (string.IsNullOrWhiteSpace(ticker))
            {
                throw new ArgumentNullException(nameof(ticker));
            }

            var url = string.Format(Constants.GoogleFinanceUrl, ticker);

            using (var client = new WebClient())
            {
                using (var stream = client.OpenRead(url))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        return new JObjectData(CreateJsonObject(reader));
                    }
                }
            }
        }

        #region Helper Methods

        static string GetResult(StreamReader reader)
        {
            var jResult = reader.ReadToEnd() ?? string.Empty;

            // Google Finance API adds a comment before the JSON response for some
            // unknown reason, so we need to remove it!
            jResult = jResult.TrimStart('\n', '/', ' ');

            return jResult;
        }

        static JObject CreateJsonObject(StreamReader reader)
        {
            var jResult = GetResult(reader);
            var jObject = default(JObject);

            if (jResult.Length > 0)
            {
                try
                {
                    if (jResult[0] != '[')
                    {
                        jObject = JObject.Parse(jResult);
                    }
                    else
                    {
                        var jArray = JArray.Parse(jResult);

                        if ((jArray?.Count ?? 0) > 0)
                        {
                            jObject = jArray[0] as JObject;
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new InvalidDataException($"Unable to parse JSON ({e.Message}).");
                }
            }

            return jObject;
        }

        #endregion
    }
}