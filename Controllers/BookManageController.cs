using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    public class BookManageController : Controller
    {
        private BookStoreContext db = null;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookManageController(BookStoreContext db, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        //Main screem
        public IActionResult index()
        {
            return View();
        }

        //List view
        public IActionResult List()
        {
            List<Book> model = db.Books
                .OrderBy(b => b.BookId)
                .ToList();

            return View(model);
        }

        //Insert view 
        [HttpGet]
        public IActionResult Insert()
        {
            FillAuthors();
            FillPublishers();
            FillSuppliers();
            return View();
        }

        //Create a new book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(Book book, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.Length > 0)
                {
                    var fileName = Path.GetFileName(Image.FileName);
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }
                    book.Image = "images\\" + fileName;
                }
                db.Books.Add(book);
                db.SaveChanges();
                ViewBag.Message = "Thêm sách thành công";
                ModelState.Clear(); // xóa ModelState để làm sạch dữ liệu trên trang
                FillAuthors();
                FillPublishers();
                FillSuppliers();
                return View("Insert", new Book()); // trả về View Insert với đối tượng rỗng
            }
            FillAuthors();
            FillPublishers();
            FillSuppliers();
            return View(book);
        }

        //Find all author
        private void FillAuthors()
        {
            List<SelectListItem> listOfAuthors = (from a in db.Authors
                                                  select new SelectListItem()
                                                  {
                                                      Text = a.Name,
                                                      Value = a.AuthorId.ToString()
                                                  }).ToList();
            ViewBag.Authors = listOfAuthors;
        }

        //Find all publisher
        private void FillPublishers()
        {
            List<SelectListItem> listOfPublishers = (from p in db.Publishers
                                                     select new SelectListItem()
                                                     {
                                                         Text = p.Name,
                                                         Value = p.PublisherId.ToString()
                                                     }).ToList();
            ViewBag.Publishers = listOfPublishers;
        }

        //Find all supplier
        private void FillSuppliers()
        {
            List<SelectListItem> listOfSuppliers = (from s in db.Suppliers
                                                    select new SelectListItem()
                                                    {
                                                        Text = s.Name,
                                                        Value = s.SupplierId.ToString()
                                                    }).ToList();
            ViewBag.Suppliers = listOfSuppliers;
        }

        //Update view
        [HttpGet]
        public IActionResult Update(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            FillAuthors();
            FillPublishers();
            FillSuppliers();

            return View(book);
        }

        //Update a book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            // Find the book in the database and attach it to the context
            Book dbBook = db.Books.Find(id);
            db.Books.Attach(dbBook);

            // Update the book with the values from the form
            dbBook.Title = book.Title;
            dbBook.AuthorId = book.AuthorId;
            dbBook.PublisherId = book.PublisherId;
            dbBook.SupplierId = book.SupplierId;
            dbBook.Price = book.Price;
            dbBook.Status = book.Status;

            if (ModelState.IsValid)
            {
                db.SaveChanges();

                TempData["SuccessMessage"] = "Cập nhật sách thành công";
                return RedirectToAction("Update", new { id = book.BookId });
            }

            FillAuthors();
            FillPublishers();
            FillSuppliers();

            return View(book);
        }

        //Update image view
        [HttpGet]
        public IActionResult UpdateImage(int id)
        {
            var book = db.Books.FirstOrDefault(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        //Update the image view
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateImage(int id, IFormFile Image)
        {
            var book = db.Books.FirstOrDefault(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (Image != null && Image.Length > 0)
                {
                    // upload and save new image
                    var fileName = Path.GetFileName(Image.FileName);
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }
                    book.Image = "images\\" + fileName;

                    db.Entry(book).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            TempData["SuccessMessage"] = "Cập nhật ảnh bìa thành công";
            return RedirectToAction("Update", new { id = book.BookId });
        }

        //Update the image of the book
        [HttpGet]
        public IActionResult Details(int id)
        {
            var book = db.Books
                         .Include(b => b.Author)
                         .Include(b => b.Publisher)
                         .Include(b => b.Supplier)
                         .FirstOrDefault(b => b.BookId == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
    }
}
