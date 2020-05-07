using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Interfaces;
using CurrencyExchange.Core.Constants;
using CurrencyExchange.Core.Interfaces;
using CurrencyExchange.Core.Models;
using Newtonsoft.Json;

namespace CurrencyExchange.Core.Controllers
{
    /// <summary>
    /// Provides a base class to deal with currency rates.
    /// </summary>
    public class CurrencyController : IDisposable
    {
        #region Fields & Properties
        // Disposing status.
        private bool _disposed = false;

        // HTTP client to send requests to nbrb.by.
        private readonly HttpClient _client = new HttpClient();

        // HTTP requests builder for the nbrb.by.
        private readonly IRequestBuilder _requestBuilder;

        private readonly IWriter _fileWriter;

        // Cache information on the world-wide currencies.
        private ICollection<Currency> _currencyInfoCache;

        // Cache information on the currency rates (for today).
        private ICollection<Rate> _currencyRatesCache;

        /// <summary>
        /// Enable/disable saving information to *.txt file.
        /// </summary>
        public bool SaveToFileEnable { get; set; } = false;

        /// <summary>
        /// Enable/disable to save result to file asynchronously.
        /// </summary>
        public bool SaveToFileAsync { get; set; } = true;

        #endregion

        /// <summary>
        /// Define currencyHandler object to check currency rates.
        /// </summary>
        public CurrencyController()
        {
            _requestBuilder = new HttpRequestBuilder();
            _fileWriter = new TxtFileWriter();
            _currencyInfoCache = new List<Currency>();
            _currencyRatesCache = new List<Rate>();
        }

        #region Requests & Responses
        // Send request to the server asynchronously.
        private async Task<string> SendRequestAsync(string request)
        {
            if (request == null)
                throw new ArgumentNullException();

            string responseBody = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync(request);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch
            {
                Console.WriteLine("\n" + ErrorConstants.ConnectionIssues);
            }

            return responseBody;
        }

        // Request the whole list of currencies.
        private void RequestCurrencyInfo()
        {
            var request = _requestBuilder.BuildCurrencyInfoRequest();
            var responseString = SendRequestAsync(request);

            if (responseString == null)
                return;

            // Try to parse currency rates.
            try
            {
                _currencyInfoCache = JsonConvert.DeserializeObject<List<Currency>>(responseString.Result);
            }
            catch
            {
                return;
            }
        }

        // Request the whole currency list asynchronously.
        private async Task RequestCurrencyInfoAsync() => await Task.Run(() => RequestCurrencyInfo());

        // Send request to the server for information on the currency exchange rates.
        private void RequestCurrencyRates()
        {
            var request = _requestBuilder.BuildCurrencyRateRequest();
            var responseString = SendRequestAsync(request);

            if (responseString == null)
                return;

            // Try to parse currency info.
            try
            {
                _currencyRatesCache = JsonConvert.DeserializeObject<List<Rate>>(responseString.Result);
            }
            catch
            {
                return;
            }

            TranslateCurrencyRatesToEnglish();
        }

        // Send request to the server for information on the currency exchange rates asynchronously.
        private async Task RequestCurrencyRatesAsync() => await Task.Run(() => RequestCurrencyRates());

        // Check, if there is some currency rate cache, if not, send request to the server.
        public bool CheckCurrencyRates()
        {
            // Try to connect to server to get the required data.
            if (_currencyRatesCache.Count == 0)
                RequestCurrencyRatesAsync().GetAwaiter().GetResult();
            {
                // If there is still no data, return.
                if (_currencyRatesCache.Count == 0)
                {
                    Console.WriteLine(ErrorConstants.NoCurrencyAvailable);
                    return false;
                }
            }
            return true;
        }

        // Check, if there is some currency rate cache, if not, send request to the server asynchronously.
        private async Task<bool> CheckCurrencyRatesAsync() => await Task.Run(() => CheckCurrencyRates());

        // Check, if there is some currency info cache, if not, send request to the server.
        private bool CheckCurrencyInfo()
        {
            // Try to connect to server to get the required data.
            if (_currencyInfoCache.Count == 0)
                RequestCurrencyInfoAsync().GetAwaiter().GetResult();
            {
                // If there is still no data, return.
                if (_currencyInfoCache.Count == 0)
                {
                    Console.WriteLine(ErrorConstants.NoCurrencyAvailable);
                    return false;
                }
            }
            return true;
        }

        // Check, if there is some currency info cache, if not, send request to the server asynchronously.
        private async Task<bool> CheckCurrencyInfoAsync() => await Task.Run(() => CheckCurrencyInfo());

        #endregion

        #region Show & Print methods

