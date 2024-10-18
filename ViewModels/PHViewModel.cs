using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Web_final.Models;

namespace Web_final.ViewModels
{
    public class PHViewModel : Controller
    {
        public List<Menu> Menus { get; set; }
        public List<Ph> Phs { get; set; }
        public List<User> Users { get;  set; }
        public Ph PhanHoi { get; set; }
        public PHViewModel()
        {
            PhanHoi = new Ph();
        }
    }
}
