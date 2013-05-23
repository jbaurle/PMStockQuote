// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using ExcelDna.Integration;

namespace PMStockQuote
{
	public static class UserDefinedFunctions
	{
		static object _cacheLock = new object();
		static TimeoutWatch _cacheTimeout;
		static Dictionary<string, string> _cache = new Dictionary<string, string>();

		[ExcelFunction(Description = "Retrieves stock data for the passed symbol. The InfoCode defines the type of data that will be returned. InfoCode values: NAME, DATE, TIME and PRICE (default).")]
		public static object PSQ(string Symbol, string InfoCode)
		{
			return ExcelAsyncUtil.Run("PSQ", new object[] { Symbol, InfoCode }, delegate {

				if(string.IsNullOrWhiteSpace(Symbol))
					throw new Exception("Symbol must be defined.");
				if(string.IsNullOrWhiteSpace(InfoCode))
					InfoCode = "PRICE";

				string symbol = Symbol.Trim();
				string content = string.Empty;

				try
				{
					if(_cacheTimeout == null)
					{
						lock(_cacheLock)
							_cacheTimeout = new TimeoutWatch(TimeSpan.FromMinutes(1));
					}
					else if(_cacheTimeout.IsExpired)
					{
						lock(_cacheLock)
						{
							_cache.Clear();
							_cacheTimeout = new TimeoutWatch(TimeSpan.FromMinutes(1));
						}
					}

					if(_cache.ContainsKey(symbol))
						content = _cache[symbol];
					else
					{
						WebClient client = new WebClient();
						Stream data = client.OpenRead("http://download.finance.yahoo.com/d/quotes.csv?s=" + symbol + "&f=sl1d1t1n");
						StreamReader reader = new StreamReader(data);
						content = reader.ReadToEnd();
						data.Close();
						reader.Close();

						lock(_cacheLock)
							_cache.Add(symbol, content);
					}
				}
				catch(Exception e)
				{
					throw new Exception("Couldn't retrieve and/or process financial data from internet service. Internal message: " + e.Message);
				}

				string[] quote = content.Split(",".ToCharArray());

				switch(InfoCode.Trim().ToUpper())
				{
					case "NAME":
						return quote[4].Replace("\"", "").Replace("\r", "").Replace("\n", "");
					case "DATE":
						if(quote[2] == "N/A")
							return ExcelError.ExcelErrorNA;
						return Convert.ToDateTime(quote[2].Trim("\"".ToCharArray()), CultureInfo.InvariantCulture).ToShortDateString();
					case "TIME":
						if(quote[3] == "N/A")
							return ExcelError.ExcelErrorNA;
						return Convert.ToDateTime(quote[3].Trim("\"".ToCharArray()), CultureInfo.InvariantCulture).ToShortTimeString();
					case "PRICE":
					default:
						if(quote[1] == "N/A")
							return ExcelError.ExcelErrorNA;
						return Convert.ToDouble(quote[1], CultureInfo.InvariantCulture);
				}

			});
		}
	}
}
