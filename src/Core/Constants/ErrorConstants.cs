namespace CurrencyExchange.Core.Constants
{
    public class ErrorConstants
    {
        /// <summary>
        /// Invalid date format.
        /// </summary>
        public const string DateFormatIssues = "Invalid date-time format!";

        /// <summary>
        /// No currency rates/info is available for processing.
        /// </summary>
        public const string NoCurrencyAvailable = "No currency data is available...";

        /// <summary>
        /// Connection to server is not established.
        /// </summary>
        public const string ConnectionIssues = "Unable to connect to server! Try later...";

        /// <summary>
        /// Unknown currency.
        /// </summary>
        public const string UnknownCurrency = "Unknown currency abbreviation! Try again!";
    }
}