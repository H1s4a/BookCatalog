using BookCatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BookTag> BookTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookTag>()
                .HasKey(bt => new { bt.BookId, bt.TagId });

            modelBuilder.Entity<BookTag>()
                .HasOne(bt => bt.Book)
                .WithMany(b => b.BookTags)
                .HasForeignKey(bt => bt.BookId);

            modelBuilder.Entity<BookTag>()
                .HasOne(bt => bt.Tag)
                .WithMany(t => t.BookTags)
                .HasForeignKey(bt => bt.TagId);

            // Seed данни за Genre
            modelBuilder.Entity<Genre>().HasData(
                new Genre { GenreId = 1, Name = "Фентъзи", Info = "Потопете се в магически светове и епични приключения.", ImagePath = "/images/fantasy.jpg" },
                new Genre { GenreId = 2, Name = "Драма", Info = "Изживейте интензивни разкази и силни емоции.", ImagePath = "/images/drama.jpg" },
                new Genre { GenreId = 3, Name = "Научна фантастика", Info = "Започнете междузвездни пътешествия и космически приключения.", ImagePath = "/images/sci-fi.jpg" },
                new Genre { GenreId = 4, Name = "Романтика", Info = "Потопете се в сърдечни истории за любов и страст.", ImagePath = "/images/romance.jpg" }
            );

            // Seed данни за Author
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, Name = "J.K. Rowling", Bio = "Джоан Роулинг е британска писателка, известна с поредицата за \"Хари Потър\". Тя създава магическия свят на Хогуортс, който завладява милиони читатели.", ImagePath = "/images/rowling.jpg" },
                new Author { AuthorId = 2, Name = "Stephen King", Bio = "Стивън Кинг е американски писател, наричан \"Кралят на ужаса\". Неговите романи са адаптирани в множество филми и сериали.", ImagePath = "/images/king.jpg" },
                new Author { AuthorId = 3, Name = "Agatha Christie", Bio = "Агата Кристи е британска писателка, известна с криминалните си романи. Тя е автор на някои от най-продаваните книги в света.", ImagePath = "/images/christie.jpg" },
                new Author { AuthorId = 4, Name = "G.R.R. Martin", Bio = "Джордж Р.Р. Мартин е американски писател, известен с фентъзи поредицата си \"Песен за огън и лед\", която вдъхновява сериала \"Игра на тронове\".", ImagePath = "/images/martin.jpg" }
            );

            // Seed данни за Book
            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, AuthorId = 1, GenreId = 1, Title = "The Great Gatsby", ImagePath = "/images/gatsby.jpg", BookInformation = "" },
                new Book { BookId = 2, AuthorId = 2, GenreId = 2, Title = "To Kill a Mockingbird", ImagePath = "/images/mockingbird.jpg", BookInformation = "" },
                new Book { BookId = 3, AuthorId = 3, GenreId = 3, Title = "1984", ImagePath = "/images/1984.jpg", BookInformation = "" },
                new Book { BookId = 4, AuthorId = 4, GenreId = 4, Title = "Pride and Prejudice", ImagePath = "/images/pride.jpg", BookInformation = "" }
            );

            // Seed данни за Tag
            modelBuilder.Entity<Tag>().HasData(
                new Tag { TagId = 1, Name = "Класика" },
                new Tag { TagId = 2, Name = "Трилър" },
                new Tag { TagId = 3, Name = "Любовна история" },
                new Tag { TagId = 4, Name = "Дистопия" }
            );

            // Seed данни за BookTag
            modelBuilder.Entity<BookTag>().HasData(
                new BookTag { BookId = 1, TagId = 1 },
                new BookTag { BookId = 1, TagId = 3 }, 
                new BookTag { BookId = 2, TagId = 1 },
                new BookTag { BookId = 3, TagId = 4 },
                new BookTag { BookId = 4, TagId = 3 } 
            );
        }
    }
}
