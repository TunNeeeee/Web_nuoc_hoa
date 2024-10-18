using Web_final.Models;
using WebsiteBanHang.Controllers;

namespace Web_final.ViewModels
{
    public class ProductViewModel
    {
        public List<Menu> Menus { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Product> Prods { get; set; }
        public String cateName { get; set; }
        internal PaginationInfo PaginationInfo { get; set; }
        internal ProductFilter ProductFilter { get; set; }
    }
}
