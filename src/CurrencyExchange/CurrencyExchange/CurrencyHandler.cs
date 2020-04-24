using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CurrencyExchange.API;
using Newtonsoft.Json;

namespace CurrencyExchange
{
    /// <summary>
    /// Provides a base class to deal with currency rates.
    /// </summary>
    public class CurrencyHandler
    {
        // HTTP client to send requests to nbrb.by.
        private readonly HttpClient _client = new HttpClient();

        // HTTP requests builder for the nbrb.by.
        private readonly HttpRequestBuilder _requestBuilder = new HttpRequestBuilder();

        // Full information on the world-wide currencies.
        private List<Currency> _currencyInfoFull = new List<Currency>();

        // Information on the currencu rates.
        private List<Rate> _currencyRates = new List<Rate>();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CurrencyHandler(){ }

        // Get the whole currency list.
        private void RequestFullCurrencyInfo()
        {
            var request = _requestBuilder.GetCurrencyInfoRequest();
            var responseString = SendRequestAsync(request);

            if (responseString == null)
                return;

            // Try to parse currency rates.
            try
            {
                _currencyInfoFull = JsonConvert.DeserializeObject<List<Currency>>(responseString.Result);
            }
            catch
            {
                return;
            }
        }

        // Get the whole currency list asynchronously.
        private async Task RequestFullCurrencyInfoAsync() => await Task.Run(() => RequestFullCurrencyInfo());

        // Send request to the server for information on the currency exchange rates.
        private void RequestCurrencyRates()
        {
            var request = _requestBuilder.GetCurrencyRateRequest();
            var responseString = SendRequestAsync(request);

            if (responseString == null)
                return;

            // Try to parse currency info.
            try
            {
                _currencyRates = JsonConvert.DeserializeObject<List<Rate>>(responseString.Result);
            }
            catch
            {
                return;
            }

            TranslateCurrencyRatesToEnglish();
        }

        // Send request to the server for information on the currency exchange rates asynchronously.
        private async Task RequestCurrencyRatesAsync() => await Task.Run(() => RequestCurrencyRates());

        /// <summary>
        /// Show the list of available currencies.
        /// </summary>
        public void ShowWorldCurrenciesList()
        {
            // Try to connect to server to get the required data.
            if (_currencyInfoFull.Count == 0)
                RequestFullCurrencyInfoAsync().GetAwaiter().GetResult();
            {
                // If there is still no data, return.
                if (_currencyInfoFull.Count == 0)
                {
                    Console.WriteLine("No currency data is available...");
                    return;
                }
            }

            Console.WriteLine("\n\t\t\tWorld Currencies");
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine($"| Nbrn ID \t| Abbreviation\t| {"Currency Name",25} |");
            Console.WriteLine("-------------------------------------------------------------");
            foreach (var data in _currencyInfoFull)
                Console.WriteLine($"| ID={data.Cur_ID,5:N0}\t| {data.Cur_Abbreviation,10}\t| {data.Cur_Name_Eng, 25} |");
            Console.WriteLine("-------------------------------------------------------------\n");
        }

        /// <summary>
        /// Print co console world-wide currencies asynchronously.
        /// </summary>
        public async Task ShowWorldCurrenciesListAsync() => await Task.Run(() => ShowWorldCurrenciesList());

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
                Thread.Sleep(3000); // Debug for async
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch
            {
                Console.WriteLine("\nUnable to connect to server! Try later...");
            }

            return responseBody;
        }

        /// <summary>
        /// Print to console all available currency types.
        /// </summary>
        public void ShowAvailableCurrencies()
        {
            var success = CheckCurrencyRates();
            if (!success)
                return;

            Console.WriteLine("\n               Available Currencies");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine($"| Code \t| {"Currency Name",35} |");
            Console.WriteLine("-----------------------------------------------");
            foreach (var data in _currencyRates)
                Console.WriteLine($"| {data.Cur_Abbreviation}\t| {data.Cur_Name,35} |");
            Console.WriteLine("-----------------------------------------------\n");
        }

        /// <summary>
        /// Print to Console the full list of currency rates.
        /// </summary>
        public void ShowAllCurrencyRates()
        {
            var success = CheckCurrencyRates();
            if (!success)
                return;

            Print(_currencyRates);
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
            abbreviation = abbreviation.ToUpper() ?? throw new ArgumentNullException();

            var success = CheckCurrencyRates();
            if (!success)
                return;

            var currencyRate = _currencyRates.FirstOrDefault(a => a.Cur_Abbreviation == abbreviation);
            if (currencyRate == null)
                Console.WriteLine($"Unknown currency abbreviation {abbreviation}! Try again!");
            else   
                Print(currencyRate);
        }

        /// <summary>
        /// Show current exchange rate of the currency with a sertain abbreviation (e.g. USD, EUR). asynchronously.
        /// </summary>
        public async Task ShowCurrencyRateAsync(string abbreviation) => await Task.Run(() => ShowCurrencyRate(abbreviation));

