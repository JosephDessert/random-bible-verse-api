using Microsoft.EntityFrameworkCore;
using random_bible_verse_api.Models;

namespace random_bible_verse_api.Models;

public class VerseContext : DbContext
{
    public VerseContext(DbContextOptions<VerseContext> options)
        : base(options)
    {
    }

    public DbSet<Verse> VerseItem { get; set; } = null!;
}