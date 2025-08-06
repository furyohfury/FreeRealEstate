using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;

namespace Utils
{
    public static class ParseUtils
    {
        public static Dictionary<string, float> StringToStringFloatDictionary(
            string str,
            char separator = ',',
            char valueSeparator = ':')
        {
            List<string> cards = StringToStringList(str, separator);
            Dictionary<string, float> requiredCards = new Dictionary<string, float>();
            for (var index = 0; index < cards.Count; index++)
            {
                string card = cards[index];
                string[] s = card.Split(valueSeparator);
                if (s.Length == 2)
                {
                    if (float.TryParse(s[1], NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat, out float value))
                    {
                        if (!requiredCards.ContainsKey(s[0]))
                        {
                            requiredCards.Add(s[0], value);
                        }
                        else
                        {
                            Debug.LogError($"duplicate key {s[0]}");
                        }
                    }
                    else
                    {
                        Debug.LogError($"wrong key {s[0]} or value {s[1]} in {card}");
                    }
                }
                else
                {
                    Debug.LogError($"{card} don't contains :");
                }
            }

            return requiredCards;
        }

        public static string ConvertDictionaryStringFloatToString(
            Dictionary<string, float> records,
            char separator = ',',
            char valueSeparator = ':')
        {
            var stringBuilder = new StringBuilder();
            foreach ((string key, float value) in records)
            {
                stringBuilder.Append($"{key}{valueSeparator}{value}");
                stringBuilder.Append(separator);
            }

            string result = stringBuilder.ToString();
            result = result.Remove(result.Length - 1);
            return result;
        }

        public static List<string> StringToStringList(
            string str,
            char separator = ',',
            StringSplitOptions splitOptions = StringSplitOptions.None)
        {
            str = str.Replace("\n", String.Empty).Replace("\r", String.Empty);
            str = str.Replace(separator + " ", separator.ToString());
            str = str.Replace(" " + separator, separator.ToString());
            if (str == "") return new List<string>();
            List<string> elements = new List<string>(str.Split(separator, splitOptions));
            return elements;
        }
    }
}