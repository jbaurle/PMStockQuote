// Copyright © Jürgen Bäurle, http://www.parago.de
// This code released under the terms of the Microsoft Public License (MS-PL)

using System.Collections.Generic;
using System.Globalization;
using ExcelDna.Integration;
using Newtonsoft.Json.Linq;

namespace PMStockQuote
{
    class JObjectData
    {
        #region Fields

        JObject _jObject;
        Dictionary<string, object> _jVCache;

        #endregion

        public JObjectData(JObject jObject)
        {
            _jObject = jObject;
            _jVCache = new Dictionary<string, object>(5);
        }

        public object GetValueAsString(string propertyName)
        {
            if (!TryGetValueFromCache(propertyName, out string value))
            {
                value = SetValueToCache(propertyName, _jObject?.GetValueAsString(propertyName));
            }

            if (value != null)
            {
                return value;
            }
            else
            {
                return ExcelError.ExcelErrorNA;
            }
        }

        public object GetValueAsString(string propertyName, string alternativePropertyName)
        {
            if (!TryGetValueFromCache(propertyName, out string value))
            {
                value = SetValueToCache(propertyName, _jObject?.GetValueAsString(propertyName, alternativePropertyName));
            }

            if (value != null)
            {
                return value;
            }
            else
            {
                return ExcelError.ExcelErrorNA;
            }
        }

        public object GetValueAsDouble(string propertyName)
        {
            if (!TryGetValueFromCache(propertyName, out double value))
            {
                value = SetValueToCache(propertyName, _jObject?.GetValueAsDouble(propertyName) ?? double.MinValue);
            }

            if (value != double.MinValue)
            {
                return value;
            }
            else
            {
                return ExcelError.ExcelErrorNA;
            }
        }

        public object GetValueFromPath(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (TryGetValueFromCache(path, out object value))
                {
                    return value;
                }

                // The sign (!) to convert the value to double is only used if others characters are appended to the path
                var returnAsDouble = path.StartsWith("!") && path.Length > 1;

                value = GetValueFromPathAsString(returnAsDouble ? path.Substring(1) : path);

                if (value is string stringValue)
                {
                    if (returnAsDouble && double.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double doubleValue))
                    {
                        return SetValueToCache(path, doubleValue);
                    }

                    return SetValueToCache(path, value);
                }
            }

            return ExcelError.ExcelErrorNA;
        }

        public object GetValueFromPathAsString(string path)
        {
            try
            {
                var segments = path.Split(new char[] { '/' });
                var data = _jObject as JContainer;
                var value = default(string);

                foreach (var segment in segments)
                {
                    if (!string.IsNullOrEmpty(segment))
                    {
                        if (data is JObject dataObject)
                        {
                            if (dataObject.TryGetValue(DecodeSpecialCharacters(segment), out JToken token))
                            {
                                if (token is JContainer item)
                                {
                                    data = item;
                                    continue;
                                }
                                else if (token is JValue jValue)
                                {
                                    value = jValue.Value?.ToString() ?? string.Empty;
                                }
                            }
                        }
                        else if (data is JArray dataArray)
                        {
                            if (int.TryParse(segment, out int index) && index >= 0 && index < dataArray.Count)
                            {
                                data = dataArray[index] as JContainer;
                                continue;
                            }
                        }
                    }

                    break;
                }

                if (value != null)
                {
                    return value;
                }
            }
            catch { }

            return ExcelError.ExcelErrorNA;
        }

        #region Helper Methods

        bool TryGetValueFromCache<T>(string key, out T value)
        {
            if (_jVCache.TryGetValue(key, out object objectValue) && objectValue is T typeValue)
            {
                value = typeValue;
                return true;
            }

            value = default(T);
            return false;
        }

        T SetValueToCache<T>(string key, T value)
        {
            key = key ?? string.Empty;

            if (_jVCache.ContainsKey(key))
            {
                _jVCache[key] = value;
            }
            else
            {
                _jVCache.Add(key, value);
            }

            return value;
        }

        string DecodeSpecialCharacters(string value)
        {
            return (value ?? string.Empty).Replace("%2F", "/").Replace("%21", "!");
        }

        #endregion
    }
}