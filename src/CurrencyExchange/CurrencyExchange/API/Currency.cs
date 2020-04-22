using System;
namespace CurrencyExchange.API
{
    /// <summary>
    /// Provides a base class of nrbr.by API, which contains basic information about major currencies.
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// Internal code.
        /// </summary>
        public string Cur_ID { get; set; }

        /// <summary>
        /// This code is used for communication, when changing the name, the number of units to which the Belarusian ruble, alphanumeric, digital codes, etc. are set. virtually the same currency *.
        /// </summary>
        public string Cur_ParentID { get; set; }

        /// <summary>
        /// Digital code.
        /// </summary>
        public string Cur_Code { get; set; }

        /// <summary>
        /// Letter code.
        /// </summary>
        public string Cur_Abbreviation { get; set; }

        /// <summary>
        /// Currency name in Russian.
        /// </summary>
        public string Cur_Name { get; set; }

        /// <summary>
        /// Currency name in Belarussian.
        /// </summary>
        public string Cur_Name_Bel { get; set; }

        /// <summary>
        /// Currency name in English.
        /// </summary>
        public string Cur_Name_Eng { get; set; }

        /// <summary>
        /// Currency name in Russian containing the number of units.
        /// </summary>
        public string Cur_QuotName { get; set; }

        /// <summary>
        /// Currency name in Belarussian containing the number of units.
        /// </summary>
        public string Cur_QuotName_Bel { get; set; }

        /// <summary>
        /// Currency name in English containing the number of units.
        /// </summary>
        public string Cur_QuotName_Eng { get; set; }

        /// <summary>
        /// Currency name in Russian in the plural.
        /// </summary>
        public string Cur_NameMulti { get; set; }

        /// <summary>
        /// Currency name in Belarussian in the plural.
        /// </summary>
        public string Cur_Name_BelMulti { get; set; }

        /// <summary>
        /// Currency name in English in the plural.
        /// </summary>
        public string Cur_Name_EngMulti { get; set; }

        /// <summary>
        /// Number of units of foreign currency.
        /// </summary>
        public string Cur_Scale { get; set; }

        /// <summary>
        /// The frequency of establishing the course (0 - daily, 1 - monthly).
        /// </summary>
        public string Cur_Periodicity { get; set; }

        /// <summary>
        /// The date the currency is included in the list of currencies to which the official rate of the Belarusian ruble is set.
        /// </summary>
        public string Cur_DateStart { get; set; }

        /// <summary>
        /// The date the currency is excluded from the list of currencies to which the official exchange rate of the Belarusian ruble is established.
        /// </summary>
        public string Cur_DateEnd { get; set; }
    }
}