package com.jokecompany;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.URISyntaxException;
import java.util.Hashtable;

public class Main {

    static String[] results = new String[50];
    static char key;
    static Hashtable<String, String> names = new Hashtable<>();
    static ConsolePrinter printer = new ConsolePrinter();


    public static void main(String[] args) throws InterruptedException, IOException, URISyntaxException {
        BufferedReader c = new BufferedReader(new InputStreamReader(System.in));
        printer.Value("Press ? to get instructions.").toString();
        String input = c.readLine();
        if (input.equals("?")) {
            while (true) {
                printer.Value("Press c to get categories").toString();
                printer.Value("Press r to get random jokes").toString();
                getEnteredKey(c.readLine());
                if (key == 'c')
                {
                    getCategories();
                    PrintResults();
                }
                if (key == 'r')
                {
                    printer.Value("Want to use a random name? y/n").toString();
                    getEnteredKey(c.readLine());
                    if (key == 'y')
                        getNames();
                    printer.Value("Want to specify a category? y/n").toString();
                    if (key == 'y')
                    {
                        printer.Value("How many jokes do you want? (1-9)").toString();
                        int n = Integer.parseInt(c.readLine());
                        printer.Value("Enter a category;").toString();
                        getRandomJokes(c.readLine(), n);
                        PrintResults();
                    }
                    else
                    {
                        printer.Value("How many jokes do you want? (1-9)").toString();
                        int n = Integer.parseInt(c.readLine());
                        getRandomJokes(null, n);
                        PrintResults();
                    }
                }
                names = null;
            }
        }
    }

    private static void PrintResults()
    {
        printer.Value("[" + String.join(",", results) + "]").toString();
    }

    private static void getEnteredKey(String k) {
        switch (k.substring(0,1))
        {
            case "c":
                key = 'c';
                break;
            case "0" :
                key = '0';
                break;
            case "1":
                key = '1';
                break;
            case "3":
                key = '3';
                break;
            case "4":
                key = '4';
                break;
            case "5":
                key = '5';
                break;
            case "6":
                key = '6';
                break;
            case "7":
                key = '7';
                break;
            case "8":
                key = '8';
                break;
            case "9":
                key = '9';
                break;
            case "r":
                key = 'r';
                break;
            case "y":
                key = 'y';
                break;
        }
    }

    private static void getRandomJokes(String category, int number) throws InterruptedException, IOException, URISyntaxException {
        var var1 = names.entrySet().iterator().next();
        new JsonFeed("https://api.chucknorris.io/jokes/", number);
        results = JsonFeed.getRandomJokes(var1.getKey(), var1.getValue(), category);
    }

    private static void getCategories() throws InterruptedException, IOException, URISyntaxException {
        new JsonFeed("https://api.chucknorris.io/jokes/", 0);
        results = JsonFeed.getCategories();
    }

    private static void getNames() throws InterruptedException, IOException, URISyntaxException {
        new JsonFeed("https://www.names.privserv.com/api/", 0);
        Dto dto = JsonFeed.getnames();
        Main.names.put(dto.getName(), dto.getSurname());
    }
}
