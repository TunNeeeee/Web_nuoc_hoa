using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_final.Models;
using Web_final.ViewModels;

namespace Web_final.Controllers
{
    public class BlogController : Controller
    {
        private readonly WebsiteNuocHoaContext _context;
        public BlogController(WebsiteNuocHoaContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var bloggs = await _context.Blogs.ToListAsync();
            var viewModel = new BlogViewModel
            {
                Menus = menus,
                Bloggs = bloggs,
            };
            return View(viewModel);
        }
        public async Task<IActionResult> BlogDetail(string slug, long id)
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var bloggs = await _context.Blogs.Where(m => m.Link == slug && m.IdBlog == id).ToListAsync();
            if (bloggs == null)
            {
                var errorViewModel = new ErrorViewModel
                {
                    RequestId = "Product Error",
                };
                return View("Error", errorViewModel);
            }

            var viewModel = new BlogViewModel
            {
                Menus = menus,
                Bloggs = bloggs,
            };
            return View(viewModel);
        }
        public async Task<IActionResult> _MenuPartial()
        {
            return PartialView();
        }
    }
}
