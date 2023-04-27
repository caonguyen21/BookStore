using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AuthorManageController : Controller
    {
        private BookStoreContext db = null;

        public AuthorManageController(BookStoreContext db)
        {
            this.db = db;
        }

        //List view
        public IActionResult List()
        {
            List<Author> model = db.Authors
                .OrderBy(a => a.AuthorId)
                .ToList();

            return View(model);
        }

        //Insert view 
        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        //Create new author
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(Author author)
        {
            if (ModelState.IsValid)
            {
                db.Authors.Add(author);
                db.SaveChanges();
                ViewBag.Message = "Thêm tác giả thành công";
                ModelState.Clear(); // xóa ModelState để làm sạch dữ liệu trên trang
                return View("Insert", new Author()); // trả về View Insert với đối tượng rỗng
            }
            return View(author);
        }

        //Detail
        [HttpGet]
        public IActionResult Details(int id)
        {
            var author = db.Authors
                         .FirstOrDefault(a => a.AuthorId == id);

            if (author == null)
            {
                return View("pages-error-404");
            }

            return View(author);
        }

        // Update view
        [HttpGet]
        public IActionResult Update(int id)
        {
            var author = db.Authors.Find(id);
            if (author == null)
            {
                return View("pages-error-404");
            }

            return View(author);
        }

        // Update an author
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Author author)
        {
            if (id != author.AuthorId)
            {
                return View("pages-error-404");
            }

            if (ModelState.IsValid)
            {
                var authorToUpdate = db.Authors.Find(id);
                if (authorToUpdate == null)
                {
                    return View("pages-error-404");
                }

                authorToUpdate.Name = author.Name;
                authorToUpdate.Email = author.Email;
                authorToUpdate.Status = author.Status;

                db.SaveChanges();

                TempData["SuccessMessage"] = "Cập nhật tác giả thành công";
                return RedirectToAction(nameof(Update), new { id = author.AuthorId });
            }

            return View(author);
        }
    }
}