        // Translate currency names from Russian to English
        private void TranslateCurrencyRatesToEnglish()
        {
            if (_currencyRates.Count == 0)
                if (!CheckCurrencyRates())
                    return;

            if (_currencyInfoFull.Count == 0)
                if (!CheckCurrencyInfo())
                    return;

            // Translate currency names from Russian to English.
            foreach (var r in _currencyRates)
            {
                foreach (var l in _currencyInfoFull)
                {
                    if (r.Cur_ID == l.Cur_ID)
                        r.Cur_Name = l.Cur_Name_Eng;
                }
            }
        }

        // Check, if there is some currency rate in the class for now, if not, send request to the server.
        private bool CheckCurrencyRates()
        {
            // Try to connect to server to get the required data.
            if (_currencyRates.Count == 0)
                RequestCurrencyRatesAsync().GetAwaiter().GetResult();
            {
                // If there is still no data, return.
                if (_currencyRates.Count == 0)
                {
                    Console.WriteLine("No currency rates is available...");
                    return false;
                }
            }
            return true;
        }

        // Check, if there is some currency rate in the class for now, if not, send request to the server asynchronously.
        private async Task<bool> CheckCurrencyRatesAsync() => await Task.Run(() => CheckCurrencyRates());

        // Check, if there is some currency info for now, if not, send request to the server.
        private bool CheckCurrencyInfo()
        {
            // Try to connect to server to get the required data.
            if (_currencyInfoFull.Count == 0)
                RequestFullCurrencyInfoAsync().GetAwaiter().GetResult();
            {
                // If there is still no data, return.
                if (_currencyInfoFull.Count == 0)
                {
                    Console.WriteLine("No currency data is available...");
                    return false;
                }
            }
            return true;
        }

        // Check, if there is some currency info for now, if not, send request to the server asynchronously.
        private async Task<bool> CheckCurrencyInfoAsync() => await Task.Run(() => CheckCurrencyInfo());

        /// <summary>
        /// Print information on the currency rates to the console & *.txt file.
        /// </summary>
        /// <param name="rate"Currency rate.></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Print(Rate rate)
        {
            if (rate == null)
                throw new ArgumentNullException();

            List<Rate> rates = new List<Rate>();
            rates.Add(rate);
            Print(rates);
        }

        /// <summary>
        /// Print information on the currency rates to the console & *.txt file.
        /// </summary>
        /// <param name="currencyRates"Currency rate.></param>
        /// <exception cref="ArgumentException"></exception>
        public void Print(List<Rate> currencyRates)
        {
            if (currencyRates.Count == 0)
                throw new ArgumentException();

            // Show Currency Rates.
            Console.WriteLine($"\n Date : [{DateTime.Today,0:dd/MM/yyyy}]");
            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine($"|{"Currency Name",35}\t| Ammout x Code | Official rate\t|");
            Console.WriteLine("------------------------------------------------------------------------");
            foreach (var r in currencyRates)
                Console.WriteLine($"| {r.Cur_Name,35}\t| {r.Cur_Scale} x {r.Cur_Abbreviation}\t| {r.Cur_OfficialRate}\t|");
            Console.WriteLine("------------------------------------------------------------------------\n");

            SaveFile (currencyRates);
        }

        /// <summary>
        /// Save information on the currency rates to the *.txt file.
        /// </summary>
        /// <param name="currencyRates"></param>
        /// <exception cref="ArgumentException"></exception>
        public async void SaveFile (List<Rate> currencyRates)
        {         
            if (currencyRates.Count == 0)
                throw new ArgumentException();

            string fileName = "temp.txt";

            string currentDirectoryPath = Directory.GetCurrentDirectory();
            string currentDirectoryRoot = Directory.GetDirectoryRoot(currentDirectoryPath);

            try
            {
                string currentDirectory = Path.Combine(currentDirectoryRoot, "ApplicationFolder");

                Directory.CreateDirectory(currentDirectory);
                Directory.SetCurrentDirectory(currentDirectory);

                string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);

                using (StreamWriter writer = new StreamWriter(path, true, System.Text.Encoding.Default))
                {
                    await writer.WriteLineAsync($"\n Date : [{DateTime.Today,0:dd/MM/yyyy}]");
                    await writer.WriteLineAsync("------------------------------------------------------------------------");
                    await writer.WriteLineAsync($"|{"Currency Name",35}\t| Ammout x Code | Official rate\t|");
                    await writer.WriteLineAsync("------------------------------------------------------------------------");
                    foreach (var r in currencyRates)
                        await writer.WriteLineAsync($"| {r.Cur_Name,35}\t| {r.Cur_Scale} x {r.Cur_Abbreviation}\t| {r.Cur_OfficialRate}\t|");
                    await writer.WriteLineAsync("------------------------------------------------------------------------\n");
                }
            }

            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine($"The current directory for application does not exist. {e}");
            }
            
            
        }
    }
}