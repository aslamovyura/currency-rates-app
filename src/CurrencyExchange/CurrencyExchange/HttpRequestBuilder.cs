using System;
namespace CurrencyExchange
{
    /// <summary>
    /// Provides a base class to build requests to the nrbr.by.
    /// </summary>
    public class HttpRequestBuilder
    {
        // Root URL.
        private readonly string _root = "https://www.nbrb.by/api/exrates/";

        /// <summary>
        /// Default constructor.
        /// </summary>
        public HttpRequestBuilder() { }

        /// <summary>
        /// Initialize httpRequestBuilder with root URL.
        /// </summary>
        public HttpRequestBuilder(string rootUrl)
        {
            _root = rootUrl ?? throw new ArgumentNullException();
        }

        /// <summary>
        /// Get request for the information about the whole currencies at nbrb.by.
        /// </summary>
        /// <returns></returns>
        public string GetCurrencyInfoRequest() => string.Concat(_root, "currencies/");

        /// <summary>
        /// Get request for the information about the currency with a specific Identifier (accordint to nbrb.by).
        /// </summary>
        /// <param name="currencyId">Currency ID according to nbrb.by.</param>
        /// <returns>URL request.</returns>
        public string GetCurrencyInfoRequest(int currencyId)
        {
            if (currencyId < 0)
                throw new ArgumentOutOfRangeException();

            return string.Concat(_root, "currencies/", currencyId.ToString());
        }

        /// <summary>
        /// Get request for the rates of the whole currencies at nbrb.by.
        /// </summary>
        /// <returns></returns>
        public string GetCurrencyRateRequest() => string.Concat(_root, "rates?periodicity=0");

        /// <summary>
        /// Get request for the rate of the currency with a specific digital code (according to ISO-4217).
        /// </summary>
        /// <param name="digitalCode">Digital code of currency according to ISO-4217.</param>
        /// <returns>URL request.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public string GetCurrencyRateRequest(int digitalCode)
        {
            if (digitalCode < 0)
                throw new ArgumentOutOfRangeException();

            return string.Concat(_root, "rates/", digitalCode.ToString(), "?parammode=1");
        }

        /// <summary>
        /// Get request for the rate of the currency with a specific abbreviation according to ISO-4217 (USD, EUR, RUB).
        /// </summary>
        /// <param name="curAbbreviation">Currency ISO-4217 abbreviation (USD, EUR, RUB).</param>
        /// <returns>URL request.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public string GetCurrencyRateRequest(string curAbbreviation)
        {
            curAbbreviation = curAbbreviation ?? throw new ArgumentNullException();

            return string.Concat(_root, "rates/", curAbbreviation.ToUpper(), "?parammode=2");
        }

        /// <summary>
        /// Get request for the rate of the currency with a specific abbreviation according to ISO-4217 (USD, EUR, RUB).
        /// </summary>
        /// <param name="currencyId">Currency ISO-4217 abbreviation (USD, EUR, RUB).</param>
        /// <returns>URL request.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public string GetCurrencyRateRequest(int currencyId, DateTime date)
        {
            if (currencyId < 0)
                throw new ArgumentOutOfRangeException();

            if (date == default)
                throw new ArgumentNullException();

            string dateString;
            try
            {
                dateString = date.ToString("yyyy-MM-dd");
            }
            catch
            {
                Console.WriteLine("Invalid date-time format!");
                return null;
            }
            
            return string.Concat(_root, "rates/", currencyId, "?ondate=", dateString);
        }
    }
}