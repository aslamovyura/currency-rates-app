using System;
using System.Linq;
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
            // Processing of command-line arguments
            if (args.Length != 0)
            {
                CmdArgsProcessing(args);
                return;
            }
                
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
                        CurrencyListMessage();
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
                        if (input.Length == 3 && input.All(c => char.IsLetter(c)))
                        {
                            Console.Write("\n Curriency rate is loading ... Please, wait.\n ");
                            currencyHandler.ShowCurrencyRateAsync(input)
                                .ContinueWith((t) => Console.Write("\nUser input: "));
                        }
                        else
                            Console.Write("\n Unknown command or curriency abbreviation... Try again, please.\n ");
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
        static void CurrencyListMessage()
        {
            Console.WriteLine("\nThe following currency abbreviations is available:");
            currencyHandler.ShowAvailableCurrencies();
        }

        // Enable / disable save to file
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

        // Enable / disable save to file
        static void SaveToFileEnable(bool saveToFileEnable)
        {
            string status;
            if (!saveToFileEnable)
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

        // Processing of command-line arguments.
        static void CmdArgsProcessing(string[] args)
        {
            // Check if it is need to save info to *.txt file.
            if (args.Any(a => a == "-s" || a == "--save"))
            {
                SaveToFileEnable(true);
                args = args.Where(a => a != "-s").Where(a => a != "--save").ToArray();
            }

            // Check other commands
            foreach (string arg in args)
            {
                switch (arg)
                {
                    case "-a":
                    case "--all":
                        Console.Write("\n Curriency rates are loading ... Please, wait.\n");
                        currencyHandler.ShowAllCurrencyRatesAsync().GetAwaiter().GetResult();
                        break;

                    case "-c":
                    case "--currency":
                        Console.Write("\n Curriency rates are loading ... Please, wait.\n");
                        CurrencyListMessage();
                        break;

                    case "-h":
                    case "--help":
                        Console.WriteLine();
                        CmdHelpMessage();
                        break;

                    default:
                        if (arg.Length == 3 && arg.All(c => char.IsLetter(c)))
                        {
                            Console.Write("\n Curriency rate is loading ... Please, wait.\n ");
                            currencyHandler.ShowCurrencyRateAsync(arg).GetAwaiter().GetResult();
                        }
                        else
                            Console.Write($"\n Unknown command {arg} or curriency abbreviation... Try again, please.\n ");
                        break;
                }
            }
        }

        // Display the list of possible commands
        static void CmdHelpMessage()
        {
                Console.WriteLine("\nInput currency abbreviation (e.g. USD, EUR, RUB etc) or the following command:");
                Console.WriteLine("\t`-a`,`--all`       --- show all currency exchange rates");
                Console.WriteLine("\t`-c`,`--currency`  --- info on the available currencies abbreviations");
                Console.WriteLine("\t`-s`,`--save`      --- enable/disable saving to *.txt file (disable by default)");
                Console.WriteLine("\t`-h`,`--help`      --- show available commands");
        }
    }
}