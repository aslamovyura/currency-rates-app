using System;
using System.Linq;
using System.Threading.Tasks;
using CurrencyExchange.Core.Controllers;

namespace CurrencyExchange.AppConsole
{
    class Program
    {
        // Object containing info on the currency exchange rates.
        static CurrencyController currencyController = new CurrencyController();

        static void Main(string[] args)
        {
            // Processing of command-line arguments
            if (args.Length != 0)
            {
                CmdArgsProcessing(args);
                return;
            }
                
            // Check currency rates for today.
            Task.Run(() => currencyController.CheckCurrencyRates()).ContinueWith((t) => ShowCursor());

            GreetingMessage();
            HelpMessage();
            ShowCursor();
            while (true)
            {
                var input = Console.ReadLine().Trim().ToUpper();
                switch (input)
                {
                    case "A":
                    case "ALL":
                        Console.Write("\n Curriency rates are loading ... Please, wait.\n");
                        currencyController.ShowAllCurrencyRatesAsync().
                            ContinueWith((t) => ShowCursor());
                        break;

                    case "I":
                    case "INFO":
                        Console.Write("\n Curriency rates are loading ... Please, wait.\n");
                        Task t1 = currencyController.ShowWorldCurrenciesListAsync().ContinueWith((t) => ShowCursor());
                        break;
                            
                    case "H":
                    case "HELP":
                        Console.WriteLine();
                        HelpMessage();
                        CurrencyListMessage();
                        ShowCursor();
                        break;

                    case "S":
                    case "SAVE":
                        SaveToFileEnable();
                        ShowCursor();
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
                            currencyController.ShowCurrencyRateAsync(input).ContinueWith((t) => ShowCursor());
                        }
                        else
                        {
                            Console.Write("\n Unknown command or curriency abbreviation... Try again, please.\n ");
                            ShowCursor();
                        }   
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
        }

        // Print help message
        static void CurrencyListMessage()
        {
            Console.WriteLine("\nThe following currency abbreviations is available:");
            currencyController.ShowAvailableCurrencies();
        }

        static void ShowCursor()
        {
            Console.Write("\nUser input: ");
        }

        // Enable / disable save to file
        static void SaveToFileEnable()
        {
            string status;
            if (currencyController.SaveToFileEnable)
            {
                currencyController.SaveToFileEnable = false;
                status = "disable";
            }     
            else
            {
                currencyController.SaveToFileEnable = true;
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
                currencyController.SaveToFileEnable = false;
                status = "disable";
            }
            else
            {
                currencyController.SaveToFileEnable = true;
                status = "enable";
            }
            Console.WriteLine($"\nSaving to *.txt file is {status.ToUpper()} now.\n");
        }

        static void HelpMessage()
        {
            Console.WriteLine("`Currency Exchange` application provides you with information about the currencies exchange rates (based on http://www.nbrb.by)");
            Console.WriteLine("P.S.: Application has a delay (3 sec) in requests & saving data to file.");
            Console.WriteLine("P.P.S: All processing data will be saved to the *.txt file .../TMS-DotNet-Team1-YAP/src/CurrencyExchange/CurrencyExchangeApp/bin/Debug/netcoreapp3.1/Temp/temp.txt");
            Console.WriteLine("\nInput currency abbreviation (e.g. USD, EUR, RUB etc) or the following key:");
            Console.WriteLine("\t`a` --- show all currency exchange rates");
            Console.WriteLine("\t`l` --- list of the main world currencies");
            Console.WriteLine("\t`s` --- enable/disable saving to *.txt file (disable by default)");
            Console.WriteLine("\t`h` --- help massage");
            Console.WriteLine("\t`q` --- quit program");
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
                        currencyController.ShowAllCurrencyRatesAsync().GetAwaiter().GetResult();
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
                            currencyController.ShowCurrencyRateAsync(arg).GetAwaiter().GetResult();
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