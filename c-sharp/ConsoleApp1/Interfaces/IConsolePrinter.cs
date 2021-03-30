namespace ConsoleApp1
{
    public interface IConsolePrinter
    {
        string ToString();
        ConsolePrinter Value(string value);
        void PrintLine(bool isCategory);
        public void PrintRow(bool isCategory, params string[] columns);
    }
}