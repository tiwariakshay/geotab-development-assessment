namespace ConsoleApp1
{
    public interface IJsonFeed
    {
        dynamic GetNames();
        string[] GetRandomJokes(string firstname, string lastname, string category, int number);
        string[] GetCategories();
    }
}