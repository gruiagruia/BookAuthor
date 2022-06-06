using AuthorAPI.Models;

namespace AuthorAPI.Interfaces;

public interface IBookHome
{
    Task<ICollection<Book>> GetAsync();
    Task<Book> AddAsync(Book book, int id);

    Task DeleteAsync(int id);
}