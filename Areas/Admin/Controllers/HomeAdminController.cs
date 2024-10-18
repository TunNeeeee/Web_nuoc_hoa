using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Web_final.Models;
using Web_final.ViewModels;

namespace Web_final.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("homeadmin")]
    [Authorize(Roles = "1")]
    public class HomeAdminController : Controller
    {
        WebsiteNuocHoaContext db = new WebsiteNuocHoaContext();
        private async Task<string> SaveImageProd(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/images/product/", image.FileName); // Thay đổi đường dẫn theo cấu hình của bạn
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return image.FileName; // Trả về đường dẫn tương đối
        }
        private async Task<string> SaveImageUser(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/images/KH/", image.FileName); // Thay đổi đường dẫn theo cấu hình của bạn
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return image.FileName; // Trả về đường dẫn tương đối
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("manageproduct")]
        public IActionResult DanhMucSP()
        {
            var lstSanPham = db.Products.ToList();
            return View(lstSanPham);    
        }
        
        [Route("AddProd")]
        [HttpGet]
        public async Task<IActionResult> AddProd()
        {
            ViewBag.IdCat = new SelectList( await db.Catologies.ToListAsync(),"IdCat","NameCat");
            return View();
        }
        [Route("AddProd")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProd(Product product, IFormFile img1,IFormFile img2, IFormFile img3)
        {
            if (ModelState.IsValid)
            {
                if (img1 != null)
                {
                    // Lưu hình ảnh đại diện
                    product.Img1 = await SaveImageProd(img1);
                }
                if (img2 != null)
                {
                    product.Img2 = await SaveImageProd(img2);
                }
                if (img3 != null)
                {
                    product.Img3 = await SaveImageProd(img3);
                }

                _ = db.Products.AddAsync(product);
                await db.SaveChangesAsync();
                return RedirectToAction("DanhMucSP", "HomeAdmin");
            }
            return View(product);
        }
        [Route("DisplayProd")]
        public async Task<IActionResult> DisplayProd(int id)
        {
            var product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        // Show the product update form
        [Route("UpdateProd")]
        [HttpGet]
        public async Task<IActionResult> UpdateProd(int id)
        {
            ViewBag.IdCat = new SelectList( await db.Catologies.ToListAsync(), "IdCat", "NameCat");
            var product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        // Process the product update
        [Route("UpdateProd")]
        [HttpPost]
        public async Task<IActionResult> UpdateProd(int idpro, Product product, IFormFile img1,
        IFormFile img2, IFormFile img3)
        {
            if (idpro != product.IdPro)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (product.Img1 != null || product.Img1 != img1.ToString())
                {
                    // Lưu hình ảnh đại diện
                    product.Img1 = await SaveImageProd(img1);
                }
                if (product.Img2 != null || product.Img2 != img2.ToString())
                {
                    // Lưu hình ảnh đại diện
                    product.Img2 = await SaveImageProd(img2);
                }
                if (product.Img3 != null || product.Img3 != img3.ToString())
                {
                    // Lưu hình ảnh đại diện
                    product.Img3 = await SaveImageProd(img3);
                }
                _ = db.Products.Update(product);
                await db.SaveChangesAsync();
                return RedirectToAction("DanhMucSP", "HomeAdmin");
            }

            return View(product);
        }
        // Show the product delete confirmation
        [Route("deleteprod")]
        public IActionResult DeleteProd(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        // Process the product deletion
        [HttpPost]
        [Route("deleteprod")]
        public IActionResult DeleteProd(int idpro, Product product)
        {
            if (idpro != product.IdPro)
            {
                return NotFound();
            }
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("DanhMucSP", "HomeAdmin");
        }

        [Route("manageuser")]
        public IActionResult QLUsers()
        {
            var listuser = db.Users.ToList();
            return View(listuser);
        }

        [Route("AddUser")]
        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            return View();
        }
        [Route("AddUser")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(User user, IFormFile img)
        {
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    // Lưu hình ảnh đại diện
                    user.Img = await SaveImageUser(img);
                }
                var existingUser = await db.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
                if (existingUser != null)
                {
                    ViewBag.ErrorMessage = "Tên đăng nhập đã tồn tại.";
                    return View(user);
                }
                user.Password =BCrypt.Net.BCrypt.HashPassword(user.Password);

                _ = db.Users.AddAsync(user);
                await db.SaveChangesAsync();
                return RedirectToAction("ManageUser", "HomeAdmin");
            }
            return View(user);
        }
        [Route("DisplayUser")]
        public async Task<IActionResult> DisplayUser(int id)
        {
            var user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        // Show the user update form
        [Route("UpdateUser")]
        [HttpGet]
        public async Task<IActionResult> UpdateUser(int id)
        {
            var user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        // Process the user update
        [Route("UpdateUser")]
        [HttpPost]
        public async Task<IActionResult> UpdateUser(int idusers, User user, IFormFile img)
        {
            if (idusers != user.IdUsers)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (user.Img != null || user.Img != img.ToString())
                {
                    // Lưu hình ảnh đại diện
                    user.Img = await SaveImageUser(img);
                }
                if (user.Password != null)
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                }          
                _ = db.Users.Update(user);
                await db.SaveChangesAsync();
                return RedirectToAction("ManageUser", "HomeAdmin");
            }

            return View(user);
        }
        // Show the user delete confirmation
        [Route("deleteuser")]
        public IActionResult DeleteUser(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        // Process the user deletion
        [HttpPost]
        [Route("deleteuser")]
        public IActionResult DeleteUser(int idusers, User user)
        {
            if (idusers != user.IdUsers)
            {
                return NotFound();
            }
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("ManageUser", "HomeAdmin");
        }
        [Route("manageoder")]
        public IActionResult QLOder()
        {
            var cart = db.Carts.Include(c => c.IdUsersNavigation).Include(c => c.CartDetails).ThenInclude(cd => cd.IdProNavigation).ToList();
            return View(cart) ;
        }
        [Route("managePH")]
        public IActionResult QLPH()
        {
            var lstPH = db.Phs.Include(c=>c.IdUsersNavigation).ToList();
            return View(lstPH);
        }
    }
}

