namespace BookCatalog.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public required string Name { get; set; }
        public string? Info { get; set; }
        public string ImagePath { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
