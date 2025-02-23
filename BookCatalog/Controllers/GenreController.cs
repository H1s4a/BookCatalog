using BookCatalog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BookCatalog.Controllers
{
    public class GenreController : Controller
    {
        private readonly BookDbContext _dbContext;

        public GenreController(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Index()
        {
            var genres = _dbContext.Genres.ToList();
            return View(genres);
        }
        public ActionResult Details(int id)
        {
            var genre = _dbContext.Genres
                .Include(g => g.Books)
                .ThenInclude(b => b.Author)
                .FirstOrDefault(g => g.GenreId == id);

            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
