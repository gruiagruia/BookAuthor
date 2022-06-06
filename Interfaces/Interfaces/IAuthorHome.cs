using AuthorAPI.Models;

namespace AuthorAPI.Interfaces;

public interface IAuthorHome
{
    Task<ICollection<Author>> GetAsync();
    Task<Author> AddAsync(Author author);
}