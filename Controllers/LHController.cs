using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_final.Models;
using Web_final.ViewModels;

namespace Web_final.Controllers
{
    public class LHController : Controller
    {
        private readonly WebsiteNuocHoaContext _context;
        public LHController(WebsiteNuocHoaContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var lhs = await _context.Lhs.ToListAsync();
            var viewModel = new LHViewModel
            {
                Menus = menus,
                Lhs = lhs,
            };
            return View(viewModel);
        }
        public async Task<IActionResult> _MenuPartial()
        {
            return PartialView();
        }
    }
}
