using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class ConsolePrinter : IConsolePrinter
    {
        public static object PrintValue;
        static readonly int tableWidthJokes = 200;
        static readonly int tableWidthCategory = 50;


        public ConsolePrinter Value(string value)
        {
            PrintValue = value;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            Console.WriteLine(PrintValue);
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isCategory"></param>
        public void PrintLine(bool isCategory)
        {
            Console.WriteLine(new string('-', isCategory ? tableWidthCategory : tableWidthJokes));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isCategory"></param>
        /// <param name="columns"></param>
        public void PrintRow(bool isCategory, params string[] columns)
        {
            int width = (isCategory ? tableWidthCategory : tableWidthJokes - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}
