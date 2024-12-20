using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using random_bible_verse_api.Models;
using Microsoft.EntityFrameworkCore;


namespace random_bible_verse_api.Controllers;

[ApiController]
[Route("[controller]")]
public class BibleVersesController : ControllerBase
{

    private readonly VerseContext _context;
    private readonly HttpClient _client;

    public BibleVersesController(VerseContext context)
    {
        _context = context;

        _client = new HttpClient();

    }

    [HttpGet]
    public  ActionResult<List<Verse>> getAll(){
        var verses = _context.VerseItem.ToList();
        
       if (verses == null)
        {
            return NotFound();
        }

        return verses;
    }

    [HttpGet("{id}")]
    public ActionResult<List<Verse>> getBookVerses(string id){
        var verses = _context.VerseItem.Where(x => x.book == id).ToList();
        
       if (verses == null)
        {
            return NotFound();
        }
        return Ok(verses);
    }

    
    [HttpGet("books")]
    public ActionResult<List<Verse>> getBooks() {
        List<Verse> retVal =  _context.VerseItem.GroupBy(x => x.book).Select(g => g.First()).ToList();
        return Ok(retVal);
    }

    [HttpGet("new")]
    public async Task<string> getNewVerse(){
        using HttpResponseMessage response = await _client.GetAsync("https://bible-api.com/?random=verse");

        return await response.Content.ReadAsStringAsync();
    }

    [HttpPost]
    public async Task<ActionResult<string>> create([FromBody] Verse entity) {

        _context.VerseItem.Add(entity);
        await _context.SaveChangesAsync();

        return Ok(entity);
        
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> deleteItem(long id) {
        var verse = await _context.VerseItem.FindAsync(id);
        if (verse == null)
        {
            return NotFound();
        }

        _context.VerseItem.Remove(verse);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}