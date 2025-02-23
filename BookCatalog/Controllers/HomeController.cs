using BookCatalog.Data;
using BookCatalog.Models;
using BookCatalog.Models.BookModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookCatalog.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookDbContext _dbContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, BookDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var books = _dbContext.Books.Include(b => b.Author).Include(b => b.Genre).ToList();
            var authors = _dbContext.Authors.ToList();
            var viewModel = new HomeViewModel
            {
                Books = books,
                Authors = authors
            };
            return View(viewModel);
        }

        public class HomeViewModel
        {
            public IEnumerable<Book> Books { get; set; }
            public IEnumerable<Author> Authors { get; set; }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _dbContext.Books.Include(b => b.Author).Include(b => b.Genre).FirstOrDefault(m => m.BookId == id);

            if (book == null)
                return NotFound();

            var model = new EditBookViewModel
            {
                BookId = book.BookId,
                Title = book.Title,
                GenreId = book.GenreId
            };

            ViewData["GenreId"] = new SelectList(_dbContext.Genres, "GenreId", "Name", book.GenreId);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, EditBookViewModel model)
        {
            if (id != model.BookId)
                return NotFound();

            var book = _dbContext.Books.FirstOrDefault(b => b.BookId == id);
            if (book == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                book.Title = model.Title;
                book.GenreId = model.GenreId;
                _dbContext.Update(book);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewData["GenreId"] = new SelectList(_dbContext.Genres, "GenreId", "Name", model.GenreId);
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var book = _dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.BookTags)
                .ThenInclude(bt => bt.Tag)
                .FirstOrDefault(m => m.BookId == id);

            if (book == null)
                return NotFound();

            return View(book);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var book = _dbContext.Books.Include(b => b.Author).Include(b => b.Genre).FirstOrDefault(m => m.BookId == id);
            if (book == null)
                return NotFound();

            return View(book);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _dbContext.Books.Find(id);
            if (book == null)
                return RedirectToAction("Index");

            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
