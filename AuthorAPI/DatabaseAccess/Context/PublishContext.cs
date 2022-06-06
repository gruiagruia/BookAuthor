using AuthorAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorAPI.DatabaseAccess.Context;

public class PublishContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source = D:\DNP\BookAuthor-Exam\AuthorAPI\Publishes.db");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().HasKey(author => author.Id);
        modelBuilder.Entity<Book>().HasKey(book => book.ISBN);
    }
}