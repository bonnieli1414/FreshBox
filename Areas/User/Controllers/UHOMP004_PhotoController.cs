using Microsoft.AspNetCore.Mvc;
using X.PagedList;


namespace FreshBox.Areas.User.Controllers
{
    public class UHOMP004_PhotoController : BaseController
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
            using var datas = new z_repoPhotos(SessionService.SortColumn, SessionService.SortDirection);
            var model = datas.GetDataList(SessionService.SearchText)
                .ToPagedList(id, SessionService.PageSize);
            SessionService.SetPageInfo(id, model.PageCount);
            SessionService.SetActionInfo(enAction.Index, enCardSize.Max, id, "");
            return View(model);
        }

        /// <summary>
        /// 新增/修改
        /// </summary>
        /// <param name="PhotoNo">新增/修改 Key 值</param>
        /// <returns></returns>
        [Area("User")]
        [Login(RoleList = "User")]
        [HttpGet]
        public IActionResult CreateEdit(string PhotoNo)
        {
            SessionService.SetActionInfo(enAction.CreateEdit, enCardSize.Medium);
            using var photos = new z_repoPhotos();
            var model = photos.GetData(PhotoNo);
            if (model == null) return RedirectToAction("Index", ActionService.Controller, new { area = ActionService.Area });
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
        public IActionResult CreateEdit(Photos model)
        {
            if (!ModelState.IsValid) return View(model);
            using var datas = new z_repoPhotos();
            datas.CreateEdit(model);
            // string msg = datas.ErrorMessage;

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
            using var photos = new z_repoPhotos();
            photos.Delete(id);
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
            using var photos = new z_repoPhotos();
            photos.Delete(id);
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

        [Area("User")]
        [HttpGet]
        [Login()]
        public IActionResult PhotoUpload(string PhotoNo)
        {
            using var repoProd = new z_repoPhotos();
            var model = repoProd.GetData(PhotoNo);
            SessionService.StringValue1 = model.PhotoNo;
            ActionService.SetActionData("上傳照片", "", "商品照片上傳");
            return View();
        }

        [Area("User")]
        [HttpPost]
        [Login()]
        public IActionResult PhotoUpload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // 取得目前專案資料夾目錄路徑
                string FileNameOnServer = Directory.GetCurrentDirectory();
                // 專案目錄路徑
                string WebFolder = Path.Combine(FileNameOnServer, "wwwroot\\images\\product");
                string ForderName = Path.Combine(WebFolder, SessionService.StringValue1);
                if (!Directory.Exists(ForderName)) Directory.CreateDirectory(ForderName);

                string FileName = $"{SessionService.StringValue1}.jpg";
                string FilePathName = Path.Combine(ForderName, FileName);
                if (System.IO.File.Exists(FilePathName))
                {
                    string str_file = "";
                    int int_index = 0;
                    while (true)
                    {
                        int_index++;
                        str_file = SessionService.StringValue1 + int_index.ToString().PadLeft(2, '0') + ".jpg";
                        FilePathName = Path.Combine(ForderName, str_file);
                        if (!System.IO.File.Exists(FilePathName)) break;
                    }
                }

                // 建立一個串流物件
                using var stream = System.IO.File.Create(FilePathName);
                // 將檔案寫入到此串流物件中
                file.CopyTo(stream);
            }
            return RedirectToAction("Index", "UORDP003_Product", new { area = "User" });
        }
    }
}