namespace random_bible_verse_api.Models;

public class Verse
{
    public long ID {get; set;}
    public String? book {get; set;}

    public String? text {get; set;}

    public long chapter {get; set;}

    public long verseNumber {get; set;}

}
