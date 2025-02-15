using BookCatalog.Data;
using BookCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookCatalog.Controllers
{
    public class SearchController : Controller
    {
        private readonly BookDbContext _dbContext;

        public SearchController(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Метод за автодовършване
        [HttpGet]
        public JsonResult Autocomplete(string term)
        {
            // Игнорираме малки и главни букви, за да направим търсенето нечувствително към регистъра
            var books = _dbContext.Books
                .Where(b => EF.Functions.Like(b.Title.ToLower(), "%" + term.ToLower() + "%")) // Търсене без значение от големи/малки букви
                .Select(b => new { label = b.Title, value = b.BookId })
                .ToList();

            return Json(books);
        }

        // Метод за показване на резултати
        public IActionResult Results(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return View("Results", new List<Book>());
            }

            // Търсене на книги, без значение дали са малки или големи букви
            var books = _dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Where(b => EF.Functions.Like(b.Title.ToLower(), "%" + query.ToLower() + "%") ||
                            EF.Functions.Like(b.Author.Name.ToLower(), "%" + query.ToLower() + "%"))
                .ToList();

            return View("Results", books);
        }

        public IActionResult Search(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                return View(new List<Book>());
            }

            var books = _dbContext.Books
                .Where(b => b.Title.ToLower().Contains(searchQuery.ToLower()) ||
                            b.BookTags.Any(bt => bt.Tag.Name.ToLower().Contains(searchQuery.ToLower())))
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.BookTags)  // Зареждаме BookTags
                    .ThenInclude(bt => bt.Tag)  // Зареждаме Tag през BookTags
                .ToList();

            // Проверка дали има резултати и дали BookTags е правилно зареден
            if (books == null || !books.Any())
            {
                ViewBag.ErrorMessage = "Няма намерени резултати.";
                return View(new List<Book>());
            }

            return View(books);
        }
    }
}
