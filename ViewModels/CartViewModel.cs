using Web_final.Models;

namespace Web_final.ViewModels
{
    public class CartViewModel
    {
        public List<Menu> Menus { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
