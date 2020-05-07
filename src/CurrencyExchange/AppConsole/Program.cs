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

        static void Main()
        {
            // Check currency rates for today.
            Task.Run(() => currencyController.CheckCurrencyRates());

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

        static void ShowCursor() => Console.Write("\nUser input: ");

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
            Console.WriteLine($"\nSaving to *.txt file is {status.ToUpper()} now.");
        }

        static void HelpMessage()
        {
            Console.WriteLine("`Currency Exchange` application provides you with information about the currencies exchange rates (based on http://www.nbrb.by)");
            Console.WriteLine("\nInput the following command:");
            Console.WriteLine("  a | all       Display all currency exchange rates.");
            Console.WriteLine("  s | save      Enable/disable saving to rates.txt file (DISABLE by default).");
            Console.WriteLine("  h | help      Show help massage.");
            Console.WriteLine("  q | quit      Quit program.");
            Console.WriteLine("\nor");
            Console.WriteLine("  Input one of the following currency codes:");
            currencyController.ShowAvailableCurrencies();
        }
    }
}