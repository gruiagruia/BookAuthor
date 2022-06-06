using System.ComponentModel.DataAnnotations;

namespace AuthorAPI.Models;

public class Book
{
    [Key]
    public int ISBN { get; set; }
    
    [Required,MaxLength(40)]
    public string Title { get; set; }
    public int PublicationYear { get; set; }
    public int NumOfPages { get; set; }
    public string Genre { get; set; }
    public Book()
    {
    }

    public Book(int isbn, string title, int publicationYear, int numOfPages, string genre)
    {
        ISBN = isbn;
        Title = title;
        PublicationYear = publicationYear;
        NumOfPages = numOfPages;
        Genre = genre;
    }
}