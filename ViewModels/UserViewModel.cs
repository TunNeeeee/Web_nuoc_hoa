using Web_final.Models;

namespace Web_final.ViewModels
{
    public class UserViewModel
    {
        public List<Menu> Menus { get; set; }
        public User Register { get; set; }
        public UserViewModel()
        {
            Register = new User();
        }
    }
}
