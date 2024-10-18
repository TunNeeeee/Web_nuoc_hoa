using Web_final.Models;

namespace Web_final.ViewModels
{
    public class HomeViewModel
    {
        public List<Menu> Menus { get; set; }
        public List<Slider> Sliders { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Product> ManProds { get; set; }
        public List<Product> WomenProds { get; set; }
        public List<Ph> Phs { get; set; }
       
    }
}
