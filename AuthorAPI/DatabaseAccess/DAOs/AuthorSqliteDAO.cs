using AuthorAPI.DatabaseAccess.Context;
using AuthorAPI.Interfaces;
using AuthorAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AuthorAPI.DatabaseAccess.DAOs;

public class AuthorSqliteDAO : IAuthorHome
{
    private readonly PublishContext _context;

    public AuthorSqliteDAO(PublishContext context)
    {
        _context = context;
    }


    public async Task<ICollection<Author>> GetAsync()
    {
        ICollection<Author> authors = await _context.Authors.ToListAsync();
        return authors;
    }

    public async Task<Author> AddAsync(Author author)
    {
        EntityEntry<Author> added = await _context.Authors.AddAsync(author);
        await _context.SaveChangesAsync();
        return added.Entity;
    }
}