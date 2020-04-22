namespace CurrencyExchange.API
{
    /// <summary>
    /// Provides a base class of nrbr.by API, which contains basic info about currency exchange rate.
    /// </summary>
    public class Rate
    {
        /// <summary>
        /// Internal code.
        /// </summary>
        public string Cur_ID { get; set; }

        /// <summary>
        /// Date on which the currency is requested.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Letter code.
        /// </summary>
        public string Cur_Abbreviation { get; set; }

        /// <summary>
        /// Number of units of foreign currency
        /// </summary>
        public string Cur_Scale { get; set; }

        /// <summary>
        /// Currency name in Russian in the plural or singular, depending on the number of units
        /// </summary>
        public string Cur_Name { get; set; }

        /// <summary>
        /// Official currency rate.
        /// </summary>
        public string Cur_OfficialRate { get; set; }
    }
}