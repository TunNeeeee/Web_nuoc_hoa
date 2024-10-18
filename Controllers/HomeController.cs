using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Web_final.Models;
using Web_final.ViewModels;

namespace Web_final.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebsiteNuocHoaContext _context;
        public HomeController(WebsiteNuocHoaContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m =>m.Order).ToListAsync();
            var slides = await _context.Sliders.Where(m => m.Hide == 0).OrderBy(m =>m.Order).ToListAsync();
            var blogs = await _context.Blogs.Where(m => m.Hide == 0).OrderBy(m =>m.Order).Take(1).ToListAsync();
            var man_prods = await _context.Products.Where(m => m.Hide == 0 && m.IdCat == 1 || m.IdCat == 3).OrderBy(m => m.Order).Take(4).ToListAsync();
            var women_prods = await _context.Products.Where(m => m.Hide == 0 && m.IdCat == 2 || m.IdCat == 3).OrderBy(m => m.Order).Take(4).ToListAsync();
            var users = await _context.Users.ToListAsync();
            var phs = await _context.Phs.Where(m => m.Hide == 0).Take(3).ToListAsync();
            foreach (var ph in phs)
            {
                var user = users.FirstOrDefault(u => u.IdUsers == ph.IdUsers);
                if (user != null)
                {
                    ph.UsersName = user.Name;
                    ph.img = user.Img;
                }
            }
            var viewModel = new HomeViewModel
            {
                Menus = menus,
                Sliders = slides,
                Blogs = blogs,
                ManProds = man_prods,
                WomenProds = women_prods,
                Phs = phs,
            };
            return View(viewModel);
        }
        public async Task<IActionResult> _MenuPartial()
        {
            return PartialView();
        }
        public async Task<IActionResult> _SlidePartial()
        {
            return PartialView();
        }
        public async Task<IActionResult> _ProductPartial()
        {
            return PartialView();
        }
        public async Task<IActionResult> _BlogPartial()
        {
            return PartialView();
        }
        public async Task<IActionResult> _PHPartial()
        {
            return PartialView();
        }
        public async Task<IActionResult> _LHPartial()
        {
            return PartialView();
        }
    }
}
