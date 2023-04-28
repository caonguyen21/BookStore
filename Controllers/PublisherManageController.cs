using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class PublisherManageController : Controller
    {
        private BookStoreContext db = null;

        public PublisherManageController(BookStoreContext db)
        {
            this.db = db;
        }

        //List view
        public IActionResult List()
        {
            List<Publisher> model = db.Publishers
                .OrderBy(p => p.PublisherId)
                .ToList();

            return View(model);
        }

        //Insert view 
        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        //Create new publisher
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                db.Publishers.Add(publisher);
                db.SaveChanges();
                ViewBag.Message = "Thêm nhà xuất bản thành công";
                ModelState.Clear(); // xóa ModelState để làm sạch dữ liệu trên trang
                return View("Insert", new Publisher()); // trả về View Insert với đối tượng rỗng
            }
            return View(publisher);
        }

        //Detail
        [HttpGet]
        public IActionResult Details(int id)
        {
            var publisher = db.Publishers
                         .FirstOrDefault(p => p.PublisherId == id);

            if (publisher == null)
            {
                return View("pages-error-404");
            }

            return View(publisher);
        }

        // Update view
        [HttpGet]
        public IActionResult Update(int id)
        {
            var publisher = db.Publishers.Find(id);
            if (publisher == null)
            {
                return View("pages-error-404");
            }

            return View(publisher);
        }

        // Update an publisher
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Publisher publisher)
        {
            if (id != publisher.PublisherId)
            {
                return View("pages-error-404");
            }

            if (ModelState.IsValid)
            {
                var publisherToUpdate = db.Publishers.Find(id);
                if (publisherToUpdate == null)
                {
                    return View("pages-error-404");
                }

                publisherToUpdate.Name = publisher.Name;
                publisherToUpdate.Email = publisher.Email;
                publisherToUpdate.Phone = publisher.Phone;
                publisherToUpdate.Address = publisher.Address;
                publisherToUpdate.Status = publisher.Status;

                db.SaveChanges();

                TempData["SuccessMessage"] = "Cập nhật nhà xuất bản thành công";
                return RedirectToAction(nameof(Update), new { id = publisher.PublisherId });
            }

            return View(publisher);
        }

    }
}
