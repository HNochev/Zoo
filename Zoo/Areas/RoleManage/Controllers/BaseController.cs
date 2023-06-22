using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zoo.Core.Constants;

namespace Zoo.Areas.RoleManage.Controllers
{
    [Authorize(Roles = UserConstants.Administrator)]
    [Area("RoleManage")]
    public class BaseController : Controller
    {

    }
}
