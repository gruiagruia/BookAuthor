using System.Text;
using System.Text.Json;
using AuthorAPI.Interfaces;
using AuthorAPI.Models;

namespace AuthorBlazor.HttpServices;

public class BookHttpClient : IBookHome
{
    public async Task<ICollection<Book>> GetAsync()
    {
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        
        HttpClient client = new HttpClient(clientHandler);
        HttpResponseMessage response = await client.GetAsync("https://localhost:7138/Book");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        ICollection<Book> authors = JsonSerializer.Deserialize<ICollection<Book>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return authors;
    }

    public async Task<Book> AddAsync(Book book, int id)
    {
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        
        HttpClient client = new HttpClient(clientHandler);

        string bookAsJson = JsonSerializer.Serialize(book);
        Console.WriteLine(bookAsJson);
        StringContent content = new(bookAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync($"https://localhost:7138/Book/{id}", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }
        
        Book returned = JsonSerializer.Deserialize<Book>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        Console.WriteLine(returned);
        
        return returned;
    }

    public async Task DeleteAsync(int id)
    {HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        
        HttpClient client = new HttpClient(clientHandler);
        
        HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7064/Book/{id}");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }
    }
}