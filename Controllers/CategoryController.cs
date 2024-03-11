using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using X.PagedList;

namespace FreshBox.Controllers
{
    public class CategoryController : Controller
    {
        /// <summary>
        /// 商品頁數功能
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IActionResult Index(string id = "All", int page = 1, int pageSize = 10)
        {
            switch (id)
            {
                case "All":
                    ViewData["idName"] = "綜合精選";
                    ViewData["idData"] = "All";
                    break;
                case "Apparel":
                    ViewData["idName"] = "時尚穿搭";
                    ViewData["idData"] = "Apparel";
                    break;
                case "Food":
                    ViewData["idName"] = "美食佳餚";
                    ViewData["idData"] = "Food";
                    break;
                case "MenWear":
                    ViewData["idName"] = "男士時尚";
                    ViewData["idData"] = "MenWear";
                    break;
                case "WomenWear":
                    ViewData["idName"] = "女性優雅";
                    ViewData["idData"] = "WomenWear";
                    break;
                case "BabyWear":
                    ViewData["idName"] = "童裝天地";
                    ViewData["idData"] = "BabyWear";
                    break;
                case "Accessories":
                    ViewData["idName"] = "精品配件";
                    ViewData["idData"] = "Accessories";
                    break;
                case "ChilledFoods":
                    ViewData["idName"] = "生鮮蔬果";
                    ViewData["idData"] = "ChilledFoods";
                    break;
                case "FrozenFoods":
                    ViewData["idName"] = "冷凍美食";
                    ViewData["idData"] = "FrozenFoods";
                    break;
                case "Pastries":
                    ViewData["idName"] = "甜點饗宴";
                    ViewData["idData"] = "Pastries";
                    break;
                case "DeliItems":
                    ViewData["idName"] = "美味熟食";
                    ViewData["idData"] = "DeliItems";
                    break;
            }

            using var prod = new z_repoProducts();
            var model = prod.GetCategoryDataList(id).ToPagedList(page, pageSize);
            ViewData["PageInfo"] = $"第 {page} 頁，共 {model.PageCount} 頁";
            SessionService.CategoryNo = id;
            return View(model);
        }
        public IActionResult Sort(string id = "D")
        {
            SessionService.SortNo = id;
            return RedirectToAction("Index", "Category", new { area = "", id = SessionService.CategoryNo });
        }
        /// <summary>
        /// 搜尋商品 name="id">搜尋文字
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Search()
        {
            object obj_text = Request.Form["SearchText"];
            SessionService.SearchText = (obj_text == null) ? "" : obj_text.ToString();
            return RedirectToAction("Index", "Category", new { area = "" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}