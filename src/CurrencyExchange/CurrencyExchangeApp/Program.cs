using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CurrencyExchange;

namespace CurrencyExchangeApp
{
    class Program
    {
        // Object containing info on the currency exchange rates.
        static CurrencyHandler currencyHandler = new CurrencyHandler();

        static void Main(string[] args)
        {
            // Check currency rates for today.
            Task.Run(() => currencyHandler.CheckCurrencyRates()).ContinueWith((t) => Console.Write("\nUser input: ")); ;

            GreetingMessage();
            Console.Write("\nUser input: ");
            while (true)
            {
                var input = Console.ReadLine().Trim().ToUpper();
                switch (input)
                {
                    case "A":
                    case "ALL":
                        Console.Write("\n Curriency rates are loading ... Please, wait.\n");
                        currencyHandler.ShowAllCurrencyRatesAsync().
                            ContinueWith((t) => Console.Write("\nUser input: ")); ;
                        break;

                    case "I":
                    case "INFO":
                        Console.Write("\n Curriency rates are loading ... Please, wait.\n");
                        Task t1 = currencyHandler.ShowWorldCurrenciesListAsync()
                            .ContinueWith((t) => Console.Write("\nUser input: "));
                        break;
                            
                    case "H":
                    case "HELP":
                        Console.WriteLine();
                        GreetingMessage();
                        HelpMessage();
                        Console.Write("\nUser input: ");
                        break;

                    case "S":
                    case "SAVE":
                        SaveToFileEnable();
                        break;

                    case "Q":
                    case "QUIT":
                        Console.WriteLine("\nThank's for using this app! See you soon!");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.Write("\n Curriency rates are loading ... Please, wait.\n ");
                        currencyHandler.ShowCurrencyRateAsync(input)
                            .ContinueWith((t) => Console.Write("\nUser input: "));
                        break;
                }
            }
        }

        // Print greeting message
        static void GreetingMessage()
        {
            Console.WriteLine("\n**********************************************");
            Console.WriteLine("************* Currency Exchange **************");
            Console.WriteLine("**********************************************\n");

            Console.WriteLine("`Currency Exchange` application provides you with information about the currencies exchange rates (based on http://www.nbrb.by)");
            Console.WriteLine("P.S.: Application has a delay (3 sec) in requests & saving data to file.");
            Console.WriteLine("P.P.S: All processing data will be saved to the *.txt file .../TMS-DotNet-Team1-YAP/src/CurrencyExchange/CurrencyExchangeApp/bin/Debug/netcoreapp3.1/Temp/temp.txt");
            Console.WriteLine("\nInput currency abbreviation (e.g. USD, EUR, RUB etc) or the following key:");
            Console.WriteLine("\t`a` --- show all currency exchange rates");
            Console.WriteLine("\t`i` --- info on the whole worl-wide currencies");
            Console.WriteLine("\t`s` --- enable/disable saving to *.txt file (disable by default)");
            Console.WriteLine("\t`h` --- show available currencies");
            Console.WriteLine("\t`q` --- quit program");
        }

        // Print help message
        static void HelpMessage()
        {
            Console.WriteLine("\nThe following currency abbreviations is available:");
            currencyHandler.ShowAvailableCurrencies();
        }

        static void SaveToFileEnable()
        {
            string status;
            if (currencyHandler.SaveToFileEnable)
            {
                currencyHandler.SaveToFileEnable = false;
                status = "disable";
            }     
            else
            {
                currencyHandler.SaveToFileEnable = true;
                status = "enable";
            }
            Console.WriteLine($"\nSaving to *.txt file is {status.ToUpper()} now.\n");
        }
    }
}