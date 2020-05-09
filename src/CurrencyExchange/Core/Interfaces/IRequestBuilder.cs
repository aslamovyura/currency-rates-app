using System;
namespace CurrencyExchange.Core.Interfaces
{
    /// <summary>
    /// Define interface to build http requests for the currency rates.
    /// </summary>
    public interface IRequestBuilder
    {
        /// <summary>
        /// Build request for the information about the whole currencies at nbrb.by.
        /// </summary>
        /// <returns>URL request.</returns>
        public string BuildCurrencyInfoRequest();

        /// <summary>
        /// Build request for the information about the currency with a specific Identifier (accordint to nbrb.by).
        /// </summary>
        /// <param name="curId">Currency ID according to nbrb.by.</param>
        /// <returns>URL request.</returns>
        public string BuildCurrencyInfoRequest(int curId);

        /// <summary>
        /// Build request for the rates of the whole currencies at nbrb.by.
        /// </summary>
        /// <returns>URL request.</returns>
        public string BuildCurrencyRateRequest();

        /// <summary>
        /// Build request for the rate of the currency with a specific digital code (according to ISO-4217).
        /// </summary>
        /// <param name="curDigitalCode">Digital code of currency according to ISO-4217.</param>
        /// <returns>URL request.</returns>
        public string BuildCurrencyRateRequest(int curDigitalCode);

        /// <summary>
        /// Build request for the currency rate based on the currency abbreviation according to ISO-4217 (USD, EUR, RUB).
        /// </summary>
        /// <param name="curAbbreviation">Currency ISO-4217 abbreviation (USD, EUR, RUB).</param>
        /// <returns>URL request.</returns>
        public string BuildCurrencyRateRequest(string curAbbreviation);

        /// <summary>
        /// Build request for the currency rate based on currency identifier.
        /// </summary>
        /// <param name="curId">Currency identifier.</param>
        /// <returns>URL request.</returns>
        public string BuildCurrencyRateRequest(int curId, DateTime date);

    }
}