using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConsoleApp1
{
    public class StartUpService : IStartUpService
    {
        private readonly ILogger<StartUpService> _log;
        private readonly IConfiguration _config;
        private readonly IConsolePrinter _printer;
        private readonly IJsonFeed _jsonFeed;
        private JokesOptions _jokesOptions;



        static string[] results = new string[50];
        static char key;
        static Tuple<string, string> names;

        public StartUpService(ILogger<StartUpService> log, IConfiguration config, IConsolePrinter printer, IJsonFeed jsonFeed, IOptions<JokesOptions> jokesOptions)
        {
            _log = log;
            _config = config;
            _printer = printer;
            _jsonFeed = jsonFeed;
            _jokesOptions = jokesOptions.Value;
        }

        /// <summary>
        /// Starting point for App
        /// </summary>
        public void Run()
        {
            _printer.Value("Press ? to get instructions.").ToString();
           
            if (Console.ReadLine() == "?")
            {
                //logging example
                _log.LogInformation("We are running the main application");
                while (true)
                {
                    _printer.Value("Press c to get categories").ToString();
                    _printer.Value("Press r to get random jokes").ToString();
                    GetEnteredKey(Console.ReadKey());
                    Console.WriteLine();

                    if (key == 'c')
                    {
                        GetCategories();
                        PrintResults(true);
                    }
                    if (key == 'r')
                    {
                        GetRandomJokes();
                    }
                    names = null;
                }
            }
        }

        #region Private Methods
        /// <summary>
        /// GetRandomJokes
        /// </summary>
        private void GetRandomJokes()
        {
            _printer.Value("Want to use a random name? y/n").ToString();
            GetEnteredKey(Console.ReadKey());
            Console.WriteLine();

            if (key == 'y')
            {
                GetNames();
            }

            _printer.Value("Want to specify a category? y/n").ToString();
            GetEnteredKey(Console.ReadKey());
            Console.WriteLine();
            if (key == 'y')
            {
                _printer.Value("Enter a category;").ToString();
                string categoryName = Console.ReadLine();
                GetJokePreference(categoryName);
            }
            else
            {
                GetJokePreference(null);
            }
        }

        /// <summary>
        /// GetJokePreference
        /// </summary>
        /// <param name="categoryName"></param>
        private void GetJokePreference(string categoryName)
        {
            _printer.Value("How many jokes do you want? (1-9)").ToString();
            int val = ValidateJokesInput();
            GetRandomJokes(categoryName, val);
            PrintResults(false);
        }

        /// <summary>
        /// Print Results
        /// </summary>
        /// <param name="isCategory"></param>
        private void PrintResults(bool isCategory)
        {
            _printer.PrintLine(isCategory);
            foreach (var element in results)
            {
                _printer.PrintRow(isCategory, element);
                _printer.PrintLine(isCategory);
            }
        }



        /// <summary>
        /// ValidateJokesInput
        /// </summary>
        /// <returns></returns>
        private int ValidateJokesInput()
        {
            int val;
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out val))
                    Console.Write("Please enter a valid integer value:");
                else if (val < 1 || val > 9)
                    _printer.Value("Please choose input between (1-9)").ToString();
                else
                    break;
            }
            return val;
        }

        /// <summary>
        /// GetEnteredKey
        /// </summary>
        /// <param name="consoleKeyInfo"></param>
        private void GetEnteredKey(ConsoleKeyInfo consoleKeyInfo)
        {

            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.C:
                    key = 'c';
                    break;
                case ConsoleKey.D0:
                    key = '0';
                    break;
                case ConsoleKey.D1:
                    key = '1';
                    break;
                case ConsoleKey.D3:
                    key = '3';
                    break;
                case ConsoleKey.D4:
                    key = '4';
                    break;
                case ConsoleKey.D5:
                    key = '5';
                    break;
                case ConsoleKey.D6:
                    key = '6';
                    break;
                case ConsoleKey.D7:
                    key = '7';
                    break;
                case ConsoleKey.D8:
                    key = '8';
                    break;
                case ConsoleKey.D9:
                    key = '9';
                    break;
                case ConsoleKey.R:
                    key = 'r';
                    break;
                case ConsoleKey.Y:
                    key = 'y';
                    break;
                case ConsoleKey.N:
                    key = 'n';
                    break;
                default:
                    key = ' ';
                    break;
            }
        }

        /// <summary>
        /// GetRandomJokes
        /// </summary>
        /// <param name="category"></param>
        /// <param name="number"></param>
        private void GetRandomJokes(string category, int number)
        {
            results = _jsonFeed.GetRandomJokes(names?.Item1, names?.Item2, category, number);
        }

        /// <summary>
        /// GetCategories
        /// </summary>
        private void GetCategories()
        {
            results = _jsonFeed.GetCategories();
        }

        /// <summary>
        /// GetNames
        /// </summary>
        private void GetNames()
        {
            var result = _jsonFeed.GetNames();
            names = Tuple.Create(result.name.ToString(), result.surname.ToString());
        }
        #endregion


    }
}
