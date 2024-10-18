using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Web_final.Models;
using Web_final.ViewModels;

namespace Web_final.Controllers
{
    public class PHController : Controller
    {
        private readonly WebsiteNuocHoaContext _context;
        public PHController(WebsiteNuocHoaContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var phs = await _context.Phs.Where(m => m.Hide == 0).ToListAsync();
            var users = await _context.Users.ToListAsync();
            foreach (var ph in phs)
            {
                var user = users.FirstOrDefault(u => u.IdUsers == ph.IdUsers);
                if (user != null)
                {
                    ph.UsersName = user.Name;
                    ph.img = user.Img;
                }
            }
            var viewModel = new PHViewModel
            {
                Menus = menus,
                Phs = phs,
                Users = users,
            };
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> SendPh()
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var users = await _context.Users.Where(m => m.Hide == 0).ToListAsync();
            var phs = new PHViewModel
            {
                Menus = menus,
                Users = users,
                Phs = await _context.Phs.ToListAsync()
            };
            return View(phs); // Truyền phs vào view
        }

        [HttpPost]
        public async Task<IActionResult> SendPh(PHViewModel model)
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var users = await _context.Users.Where(m => m.Hide == 0).ToListAsync();
            var phs = new PHViewModel
            {
                Menus = menus,
                Users = users,
                Phs = await _context.Phs.ToListAsync() // Populate Phs with the existing Phs data
            };
            if (model.Phs != null)
            {
                if (ModelState.IsValid)
                {
                    var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (int.TryParse(userIdString, out int userId))
                    {

                        var pha = new Ph
                        {
                            ID_PH = await _context.Phs.CountAsync() + 1,
                            IdUsers = userId,
                            NdPh = model.PhanHoi.NdPh
                        };
                        _context.Phs.Add(pha);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }
            }
            // Nếu ModelState không hợp lệ, truy cập lại dữ liệu cần thiết và trả về view
            return View(phs); // Truyền phs vào view
        }
        public async Task<IActionResult> _MenuPartial()
        {
            return PartialView();
        }
    }
}
