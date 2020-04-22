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
        /// Get request for the information about the whole currencies at nbrb.by.
        /// </summary>
        /// <returns></returns>
        public string GetCurrencyInfoRequest() => string.Concat(_root, "currencies/");

        /// <summary>
        /// Get request for the information about the currency with a specific index (at nbrb.by).
        /// </summary>
        /// <param name="currencyIndex">Currency index at nbrb.by.</param>
        /// <returns></returns>
        public string GetCurrencyInfoRequest(int currencyIndex)
        {
            if (currencyIndex < 0)
                throw new ArgumentOutOfRangeException();

            return string.Concat(_root, "currencies/", currencyIndex.ToString());
        }

        /// <summary>
        /// Get request for the rates of the whole currencies at nbrb.by.
        /// </summary>
        /// <returns></returns>
        public string GetCurrencyRateRequest() => string.Concat(_root, "rates?periodicity=0");

        /// <summary>
        /// Get request for the rate of the currency with a specific index (at nbrb.by).
        /// </summary>
        /// <param name="currencyIndex">Index of currency at nbrb.by.</param>
        /// <returns></returns>
        public string GetCurrencyRateRequest(int currencyIndex)
        {
            if (currencyIndex < 0)
                throw new ArgumentOutOfRangeException();

            return string.Concat(_root, "rates/", currencyIndex.ToString(), "?periodicity=0");
        }
    }
}