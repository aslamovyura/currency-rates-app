namespace CurrencyExchange.Core.Constants
{
    public static class HttpConstants
    {
        /// <summary>
        /// Root nbrb URL.
        /// </summary>
        public const string Root = "https://www.nbrb.by/api/exrates/";

        /// <summary>
        /// Rates.
        /// </summary>
        public const string Rates = "rates/";

        /// <summary>
        /// Currencies.
        /// </summary>
        public const string Currencies = "currencies/";

        /// <summary>
        /// Currency rates for today.
        /// </summary>
        public const string RatesToday = "rates?periodicity=0";

        /// <summary>
        /// Get currency rate by code.
        /// </summary>
        public const string RateByCode = "?parammode=1";

        /// <summary>
        /// Get currency rate by abbreviation.
        /// </summary>
        public const string RateByAbbr = "?parammode=2";

        /// <summary>
        /// Get currency rate for specific date.
        /// </summary>
        public const string OnDate = "?ondate=";

        /// <summary>
        /// Date format.
        /// </summary>
        public const string DateFormat = "yyyy-MM-dd";
    }
}