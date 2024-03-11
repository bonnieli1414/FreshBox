using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshBox.Areas.Member.Controllers
{
    public class HomeController : Controller
    {
        [Area("Member")]
        [HttpGet]
        [Login(RoleList = "Member")]
        public IActionResult Init()
        {
            SessionService.SetPrgInit();
            return RedirectToAction("Index", ActionService.Controller, new { area = ActionService.Area });
        }

        [Area("Member")]
        [HttpGet]
        [Login(RoleList = "Member")]
        public IActionResult Index()
        {
            SessionService.SetProgramInfo("", "儀表板", false, false, 0);
            SessionService.SetActionInfo(enAction.Dashboard, enCardSize.Max);
            return View();
        }
    }
}