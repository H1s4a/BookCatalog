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

           
            modelBuilder.Entity<Genre>().HasData(
                new Genre { GenreId = 1, Name = "Фентъзи", Info = "Потопете се в магически светове и епични приключения.", ImagePath = "/images/fantasy.jpg" },
                new Genre { GenreId = 2, Name = "Драма", Info = "Изживейте интензивни разкази и силни емоции.", ImagePath = "/images/drama.jpg" },
                new Genre { GenreId = 3, Name = "Научна фантастика", Info = "Започнете междузвездни пътешествия и космически приключения.", ImagePath = "/images/sci-fi.jpg" },
                new Genre { GenreId = 4, Name = "Романтика", Info = "Потопете се в сърдечни истории за любов и страст.", ImagePath = "/images/romance.jpg" },
                new Genre { GenreId = 5, Name = "Роман", Info = "Преживейте завладяващи разкази за човешки взаимоотношения и съдби.", ImagePath = "/images/roman.jpg" },
                new Genre { GenreId = 6, Name = "Ужаси", Info = "Потопете се в ужасяващи истории, които ще ви накарат да настръхнете.", ImagePath = "/images/ujas.jpg" },
                new Genre { GenreId = 7, Name = "Мистерия", Info = "Разгадайте загадъчни престъпления и неразрешени случаи.", ImagePath = "/images/misteriq.jpg" }


            );


            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, Name = "Дж. К. Роулинг", Bio = "Джоан Роулинг е британска писателка, известна с поредицата за \"Хари Потър\". Родена на 31 юли 1965 г. в Йейт, Англия, тя израства с любов към литературата и мечтата да стане писател. Идеята за \"Хари Потър\" ѝ хрумва по време на пътуване с влак през 1990 г. След години на трудности и откази от издатели, първата книга - \"Хари Потър и философският камък\" - е публикувана през 1997 г. и постига огромен успех. Поредицата включва седем книги, преведени на над 80 езика, и се е продала в повече от 500 милиона копия. Освен \"Хари Потър\", Роулинг е автор на криминални романи под псевдонима Робърт Галбрейт, сред които \"Зовът на кукувицата\" и \"Копринената буба\". Тя е носител на множество награди, включително Ордена на Британската империя, и е активен филантроп.", ImagePath = "/images/rowling.jpg" },
                new Author { AuthorId = 2, Name = "Стивън Кинг", Bio = "Стивън Кинг е американски писател, известен като \"Кралят на ужаса\". Роден на 21 септември 1947 г. в Портланд, Мейн, Кинг започва да пише от ранна възраст. Първият му голям успех идва с романа \"Кери\" (1974), който е отхвърлен 30 пъти преди да бъде публикуван. Творчеството му включва над 60 романа и 200 разказа, много от които са адаптирани в култови филми и сериали. Сред най-известните му творби са \"Сиянието\", \"То\", \"Мизъри\", \"Зеленият път\", \"Гробище за домашни любимци\" и \"Тъмната кула\". Стилът му съчетава ужаси, психологически трилър и социална критика. Кинг е носител на множество награди, включително Брам Стокър, Световната награда за фентъзи и Националния медал за изкуство, връчен му от президента на САЩ.", ImagePath = "/images/king.jpg" },
                new Author { AuthorId = 3, Name = "Агата Кристи", Bio = "Агата Кристи е британска писателка, известна като \"Кралицата на криминалния жанр\". Родена на 15 септември 1890 г. в Торки, Англия, тя започва да пише детективски романи през 20-те години на XX век. Нейните книги, сред които \"Убийство в Ориент Експрес\", \"Десет малки негърчета\" и \"Смърт край Нил\", са продадени в над 2 милиарда копия, което я прави един от най-продаваните автори в историята. Нейните герои, детектив Еркюл Поаро и госпожица Марпъл, се превръщат в икони на криминалния жанр. Кристи е носител на Ордена на Британската империя и първата жена, която получава титлата \"Дама\" за приноса си към литературата. Освен романи, тя пише пиеси, включително \"Капан за мишки\" - най-дълго играното театрално представление в света.", ImagePath = "/images/christie.jpg" },
                new Author { AuthorId = 4, Name = "Джордж Р. Р. Мартин", Bio = "Джордж Р. Р. Мартин е американски писател, известен с фентъзи поредицата \"Песен за огън и лед\". Роден на 20 септември 1948 г. в Байон, Ню Джърси, той започва кариерата си като автор на научна фантастика и хорър. Първата книга от \"Песен за огън и лед\", \"Игра на тронове\", излиза през 1996 г. и бързо набира популярност. Историята е вдъхновена от реални исторически събития като Войната на розите. Поредицата включва сложни герои, политически интриги и неочаквани обрати. Успехът ѝ води до създаването на хитовия сериал \"Игра на тронове\" (2011-2019) на HBO. Освен писател, Мартин е сценарист и телевизионен продуцент. Работил е по сериали като \"Зоната на здрача\" и \"Красавицата и звярът\". Той е носител на наградите Хюго, Небюла и Локус, а неговото творчество има огромно влияние върху съвременната фентъзи литература.", ImagePath = "/images/martin.jpg" }
                );

            
            modelBuilder.Entity<Book>().HasData(
            new Book { BookId = 1, AuthorId = 1, GenreId = 1, Title = "Хари Потър и философският камък", ImagePath = "/images/haripg.jpg", BookInformation = "„Хари Потър и философският камък“ е първата книга от поредицата на Дж. К. Роулинг, която разказва за приключенията на младия магьосник Хари Потър, докато открива своя магически свят и битката си срещу тъмнината." },
            new Book { BookId = 2, AuthorId = 2, GenreId = 6, Title = "То", ImagePath = "/images/itjpg.jpg", BookInformation = "Романът „То“ на Стивън Кинг разказва за група деца, които се борят срещу древно, зловещо същество, което приема формата на клоун и се завръща в родния им град." },
            new Book { BookId = 3, AuthorId = 3, GenreId = 5, Title = "Убийството на Роджър Акройд", ImagePath = "/images/murderroger.jpg", BookInformation = "„Убийството на Роджър Акройд“ на Агата Кристи е класически криминален роман, в който детектив Херкюл Поаро разкрива мистерията около убийството на заможния Роджър Акройд в малко английско селце." },
            new Book { BookId = 4, AuthorId = 4, GenreId = 1, Title = "Игра на тронове", ImagePath = "/images/game_of_thrones.jpg", BookInformation = "„Игра на тронове“ е първата книга от поредицата „Песен за огън и лед“ на Джордж Р. Р. Мартин. Тя представя сложния свят на Вестерос, където благородниците се борят за власт, а магията и драконите се завръщат." },
            new Book { BookId = 5, AuthorId = 1, GenreId = 1, Title = "Хари Потър и тайната стая", ImagePath = "/images/harry_potter_2.jpg", BookInformation = "Във втората книга от поредицата, Хари Потър се завръща в Хогуортс и открива, че училището е заплашено от мистериозна сила, която затваря Тайната стая." },
            new Book { BookId = 6, AuthorId = 2, GenreId = 6, Title = "Кери", ImagePath = "/images/carrie.jpg", BookInformation = "„Кери“ на Стивън Кинг разказва историята на тийнейджърка с телепатични способности, която се отмъщава на своите съученици и майка си, когато е подложена на тормоз." },
            new Book { BookId = 7, AuthorId = 3, GenreId = 7, Title = "10 малки негърчета", ImagePath = "/images/ten.jpg", BookInformation = "„10 малки индианци“ на Агата Кристи е известен криминален роман, в който десет души, поканени на остров, започват да умират един по един при загадъчни обстоятелства." },
            new Book { BookId = 8, AuthorId = 4, GenreId = 2, Title = "Сблъсък на крале", ImagePath = "/images/clash_of_kings.jpg", BookInformation = "„Сблъсък на крале“ е втората книга от поредицата „Песен за огън и лед“ на Джордж Р. Р. Мартин, в която битката за Железния трон в Вестерос продължава, а нови заплахи се появяват отвън." },
            new Book { BookId = 9, AuthorId = 2, GenreId = 3, Title = "Зеленият път", ImagePath = "/images/zeleno.jpg", BookInformation = "„Зеленият път“ разказва за надзирател в затвор и затворник със свръхестествени способности, който може би е невинен." }
            );


            
            modelBuilder.Entity<Tag>().HasData(
                new Tag { TagId = 1, Name = "Класика" },
                new Tag { TagId = 2, Name = "Трилър" },
                new Tag { TagId = 3, Name = "Любовна история" },
                new Tag { TagId = 4, Name = "Дистопия" }
            );


            modelBuilder.Entity<BookTag>().HasData(
            new BookTag { BookId = 1, TagId = 1 },
            new BookTag { BookId = 1, TagId = 4 },
            new BookTag { BookId = 2, TagId = 2 },
            new BookTag { BookId = 2, TagId = 4 },
            new BookTag { BookId = 3, TagId = 1 },
            new BookTag { BookId = 3, TagId = 2 },
            new BookTag { BookId = 4, TagId = 4 },
            new BookTag { BookId = 5, TagId = 1 },
            new BookTag { BookId = 5, TagId = 4 },
            new BookTag { BookId = 6, TagId = 2 },
            new BookTag { BookId = 6, TagId = 4 },
            new BookTag { BookId = 7, TagId = 1 },
            new BookTag { BookId = 7, TagId = 2 },
            new BookTag { BookId = 8, TagId = 4 },
            new BookTag { BookId = 9, TagId = 1 },
            new BookTag { BookId = 9, TagId = 3 }
            );

        }
    }
}
