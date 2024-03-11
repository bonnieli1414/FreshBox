using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshBox.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        [HttpGet]
        [Login(RoleList = "Admin")]
        public IActionResult Init()
        {
            SessionService.SetPrgInit();
            return RedirectToAction("Index", ActionService.Controller, new { area = ActionService.Area });
        }

        [Area("Admin")]
        [HttpGet]
        [Login(RoleList = "Admin")]
        public IActionResult Index()
        {
            SessionService.SetProgramInfo("", "儀表板", false, false, 0);
            SessionService.SetActionInfo(enAction.Dashboard, enCardSize.Max);
            return View();
        }
    }
}