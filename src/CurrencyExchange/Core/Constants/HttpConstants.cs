namespace CurrencyExchange.Core.Constants
{
    public static class HttpConstants
    {
        /// <summary>
        /// Root nbrb URL.
        /// </summary>
        public const string Root = "https://www.nbrb.by/api/exrates/";

        /// <summary>
        /// Rates action.
        /// </summary>
        public const string Rates = "rates/";

        /// <summary>
        /// Currencies action.
        /// </summary>
        public const string Currencies = "currencies/";

        /// <summary>
        /// Request for today currency rates.
        /// </summary>
        public const string RatesToday = "rates?periodicity=0";

        /// <summary>
        /// Request for currency rate by code.
        /// </summary>
        public const string RateByCode = "?parammode=1";

        /// <summary>
        /// Request for currency rate by abbreviation.
        /// </summary>
        public const string RateByAbbr = "?parammode=2";

        /// <summary>
        /// Request currency rate for specific date.
        /// </summary>
        public const string OnDate = "?ondate=";

        /// <summary>
        /// Available date format.
        /// </summary>
        public const string DateFormat = "yyyy-MM-dd";
    }
}