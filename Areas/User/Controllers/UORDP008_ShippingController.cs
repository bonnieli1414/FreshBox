using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FreshBox.Areas_User_Controllers
{
    public class UORDP008_ShippingController : Controller
    {
        /// <summary>
        /// 資料初始化事件
        /// </summary>
        /// <returns></returns>
        [Area("User")]
        [Login(RoleList = "User")]
        [HttpGet]
        public IActionResult Init()
        {
            SessionService.SetPrgInit();
            return RedirectToAction("Index", ActionService.Controller, new { area = ActionService.Area });
        }

        /// <summary>
        /// 資料列表 name="searchText">查詢條件
        /// </summary>
        /// <param name="id">目前頁數</param>
        /// <returns></returns>
        [Area("User")]
        [Login(RoleList = "User")]
        [HttpGet]
        public IActionResult Index(int id = 1)
        {
            using var Shippings = new z_repoShippings(SessionService.SortColumn, SessionService.SortDirection);
            var model = Shippings.GetDataList(SessionService.SearchText)
                .ToPagedList(id, SessionService.PageSize);
            SessionService.SetPageInfo(id, model.PageCount);
            SessionService.SetActionInfo(enAction.Index, enCardSize.Max, id, "");
            return View(model);
        }

        /// <summary>
        /// 新增/修改
        /// </summary>
        /// <param name="id">新增/修改 Key 值</param>
        /// <returns></returns>
        [Area("User")]
        [Login(RoleList = "User")]
        [HttpGet]
        public IActionResult CreateEdit(int id = 0)
        {
            SessionService.SetActionInfo(enAction.CreateEdit, enCardSize.Medium);
            var model = new Shippings();
            if (id == 0)
            {
                //新增預設值
            }
            else
            {
                //修改資料
                using var Shippings = new z_repoShippings();
                model = Shippings.GetData(id);
                if (model == null) return RedirectToAction("Index", ActionService.Controller, new { area = ActionService.Area });
            }
            return View(model);
        }

        /// <summary>
        /// 新增/修改
        /// </summary>
        /// <param name="model">新增/修改資料</param>
        /// <returns></returns>
        [Area("User")]
        [Login(RoleList = "User")]
        [HttpPost]
        public IActionResult CreateEdit(Shippings model)
        {
            if (!ModelState.IsValid) return View(model);
            using var Shippings = new z_repoShippings();
            Shippings.CreateEdit(model);
            return RedirectToAction("Index", ActionService.Controller, new { area = ActionService.Area });
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="id">刪除 Key 值</param>
        /// <returns></returns>
        [Area("User")]
        [Login(RoleList = "User")]
        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            using var Shippings = new z_repoShippings();
            Shippings.Delete(id);
            return RedirectToAction("Index", ActionService.Controller, new { area = ActionService.Area });
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="id">記錄 ID</param>
        /// <returns></returns>
        [Area("User")]
        [Login(RoleList = "User")]
        [HttpPost]
        public JsonResult DeleteRow(int id = 0)
        {
            //檢查刪除權限
            //if (!PrgService.IsProgramSecurity(enSecurtyMode.Delete))
            //return RedirectToAction(ActionService.Index, ActionService.Controller, new { area = ActionService.Area });

            using (z_repoShippings Shippings = new z_repoShippings())
            {
                Shippings.Delete(id);
                dmJsonMessage result = new dmJsonMessage() { Mode = true, Message = "資料已刪除!!" };
                return Json(result);
            }
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <returns></returns>
        [Area("User")]
        [Login(RoleList = "User")]
        [HttpPost]
        public IActionResult Search()
        {
            object obj_text = Request.Form["SearchText"];
            string str_text = (obj_text == null) ? string.Empty : obj_text.ToString();
            SessionService.SearchText = str_text;
            return RedirectToAction("Index", ActionService.Controller, new { area = ActionService.Area });
        }

        /// <summary>
        /// 欄位排序
        /// </summary>
        /// <param name="id">欄位名稱</param>
        /// <returns></returns>
        [Area("User")]
        [Login(RoleList = "User")]
        [HttpGet]
        public IActionResult Sort(string id)
        {
            if (SessionService.SortColumn == id)
            {
                SessionService.SortDirection = (SessionService.SortDirection == "asc") ? "desc" : "asc";
            }
            else
            {
                SessionService.SortColumn = id;
                SessionService.SortDirection = "asc";
            }
            return RedirectToAction("Index", ActionService.Controller, new { area = ActionService.Area });
        }
    }
}