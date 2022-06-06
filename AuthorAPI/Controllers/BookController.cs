using AuthorAPI.Interfaces;
using AuthorAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthorAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private IBookHome bookHome;

    public BookController(IBookHome bookHome)
    {
        this.bookHome = bookHome;
    }
    
    [HttpGet]
    public async Task<ActionResult<ICollection<Book>>> GetAll()
    {
        try
        {
            ICollection<Book> books = await bookHome.GetAsync();
            return Ok(books);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost]
    [Route("{id:int}")]
    public async Task<ActionResult<Book>> AddBook([FromBody] Book book, [FromRoute] int id)
    {
        try
        {
            Console.WriteLine("MILSUGI?");
            Book added = await bookHome.AddAsync(book,id);
            Console.WriteLine(added + " MILSUGI?");
            return Created($"/Book/{id}", added);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> DeleteBookById([FromRoute] int id)
    {
        try
        {
            await bookHome.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        } 
    }
}