// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace PMStockQuote
{
    static class JObjectExtensions
    {
        public static object GetValueAsObject(this JObject jObject, string propertyName)
        {
            return (jObject != null && jObject.TryGetValue(propertyName, StringComparison.OrdinalIgnoreCase, out JToken token) && token is JValue value && value != null) ? value.Value : null;
        }

        public static object GetValueAsObject(this JObject jObject, string propertyName, string alternativePropertyName)
        {
            return jObject.GetValueAsObject(propertyName) ?? jObject.GetValueAsObject(alternativePropertyName);
        }

        public static string GetValueAsString(this JObject jObject, string propertyName)
        {
            return jObject.GetValueAsObject(propertyName)?.ToString();
        }

        public static string GetValueAsString(this JObject jObject, string propertyName, string alternativePropertyName)
        {
            return jObject.GetValueAsObject(propertyName)?.ToString() ?? jObject.GetValueAsObject(alternativePropertyName)?.ToString();
        }

        public static double GetValueAsDouble(this JObject jObject, string propertyName)
        {
            var value = jObject.GetValueAsObject(propertyName);

            if (value != null)
            {
                if (value is double)
                {
                    return (double)value;
                }

                if (value is string stringValue && double.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double doubleValue))
                {
                    return doubleValue;
                }
            }

            return double.MinValue;
        }
    }
}