using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookCatalog.Migrations
{
    /// <inheritdoc />
    public partial class bo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Info = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookTags",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTags", x => new { x.BookId, x.TagId });
                    table.ForeignKey(
                        name: "FK_BookTags_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Bio", "ImagePath", "Name" },
                values: new object[,]
                {
                    { 1, "Джоан Роулинг е британска писателка, известна с поредицата за \"Хари Потър\". Родена на 31 юли 1965 г. в Йейт, Англия, тя израства с любов към литературата и мечтата да стане писател. Идеята за \"Хари Потър\" ѝ хрумва по време на пътуване с влак през 1990 г. След години на трудности и откази от издатели, първата книга - \"Хари Потър и философският камък\" - е публикувана през 1997 г. и постига огромен успех. Поредицата включва седем книги, преведени на над 80 езика, и се е продала в повече от 500 милиона копия. Освен \"Хари Потър\", Роулинг е автор на криминални романи под псевдонима Робърт Галбрейт, сред които \"Зовът на кукувицата\" и \"Копринената буба\". Тя е носител на множество награди, включително Ордена на Британската империя, и е активен филантроп.", "/images/rowling.jpg", "Дж. К. Роулинг" },
                    { 2, "Стивън Кинг е американски писател, известен като \"Кралят на ужаса\". Роден на 21 септември 1947 г. в Портланд, Мейн, Кинг започва да пише от ранна възраст. Първият му голям успех идва с романа \"Кери\" (1974), който е отхвърлен 30 пъти преди да бъде публикуван. Творчеството му включва над 60 романа и 200 разказа, много от които са адаптирани в култови филми и сериали. Сред най-известните му творби са \"Сиянието\", \"То\", \"Мизъри\", \"Зеленият път\", \"Гробище за домашни любимци\" и \"Тъмната кула\". Стилът му съчетава ужаси, психологически трилър и социална критика. Кинг е носител на множество награди, включително Брам Стокър, Световната награда за фентъзи и Националния медал за изкуство, връчен му от президента на САЩ.", "/images/king.jpg", "Стивън Кинг" },
                    { 3, "Агата Кристи е британска писателка, известна като \"Кралицата на криминалния жанр\". Родена на 15 септември 1890 г. в Торки, Англия, тя започва да пише детективски романи през 20-те години на XX век. Нейните книги, сред които \"Убийство в Ориент Експрес\", \"Десет малки негърчета\" и \"Смърт край Нил\", са продадени в над 2 милиарда копия, което я прави един от най-продаваните автори в историята. Нейните герои, детектив Еркюл Поаро и госпожица Марпъл, се превръщат в икони на криминалния жанр. Кристи е носител на Ордена на Британската империя и първата жена, която получава титлата \"Дама\" за приноса си към литературата. Освен романи, тя пише пиеси, включително \"Капан за мишки\" - най-дълго играното театрално представление в света.", "/images/christie.jpg", "Агата Кристи" },
                    { 4, "Джордж Р. Р. Мартин е американски писател, известен с фентъзи поредицата \"Песен за огън и лед\". Роден на 20 септември 1948 г. в Байон, Ню Джърси, той започва кариерата си като автор на научна фантастика и хорър. Първата книга от \"Песен за огън и лед\", \"Игра на тронове\", излиза през 1996 г. и бързо набира популярност. Историята е вдъхновена от реални исторически събития като Войната на розите. Поредицата включва сложни герои, политически интриги и неочаквани обрати. Успехът ѝ води до създаването на хитовия сериал \"Игра на тронове\" (2011-2019) на HBO. Освен писател, Мартин е сценарист и телевизионен продуцент. Работил е по сериали като \"Зоната на здрача\" и \"Красавицата и звярът\". Той е носител на наградите Хюго, Небюла и Локус, а неговото творчество има огромно влияние върху съвременната фентъзи литература.", "/images/martin.jpg", "Джордж Р. Р. Мартин" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "ImagePath", "Info", "Name" },
                values: new object[,]
                {
                    { 1, "/images/fantasy.jpg", "Потопете се в магически светове и епични приключения.", "Фентъзи" },
                    { 2, "/images/drama.jpg", "Изживейте интензивни разкази и силни емоции.", "Драма" },
                    { 3, "/images/sci-fi.jpg", "Започнете междузвездни пътешествия и космически приключения.", "Научна фантастика" },
                    { 4, "/images/romance.jpg", "Потопете се в сърдечни истории за любов и страст.", "Романтика" },
                    { 5, "/images/roman.jpg", "Преживейте завладяващи разкази за човешки взаимоотношения и съдби.", "Роман" },
                    { 6, "/images/ujas.jpg", "Потопете се в ужасяващи истории, които ще ви накарат да настръхнете.", "Ужаси" },
                    { 7, "/images/misteriq.jpg", "Разгадайте загадъчни престъпления и неразрешени случаи.", "Мистерия" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "TagId", "Name" },
                values: new object[,]
                {
                    { 1, "Класика" },
                    { 2, "Трилър" },
                    { 3, "Любовна история" },
                    { 4, "Дистопия" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "AuthorId", "BookInformation", "GenreId", "ImagePath", "Title" },
                values: new object[,]
                {
                    { 1, 1, "„Хари Потър и философският камък“ е първата книга от поредицата на Дж. К. Роулинг, която разказва за приключенията на младия магьосник Хари Потър, докато открива своя магически свят и битката си срещу тъмнината.", 1, "/images/haripg.jpg", "Хари Потър и философският камък" },
                    { 2, 2, "Романът „То“ на Стивън Кинг разказва за група деца, които се борят срещу древно, зловещо същество, което приема формата на клоун и се завръща в родния им град.", 6, "/images/itjpg.jpg", "То" },
                    { 3, 3, "„Убийството на Роджър Акройд“ на Агата Кристи е класически криминален роман, в който детектив Херкюл Поаро разкрива мистерията около убийството на заможния Роджър Акройд в малко английско селце.", 5, "/images/murderroger.jpg", "Убийството на Роджър Акройд" },
                    { 4, 4, "„Игра на тронове“ е първата книга от поредицата „Песен за огън и лед“ на Джордж Р. Р. Мартин. Тя представя сложния свят на Вестерос, където благородниците се борят за власт, а магията и драконите се завръщат.", 1, "/images/game_of_thrones.jpg", "Игра на тронове" },
                    { 5, 1, "Във втората книга от поредицата, Хари Потър се завръща в Хогуортс и открива, че училището е заплашено от мистериозна сила, която затваря Тайната стая.", 1, "/images/harry_potter_2.jpg", "Хари Потър и тайната стая" },
                    { 6, 2, "„Кери“ на Стивън Кинг разказва историята на тийнейджърка с телепатични способности, която се отмъщава на своите съученици и майка си, когато е подложена на тормоз.", 6, "/images/carrie.jpg", "Кери" },
                    { 7, 3, "„10 малки индианци“ на Агата Кристи е известен криминален роман, в който десет души, поканени на остров, започват да умират един по един при загадъчни обстоятелства.", 7, "/images/ten.jpg", "10 малки негърчета" },
                    { 8, 4, "„Сблъсък на крале“ е втората книга от поредицата „Песен за огън и лед“ на Джордж Р. Р. Мартин, в която битката за Железния трон в Вестерос продължава, а нови заплахи се появяват отвън.", 2, "/images/clash_of_kings.jpg", "Сблъсък на крале" },
                    { 9, 2, "„Зеленият път“ разказва за надзирател в затвор и затворник със свръхестествени способности, който може би е невинен.", 3, "/images/zeleno.jpg", "Зеленият път" }
                });

            migrationBuilder.InsertData(
                table: "BookTags",
                columns: new[] { "BookId", "TagId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 4 },
                    { 2, 2 },
                    { 2, 4 },
                    { 3, 1 },
                    { 3, 2 },
                    { 4, 4 },
                    { 5, 1 },
                    { 5, 4 },
                    { 6, 2 },
                    { 6, 4 },
                    { 7, 1 },
                    { 7, 2 },
                    { 8, 4 },
                    { 9, 1 },
                    { 9, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTags_TagId",
                table: "BookTags",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookTags");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
