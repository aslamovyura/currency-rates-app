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
        /// Build request for the information about the whole currencies at nbrb.by.
        /// </summary>
        /// <returns></returns>
        public string BuildCurrencyInfoRequest() => string.Concat(_root, "currencies/");

        /// <summary>
        /// Build request for the information about the currency with a specific Identifier (accordint to nbrb.by).
        /// </summary>
        /// <param name="curId">Currency ID according to nbrb.by.</param>
        /// <returns>URL request.</returns>
        public string BuildCurrencyInfoRequest(int curId)
        {
            if (curId < 0)
                throw new ArgumentOutOfRangeException();

            return string.Concat(_root, "currencies/", curId.ToString());
        }

        /// <summary>
        /// Build request for the rates of the whole currencies at nbrb.by.
        /// </summary>
        /// <returns></returns>
        public string BuildCurrencyRateRequest() => string.Concat(_root, "rates?periodicity=0");

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

            return string.Concat(_root, "rates/", curDigitalCode.ToString(), "?parammode=1");
        }

        /// <summary>
        /// Build request for the rate of the currency with a specific abbreviation according to ISO-4217 (USD, EUR, RUB).
        /// </summary>
        /// <param name="curAbbreviation">Currency ISO-4217 abbreviation (USD, EUR, RUB).</param>
        /// <returns>URL request.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public string BuildCurrencyRateRequest(string curAbbreviation)
        {
            curAbbreviation = curAbbreviation ?? throw new ArgumentNullException();

            return string.Concat(_root, "rates/", curAbbreviation.ToUpper(), "?parammode=2");
        }

        /// <summary>
        /// Build request for the rate of the currency with a specific abbreviation according to ISO-4217 (USD, EUR, RUB).
        /// </summary>
        /// <param name="curId">Currency ISO-4217 abbreviation (USD, EUR, RUB).</param>
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
                dateString = date.ToString("yyyy-MM-dd");
            }
            catch
            {
                Console.WriteLine("Invalid date-time format!");
                return null;
            }
            
            return string.Concat(_root, "rates/", curId, "?ondate=", dateString);
        }
    }
}