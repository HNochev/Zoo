using Microsoft.AspNetCore.Mvc;

namespace Zoo.Areas.RoleManage.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
