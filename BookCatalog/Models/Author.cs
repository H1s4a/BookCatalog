﻿namespace BookCatalog.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string? Bio { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
