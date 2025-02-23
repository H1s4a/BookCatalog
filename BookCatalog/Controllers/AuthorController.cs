using Microsoft.AspNetCore.Mvc;
using BookCatalog.Data;
using BookCatalog.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Controllers
{
    public class AuthorController : Controller
    {
        private readonly BookDbContext _dbContext;

        public AuthorController(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Author
        public ActionResult Index()
        {
            var authors = _dbContext.Authors.ToList();
            return View(authors);
        }

        // GET: Author/Details/5
        public ActionResult Details(int id)
        {
            var author = _dbContext.Authors
                .Include(a => a.Books)
                .FirstOrDefault(a => a.AuthorId == id);

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            try
            {
                _dbContext.Authors.Add(author);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Author/Edit/5
        public ActionResult Edit(int id)
        {
            var author = _dbContext.Authors.FirstOrDefault(a => a.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Author/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author updatedAuthor)
        {
            try
            {
                var author = _dbContext.Authors.FirstOrDefault(a => a.AuthorId == id);
                if (author == null)
                {
                    return NotFound();
                }

                author.Name = updatedAuthor.Name;
                author.ImagePath = updatedAuthor.ImagePath;
                author.Bio = updatedAuthor.Bio;

                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Author/Delete/5
        public ActionResult Delete(int id)
        {
            var author = _dbContext.Authors.FirstOrDefault(a => a.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Author/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var author = _dbContext.Authors.FirstOrDefault(a => a.AuthorId == id);
            if (author == null)
            {
                return NotFound();
            }

            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}