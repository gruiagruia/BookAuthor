using AuthorAPI.Interfaces;
using AuthorAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthorAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private IAuthorHome authorHome;

    public AuthorController(IAuthorHome authorHome)
    {
        this.authorHome = authorHome;
    }
    
    [HttpGet]
    public async Task<ActionResult<ICollection<Author>>> GetAll()
    {
        try
        {
            ICollection<Author> todos = await authorHome.GetAsync();
            return Ok(todos);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<Author>> AddAuthor([FromBody] Author author)
    {
        try
        {
            Author added = await authorHome.AddAsync(author);
            return Created($"/authors/{added.Id}", author);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}