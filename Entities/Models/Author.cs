using System.ComponentModel.DataAnnotations;

namespace AuthorAPI.Models;

public class Author
{
    [Key]
    public int Id { get; set; }
    
    [Required,MaxLength(15)]
    public string FirstName { get; set; }
    
    [Required,MaxLength(15)]
    public string LastName { get; set; }

    public ICollection<Book> books = new List<Book>();

    public Author()
    {
    }

    public Author(int id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }
    public Author( string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
    
}