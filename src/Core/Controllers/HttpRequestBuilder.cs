using System;
using CurrencyExchange.Core.Interfaces;
using CurrencyExchange.Core.Constants;

namespace CurrencyExchange.Core.Controllers
{
    /// <summary>
    /// Provides a base class to build requests to the nrbr.by.
    /// </summary>
    public class HttpRequestBuilder : IRequestBuilder
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public HttpRequestBuilder() { }

        /// <summary>
        /// Build request for the information about the whole currencies at nbrb.by.
        /// </summary>
        /// <returns>URL request.</returns>
        public string BuildCurrencyInfoRequest() => string.Concat(HttpConstants.Root, HttpConstants.Currencies);

        /// <summary>
        /// Build request for the information about the currency with a specific Identifier (accordint to nbrb.by).
        /// </summary>
        /// <param name="curId">Currency ID according to nbrb.by.</param>
        /// <returns>URL request.</returns>
        public string BuildCurrencyInfoRequest(int curId)
        {
            if (curId < 0)
                throw new ArgumentOutOfRangeException();

            return string.Concat(HttpConstants.Root, HttpConstants.Currencies, curId.ToString());
        }

        /// <summary>
        /// Build request for the rates of the whole currencies at nbrb.by.
        /// </summary>
        /// <returns>URL request.</returns>
        public string BuildCurrencyRateRequest() => string.Concat(HttpConstants.Root, HttpConstants.RatesToday);

        /// <summary>
        /// Build request for the rate of the currency with a specific digital code (according to ISO-4217).
        /// </summary>
        /// <param name="curDigitalCode">Digital code of currency according to ISO-4217.</param>
        /// <returns>URL request.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public string BuildCurrencyRateRequest(int curDigitalCode)
        {
            if (curDigitalCode < 0)
                throw new ArgumentOutOfRangeException();

            return string.Concat(HttpConstants.Root, HttpConstants.Rates, curDigitalCode.ToString(), HttpConstants.RateByCode);
        }

        /// <summary>
        /// Build request for the currency rate based on the currency abbreviation according to ISO-4217 (USD, EUR, RUB).
        /// </summary>
        /// <param name="curAbbreviation">Currency ISO-4217 abbreviation (USD, EUR, RUB).</param>
        /// <returns>URL request.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public string BuildCurrencyRateRequest(string curAbbreviation)
        {
            curAbbreviation = curAbbreviation ?? throw new ArgumentNullException();

            return string.Concat(HttpConstants.Root, HttpConstants.Rates, curAbbreviation.ToUpper(), HttpConstants.RateByAbbr);
        }

        /// <summary>
        /// Build request for the currency rate based on currency identifier.
        /// </summary>
        /// <param name="curId">Currency identifier.</param>
        /// <returns>URL request.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public string BuildCurrencyRateRequest(int curId, DateTime date)
        {
            if (curId < 0)
                throw new ArgumentOutOfRangeException();

            if (date == default)
                throw new ArgumentNullException();

            string dateString;
            try
            {
                dateString = date.ToString(HttpConstants.DateFormat);
            }
            catch
            {
                Console.WriteLine(ErrorConstants.DateFormatIssues);
                return null;
            }
            
            return string.Concat(HttpConstants.Root, HttpConstants.Rates, curId, HttpConstants.OnDate, dateString);
        }
    }
}