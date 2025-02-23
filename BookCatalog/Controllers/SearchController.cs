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


        [HttpGet]
        public JsonResult Autocomplete(string term)
        {

            var books = _dbContext.Books
                .Where(b => EF.Functions.Like(b.Title.ToLower(), "%" + term.ToLower() + "%"))
                .Select(b => new { label = b.Title, value = b.BookId })
                .ToList();

            return Json(books);
        }


        public IActionResult Results(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return View("Results", new List<Book>());
            }

            var books = _dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.BookTags)
                    .ThenInclude(bt => bt.Tag)
                .Where(b => EF.Functions.Like(b.Title, $"%{query}%") ||
                            EF.Functions.Like(b.Author.Name, $"%{query}%") ||
                            EF.Functions.Like(b.Genre.Name, $"%{query}%") ||
                            b.BookTags.Any(bt => EF.Functions.Like(bt.Tag.Name, $"%{query}%")))
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
                .Include(b => b.BookTags) 
                    .ThenInclude(bt => bt.Tag)
                .ToList();

         
            if (books == null || !books.Any())
            {
                ViewBag.ErrorMessage = "Няма намерени резултати.";
                return View(new List<Book>());
            }

            return View(books);
        }
    }
}
