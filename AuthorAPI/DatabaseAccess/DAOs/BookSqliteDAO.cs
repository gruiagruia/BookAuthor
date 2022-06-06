using AuthorAPI.DatabaseAccess.Context;
using AuthorAPI.Interfaces;
using AuthorAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AuthorAPI.DatabaseAccess.DAOs;

public class BookSqliteDAO : IBookHome
{
    
    private readonly PublishContext _context;

    public BookSqliteDAO(PublishContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Book>> GetAsync()
    {
        ICollection<Book> books = await _context.Books.ToListAsync();
        return books;
    }

    public async Task<Book> AddAsync(Book book,int id)
    {
        
        Author? author = await _context.Authors.FindAsync(id);
        author.books.Add(book);
        _context.Authors.Update(author);
        await _context.SaveChangesAsync();
        
        EntityEntry<Book> added = await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        Console.WriteLine(added.Entity.Title);
        return added.Entity;
    }

    public async Task DeleteAsync(int id)
    {
        Book? existing = await _context.Books.FindAsync(id);
        if (existing is null)
        {
            throw new Exception($"Could not find Book with id {id}. Nothing was deleted");
        }

        _context.Books.Remove(existing);
        await _context.SaveChangesAsync();
    }
}