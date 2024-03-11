using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FreshBox_v3.Areas.User.Controllers
{
    public class UORDP004_PromotionController : BaseController
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
        /// 資料列表
        /// </summary>
        /// <param name="id">目前頁數</param>
        /// <returns></returns>
        [Area("User")]
        [Login(RoleList = "User")]
        [HttpGet]
        public IActionResult Index(int id = 1)
        {
            using var datas = new z_repoPromotions(SessionService.SortColumn, SessionService.SortDirection);
            var model = datas.GetDataList(SessionService.SearchText)
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

            var model = new Promotions();
            SessionService.SetActionInfo(enAction.CreateEdit, enCardSize.Medium, id);
            if (id == 0)
            {
                //新增預設值
            }
            else
            {
                //修改資料
                using var promotions = new z_repoPromotions();
                model = promotions.GetData(id);

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
        public IActionResult CreateEdit(Promotions model)
        {
            ModelState.Remove("ProdImage");
            if (!ModelState.IsValid) return View(model);
            using var promotions = new z_repoPromotions();
            promotions.CreateEdit(model);
            TempData["SuccessMessage"] = (model.Id == 0) ? "新增資料成功!!" : "修改資料成功!!";
            return RedirectToAction("Index", ActionService.Controller, new { area = ActionService.Area });
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="id">記錄 ID</param>
        /// <returns></returns>
        [Area("User")]
        [Login(RoleList = "User")]
        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            using var datas = new z_repoPromotions();
            datas.Delete(id);
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
            using var datas = new z_repoPromotions();
            datas.Delete(id);
            dmJsonMessage result = new dmJsonMessage() { Mode = true, Message = "資料已刪除!!" };
            return Json(result);
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