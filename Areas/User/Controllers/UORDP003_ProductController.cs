using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FreshBox.Areas.User.Controllers
{
    public class UORDP003_ProductController : BaseController
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
            using var datas = new z_repoProducts(SessionService.SortColumn, SessionService.SortDirection);
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
            SessionService.SetActionInfo(enAction.CreateEdit, enCardSize.Medium);
            var model = new Products();
            if (id == 0)
            {
                //新增預設值
                model.IsEnabled = true;
                model.IsShowPhoto = true;
            }
            else
            {
                //修改資料
                using var Products = new z_repoProducts();
                model = Products.GetData(id);
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
        public IActionResult CreateEdit(Products model)
        {
            ModelState.Remove("ProdImage");
            if (!ModelState.IsValid) return View(model);
            using var products = new z_repoProducts();
            products.CreateEdit(model);
            if (products.ErrorMessage == "商品編號重複")
            {
                TempData["ErrorMessage"] = "失敗，原因：" + products.ErrorMessage;
            }
            else
            {
                TempData["SuccessMessage"] = (model.Id == 0) ? "新增資料成功!!" : "修改資料成功!!";
            }

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
            using var datas = new z_repoProducts();
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
            using var datas = new z_repoProducts();
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
                string ForderName = "";
                string FileName = "";
                string FilePathName = "";

                if (SessionService.StringValue1.Length > 4)
                {
                    // 非封面圖片的路徑組合
                    // 取前四個字元
                    string part1 = SessionService.StringValue1.Substring(0, 4);
                    // 前四個字元加上張數第01~09
                    string part2 = SessionService.StringValue1;
                    SessionService.StringValue1 = Path.Combine(part1, part2);
                    ForderName = WebFolder;
                    if (!Directory.Exists(ForderName)) Directory.CreateDirectory(ForderName);
                    FileName = $"{SessionService.StringValue1}.jpg";
                    FilePathName = Path.Combine(ForderName, FileName);
                }
                else
                {
                    // 封面圖片的路徑組合
                    ForderName = Path.Combine(WebFolder, SessionService.StringValue1);
                    if (!Directory.Exists(ForderName)) Directory.CreateDirectory(ForderName);
                    FileName = $"{SessionService.StringValue1}.jpg";
                    FilePathName = Path.Combine(ForderName, FileName);
                }

                // 檢查是否已經存在相同檔案，如果存在則更新
                if (System.IO.File.Exists(FilePathName))
                {
                    // 如果已經存在相同的檔案，先刪除舊的圖片
                    System.IO.File.Delete(FilePathName);
                }
                else
                {
                    // 如果不存在相同的檔案，則建立新圖片
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

        [Area("User")]
        [HttpGet]
        [Login()]
        public IActionResult AddPhotoUpload(string PhotoNo)
        {
            using var repoProd = new z_repoPhotos();
            SessionService.StringValue1 = PhotoNo;
            ActionService.SetActionData("上傳照片", "", "商品照片上傳");
            return View();
        }

        [Area("User")]
        [HttpPost]
        [Login()]
        public IActionResult AddPhotoUpload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // 取得目前專案資料夾目錄路徑
                string FileNameOnServer = Directory.GetCurrentDirectory();
                // 專案目錄路徑
                string WebFolder = Path.Combine(FileNameOnServer, "wwwroot\\images\\product");
                string ForderName = "";
                string FileName = "";
                string FilePathName = "";
                // 取前四個字元
                string prodNo = SessionService.StringValue1.Substring(0, 4);
                string photoNo = "";
                if (SessionService.StringValue1.Length > 4)
                {
                    // 取前剩餘字元
                    photoNo = SessionService.StringValue1.Substring(4);
                    int part2Int = Int32.Parse(photoNo) + 1;
                    photoNo = part2Int.ToString().PadLeft(2, '0'); ;
                    // 前四個字元加上張數第01~09
                    photoNo = prodNo + photoNo;
                }
                else
                {
                    // 前四個字元加上剩餘字元設定為01
                    photoNo = prodNo + "01";
                }
                SessionService.StringValue1 = Path.Combine(prodNo, photoNo);
                ForderName = WebFolder;
                if (!Directory.Exists(ForderName)) Directory.CreateDirectory(ForderName);
                FileName = $"{SessionService.StringValue1}.jpg";
                FilePathName = Path.Combine(ForderName, FileName);

                // 檢查是否已經存在相同檔案，如果存在則更新
                if (System.IO.File.Exists(FilePathName))
                {
                    // 如果已經存在相同的檔案，先刪除舊的圖片
                    System.IO.File.Delete(FilePathName);
                }
                else
                {
                    // 如果不存在相同的檔案，則建立新圖片
                    string str_file = "";
                    int int_index = 0;
                    while (true)
                    {
                        int_index++;
                        str_file = SessionService.StringValue1 + ".jpg";
                        FilePathName = Path.Combine(ForderName, str_file);
                        if (!System.IO.File.Exists(FilePathName)) break;
                    }
                }

                // 建立一個串流物件
                using var stream = System.IO.File.Create(FilePathName);
                // 將檔案寫入到此串流物件中
                file.CopyTo(stream);
                var photoDatas = new Photos();
                using var products = new z_repoProducts();
                var productsDatas = products.GetData(prodNo);
                photoDatas.CodeNo = productsDatas.CategoryNo;
                photoDatas.PhotoNo = photoNo;
                photoDatas.ProdNo = productsDatas.ProdNo;
                photoDatas.FolderNo = productsDatas.CategoryNo;
                var photo = new z_repoPhotos();
                photo.CreateEdit(photoDatas);
            }
            return RedirectToAction("Index", "UORDP003_Product", new { area = "User" });
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="id">傳入的值是photoNo</param>
        /// <returns></returns>
        [Area("User")]
        [Login(RoleList = "User")]
        [HttpPost]
        public JsonResult DeletePhoto(string id)
        {
            // 刪除資料夾內的資料
            string FileNameOnServer = Directory.GetCurrentDirectory();
            string WebFolder = Path.Combine(FileNameOnServer, "wwwroot\\images\\product");
            string ForderName = id.Substring(0, 4);
            string FileName = id + ".jpg";
            string FilePathName = Path.Combine(ForderName, FileName);
            FilePathName = Path.Combine(WebFolder, FilePathName);

            // 檢查是否已經存在，如果存在則刪除
            if (System.IO.File.Exists(FilePathName))
            {
                System.IO.File.Delete(FilePathName);
            }

            // 刪除資料庫資料
            using var datas = new z_repoPhotos();
            var model = datas.GetData(id);
            datas.Delete(model.Id);
            dmJsonMessage result = new dmJsonMessage() { Mode = true, Message = "資料已刪除!!" };
            return Json(result);
        }
    }
}