        /// <summary>
        /// Show the list of available currencies.
        /// </summary>
        public void ShowWorldCurrenciesList()
        {
            // Try to connect to server to get the required data.
            if (_currencyInfoCache.Count == 0)
                RequestCurrencyInfoAsync().GetAwaiter().GetResult();
            {
                // If there is still no data, return.
                if (_currencyInfoCache.Count == 0)
                {
                    Console.WriteLine(ErrorConstants.NoCurrencyAvailable);
                    return;
                }
            }

            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine($"| Nbrn ID \t| Abbreviation\t| {"Currency Name",25} |");
            Console.WriteLine("-------------------------------------------------------------");
            foreach (var data in _currencyInfoCache)
                Console.WriteLine($"| ID={data.Cur_ID,5:N0}\t| {data.Cur_Abbreviation,10}\t| {data.Cur_Name_Eng, 25} |");
            Console.WriteLine("-------------------------------------------------------------");
        }

        /// <summary>
        /// Print co console world-wide currencies asynchronously.
        /// </summary>
        public async Task ShowWorldCurrenciesListAsync() => await Task.Run(() => ShowWorldCurrenciesList());

        /// <summary>
        /// Print to console all available currency types.
        /// </summary>
        public void ShowAvailableCurrencies()
        {
            var success = CheckCurrencyRates();
            if (!success)
                return;

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine($"| Code \t| {"Currency Name",35} |");
            Console.WriteLine("-----------------------------------------------");
            foreach (var data in _currencyRatesCache)
                Console.WriteLine($"| {data.Cur_Abbreviation}\t| {data.Cur_Name,35} |");
            Console.WriteLine("-----------------------------------------------");
        }

        /// <summary>
        /// Print to Console the full list of currency rates.
        /// </summary>
        public void ShowAllCurrencyRates()
        {
            var success = CheckCurrencyRates();
            if (!success)
                return;

            Print(_currencyRatesCache);
        }

        /// <summary>
        /// Print to Console the full list of currency rates asynchronously.
        /// </summary>
        public async Task ShowAllCurrencyRatesAsync() => await Task.Run(() => ShowAllCurrencyRates());

        /// <summary>
        /// Show current exchange rate of the currency with a sertain abbreviation (e.g. USD, EUR).
        /// </summary>
        /// <param name="abbreviation">Currency abbreviation, e.g. USD, EUR.</param>
        /// <exception cref="ArgumentNullException"></exception>"
        public void ShowCurrencyRate(string abbreviation)
        {
            abbreviation = abbreviation ?? throw new ArgumentNullException();
            abbreviation = abbreviation.ToUpper();

            var success = CheckCurrencyRates();
            if (!success)
                return;

            var currencyRate = _currencyRatesCache.FirstOrDefault(a => a.Cur_Abbreviation == abbreviation);
            if (currencyRate == null)
                Console.WriteLine(ErrorConstants.UnknownCurrency);
            else
                Print(new List<Rate>{ currencyRate });
        }

        /// <summary>
        /// Show current exchange rate of the currency with a sertain abbreviation (e.g. USD, EUR). asynchronously.
        /// </summary>
        public async Task ShowCurrencyRateAsync(string abbreviation) => await Task.Run(() => ShowCurrencyRate(abbreviation));

        // Translate currency names from Russian to English
        private void TranslateCurrencyRatesToEnglish()
        {
            if (!CheckCurrencyRates())
                return;

            if (!CheckCurrencyInfo())
                return;

            // Translate currency names from Russian to English.
            foreach (var rate in _currencyRatesCache)
                foreach (var info in _currencyInfoCache)
                {
                    if (rate.Cur_ID == info.Cur_ID)
                        rate.Cur_Name = info.Cur_Name_Eng;
                }
        }

        /// <summary>
        /// Print information on the currency rates to the console & *.txt file.
        /// </summary>
        /// <param name="currencyRates"Currency rate.></param>
        /// <exception cref="ArgumentNullException"></exception>
        private void Print(ICollection<Rate> currencyRates)
        {
            if (currencyRates.Count == 0)
                throw new ArgumentNullException();

            // Create content on currency rate.
            string line = new string('-', 72) + "\n";

            string content = $"\n Date : [{DateTime.Today,0:dd/MM/yyyy}]\n" + line +
                            $"|{"Currency Name",35}\t| Ammout x Code | Official rate\t|\n" + line;

            foreach (var r in currencyRates)
                content += $"| {r.Cur_Name,35}\t| {r.Cur_Scale} x {r.Cur_Abbreviation}\t| {r.Cur_OfficialRate}\t|\n";
            content += line;

            // Print to console & txt file.
            Console.WriteLine(content);
            if (SaveToFileEnable)
            {
                if (SaveToFileAsync)
                {
                    Task.Run(() => _fileWriter.WriteAsync(content));
                }
                else
                {
                    _fileWriter.WriteAsync(content);
                }
            }    
        }
        #endregion

        #region IDisposable implementation
        /// <summary>
        /// Method for object disposing.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Method for object disposing.
        protected virtual void Dispose(bool disposing)
        {
            if(!_disposed)
            {
                if(disposing)
                {
                    if (_client != null)
                        _client.Dispose();
                }
                _disposed = true;
            }
        }

        // Destructor method.
        ~CurrencyController()
        {
            Dispose(false);
        }
        #endregion
    }
}