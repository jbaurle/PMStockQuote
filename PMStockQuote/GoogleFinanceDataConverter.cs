// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Script.Serialization;

namespace PMStockQuote
{
    class GoogleFinanceDataConverter : JavaScriptConverter
    {
        public override IEnumerable<Type> SupportedTypes
        {
            get { return new[] { typeof(GoogleFinanceData) }; }
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            var data = new GoogleFinanceData { Date = DateTime.Now };
        
            foreach (var key in dictionary.Keys)
            {
                switch (key)
                {
                    case "symbol":
                        data.Ticker = (dictionary[key] as string) ?? string.Empty;
                        break;
                    case "exchange":
                        data.Exchange = (dictionary[key] as string) ?? string.Empty;
                        break;
                    case "name":
                        data.Name = (dictionary[key] as string) ?? string.Empty;
                        break;
                    case "op":
                        data.Open = ConvertStringToDouble(dictionary[key] as string);
                        break;
                    case "l":
                        data.Last = ConvertStringToDouble(dictionary[key] as string);
                        break;
                    case "lo":
                        data.Low = ConvertStringToDouble(dictionary[key] as string);
                        break;
                    case "hi":
                        data.High = ConvertStringToDouble(dictionary[key] as string);
                        break;
                    case "lo52":
                        data.Low52 = ConvertStringToDouble(dictionary[key] as string);
                        break;
                    case "hi52":
                        data.High52 = ConvertStringToDouble(dictionary[key] as string);
                        break;
                    case "vo":
                        data.Volume = ConvertStringToDouble(dictionary[key] as string);
                        break;
                    case "c":
                        data.Change = ConvertStringToDouble(dictionary[key] as string);
                        break;
                    case "cp":
                        data.ChangePercentage = ConvertStringToDouble(dictionary[key] as string);
                        break;
                }
            }

            return data.Ticker == null && data.Exchange == null && data.Name == null ? null : data;
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            throw new NotSupportedException();
        }

        #region Helper Methods

        double ConvertStringToDouble(string stringValue)
        {
            return double.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var value) ? value : double.MinValue;
        }

        #endregion
    }
}