using AlphaVantageConnector.Enums;
using AlphaVantageDto.Enums;
using System;
using System.Collections.Generic;

namespace AlphaVantageConnector.Dictionaries
{
    /// <summary>
    /// Description of currencies by enum Key.
    /// </summary>
    public static class CurrencyDesc
    {
        private static Dictionary<Currency, string> _descriptions { get; } = new Dictionary<Currency, string>();

        static CurrencyDesc()
        {
            var file = Properties.Resources.CurrencyDesc;

            var rows = file.Split(Environment.NewLine);
            foreach (var row in rows)
            {
                if (string.IsNullOrEmpty(row)) continue;

                var pair = row.Split(',');
                var name = pair[0];
                var desc = pair[1];

                Currency key = (Currency)Enum.Parse(typeof(Currency), name);

                _descriptions.Add(key, desc);
            }
        }

        public static string GetSescripton(Currency currency)
        {
            _descriptions.TryGetValue(currency, out var desc);

            return desc;
        }
    }
}
