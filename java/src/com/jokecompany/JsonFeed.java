package com.jokecompany;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.io.IOException;
import java.net.URI;
import java.net.URISyntaxException;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;

public class JsonFeed {
    static String url = "";

    public JsonFeed(String endpoint, int results) {
        url = endpoint;
    }

    public static String[] getRandomJokes(String firstname, String lastname, String category) throws URISyntaxException, IOException, InterruptedException {
        HttpClient client = HttpClient.newHttpClient();
        url += "random/";
        if (category != null)
            url += category;
        URI uri = new URI(url);
        HttpRequest request = HttpRequest.newBuilder().uri(uri).build();
        String joke = client.send(request, HttpResponse.BodyHandlers.ofString()).body();
        if (firstname != null && lastname != null)
        {
            int index = joke.indexOf("Chuck Norris");
            String firstPart = joke.substring(0, index);
            String secondPart = joke.substring(index + "Chuck Norris".length());
            joke = firstPart + " " + firstname + " " + lastname + secondPart;
        }
        Gson jsonobject = new GsonBuilder().disableHtmlEscaping().create();
        return new String[] {jsonobject.toJson(joke)};
    }

    public static Dto getnames() throws URISyntaxException, IOException, InterruptedException {
        HttpClient client = HttpClient.newHttpClient();
        URI uri = new URI(url);
        HttpRequest request = HttpRequest.newBuilder().uri(uri).build();
        String names = client.send(request, HttpResponse.BodyHandlers.ofString()).body();
        Gson gson = new Gson();
        return gson.fromJson(names, Dto.class);
    }

    public static String[] getCategories() throws IOException, InterruptedException, URISyntaxException {
        HttpClient client = HttpClient.newHttpClient();
        URI uri = new URI(url);
        HttpRequest request = HttpRequest.newBuilder().uri(uri).build();
        String responsebody = client.send(request, HttpResponse.BodyHandlers.ofString()).body();
        return new String[] {new Gson().toJson(responsebody)};
    }
}
