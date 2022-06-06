using System.Text;
using System.Text.Json;
using AuthorAPI.Interfaces;
using AuthorAPI.Models;

namespace AuthorBlazor.HttpServices;

public class AuthorHttpClient : IAuthorHome
{
    public async Task<ICollection<Author>> GetAsync()
    {
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        
        HttpClient client = new HttpClient(clientHandler);
        HttpResponseMessage response = await client.GetAsync("https://localhost:7138/Author");
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {content}");
        }

        ICollection<Author> authors = JsonSerializer.Deserialize<ICollection<Author>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return authors;
    }

    public async Task<Author> AddAsync(Author author)
    {
        HttpClientHandler clientHandler = new HttpClientHandler();
        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        
        HttpClient client = new HttpClient(clientHandler);

        string authorAsJson = JsonSerializer.Serialize(author);

        StringContent content = new(authorAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync($"https://localhost:7138/Author", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error: {response.StatusCode}, {responseContent}");
        }
        
        Author returned = JsonSerializer.Deserialize<Author>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        
        return returned;
    }
}