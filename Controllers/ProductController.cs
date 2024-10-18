using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_final.ViewModels;
using Web_final.Models;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Authorization;

namespace WebsiteBanHang.Controllers
{
    public class ProductController : Controller
    {
        private readonly WebsiteNuocHoaContext _context;
        public ProductController(WebsiteNuocHoaContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int page = 1, int id_cat = -1, int price_from = 0, int price_to = -1)
        {
            const int pageSize = 8;
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var prods = await _context.Products
                .Where(m => m.Hide == 0 && ((id_cat != -1 && m.IdCat == id_cat) || id_cat == -1) && m.Price >= price_from && ((price_to != -1 && m.Price <= price_to) || price_to == -1))
                .OrderBy(m => m.Order)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var totalProducts = await _context.Products
               .Where(m => m.Hide == 0 && ((id_cat != -1 && m.IdCat == id_cat) || id_cat == -1) && m.Price >= price_from && ((price_to != -1 && m.Price <= price_to) || price_to == -1))
               .CountAsync();
            var viewModel = new ProductViewModel
            {
                Menus = menus,
                Prods = prods,
                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = totalProducts
                },
                ProductFilter = new ProductFilter
                {
                    id_cat = id_cat,
                    price_from = price_from,
                    price_to = price_to,
                }
            };
            return View(viewModel);
        }
        public async Task<IActionResult> CateProd(string slug, long id, int page = 1)
        {
            const int pageSize = 8;

            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m =>
                m.Order).ToListAsync();
            var cateProds = await _context.Catologies
                .Where(cp => cp.IdCat == id && cp.Link == slug).FirstOrDefaultAsync();
            if (cateProds == null)
            {
                var errorViewModel = new ErrorViewModel
                {
                    RequestId = "CateProd Error",
                };
                return View("Error", errorViewModel);
            }

            var prods = await _context.Products
                .Where(m => m.Hide == 0 && m.IdCat == cateProds.IdCat || m.IdCat == 3)
                .OrderBy(m => m.Order)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalProducts = await _context.Products
                .Where(m => m.Hide == 0 && m.IdCat == cateProds.IdCat || m.IdCat == 3)
                .CountAsync();

            var viewModel = new ProductViewModel
            {
                Menus = menus,
                Prods = prods,
                cateName = cateProds.NameCat,
                PaginationInfo = new PaginationInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = totalProducts
                }
            };

            return View(viewModel);
        }
        public async Task<IActionResult> FindProduct(string name)
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var prods = await _context.Products.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            if (!string.IsNullOrEmpty(name))
            {
                prods = await _context.Products.Where(p => p.NamePro.Contains(name) && p.Hide == 0).ToListAsync();
            }
            var viewModel = new ProductViewModel
            {
                Menus = menus,
                Prods = prods,
            };
            ViewBag.SearchName = name; // Truyền tên sản phẩm vào view
            return View("FindProduct", viewModel); // Trả về view "FindProduct" với dữ liệu viewModel
        }

        public async Task<IActionResult> ProdDetail(string slug, long id)
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var prods = await _context.Products.Where(m => m.Link == slug && m.IdPro == id).ToListAsync();
            if (prods == null)
            {
                var errorViewModel = new ErrorViewModel
                {
                    RequestId = "Product Error",
                };
                return View("Error", errorViewModel);
            }
            var viewModel = new ProductViewModel
            {
                Menus = menus,
                Prods = prods,
            };
            return View(viewModel);
        }
        public async Task<IActionResult> _MenuPartial()
        {
            return PartialView();
        }
        public async Task<IActionResult> _BlogPartial()
        {
            return PartialView();
        }
    }
}