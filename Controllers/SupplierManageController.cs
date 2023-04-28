using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class SupplierManageController : Controller
    {
        private BookStoreContext db = null;

        public SupplierManageController(BookStoreContext db)
        {
            this.db = db;
        }

        //List view
        public IActionResult List()
        {
            List<Supplier> model = db.Suppliers
                .OrderBy(s => s.SupplierId)
                .ToList();

            return View(model);
        }

        //Insert view 
        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }

        //Create new supplier
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Insert(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(supplier);
                db.SaveChanges();
                ViewBag.Message = "Thêm nhà cung cấp thành công";
                ModelState.Clear(); // xóa ModelState để làm sạch dữ liệu trên trang
                return View("Insert", new Supplier()); // trả về View Insert với đối tượng rỗng
            }
            return View(supplier);
        }

        //Detail
        [HttpGet]
        public IActionResult Details(int id)
        {
            var supplier = db.Suppliers
                         .FirstOrDefault(s => s.SupplierId == id);

            if (supplier == null)
            {
                return View("pages-error-404");
            }

            return View(supplier);
        }

        // Update view
        [HttpGet]
        public IActionResult Update(int id)
        {
            var supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return View("pages-error-404");
            }

            return View(supplier);
        }

        // Update an supplier
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Supplier supplier)
        {
            if (id != supplier.SupplierId)
            {
                return View("pages-error-404");
            }

            if (ModelState.IsValid)
            {
                var supplierToUpdate = db.Suppliers.Find(id);
                if (supplierToUpdate == null)
                {
                    return View("pages-error-404");
                }

                supplierToUpdate.Name = supplier.Name;
                supplierToUpdate.Email = supplier.Email;
                supplierToUpdate.Status = supplier.Status;
                supplierToUpdate.Address = supplier.Address;
                supplierToUpdate.Phone = supplier.Phone;

                db.SaveChanges();

                TempData["SuccessMessage"] = "Cập nhật tác giả thành công";
                return RedirectToAction(nameof(Update), new { id = supplier.SupplierId });
            }

            return View(supplier);
        }
    }
}
