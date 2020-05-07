using System;
using System.Linq;
using CurrencyExchange.Core.Controllers;

namespace AppCLI
{
    class Program
    {
        // Object containing info on the currency exchange rates.
        static CurrencyController currencyController = new CurrencyController();

        static void Main(string[] args)
        {
            if (!args.Any())
            {
                HelpMessage();
                return;
            }
 
            // Check if it is need to save info to *.txt file.
            if (args.Any(a => a == "-s" || a == "--save"))
            {
                currencyController.SaveToFileEnable = true;
                currencyController.SaveToFileAsync = false;
                args = args.Where(a => a != "-s").Where(a => a != "--save").ToArray();
            }

            // Check other commands
            foreach (string arg in args)
            {
                switch (arg)
                {
                    case "-a":
                    case "--all":
                        {
                            Console.Write("\n Curriency rates are loading ... Please, wait.\n");
                            currencyController.ShowAllCurrencyRatesAsync().GetAwaiter().GetResult();
                        }
                        break;

                    case "-h":
                    case "--help":
                        {
                            Console.WriteLine();
                            HelpMessage();
                        }
                        break;

                    default:
                        {
                            if (arg.Length == 3 && arg.All(c => char.IsLetter(c)))
                            {
                                Console.Write("\n Curriency rate is loading ... Please, wait.\n ");
                                currencyController.ShowCurrencyRateAsync(arg).GetAwaiter().GetResult();
                            }
                            else
                            {
                                Console.Write($"\n Unknown command {arg} or curriency abbreviation... Try again, please.\n ");

                            }
                        }   
                        break;
                }
            }
        }

        // Display the list of possible commands
        static void HelpMessage()
        {
            Console.WriteLine("\nUsage: AppCLI [options]");
            Console.WriteLine("Usage: AppCLI [currency-code]\n");

            Console.WriteLine("Options:");
            Console.WriteLine("  -h|--help          Show command line help.");
            Console.WriteLine("  -a|--all           Display all currency exchange rates.");
            Console.WriteLine("  -s|--save          Enable saving to rates.txt file.");

            Console.WriteLine("\n\nCurrency-Code:");
            currencyController.ShowAvailableCurrencies();

            Console.WriteLine("\nUsage examples:");
            Console.WriteLine("  AppCLI USD -s      Display USD currency rate and save it to file.");
            Console.WriteLine("  AppCLI --all       Display all currency rates without saving to file.\n");
        }
    }
}