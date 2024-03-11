using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FreshBox.Controllers
{

    public class ProductController : Controller
    {
        /// <summary>
        /// 商品明細
        /// </summary>
        /// <param name="id">商品編號</param>
        /// <returns></returns>
        public IActionResult Detail(string id)
        {
            string trimmedId = id;
            char firstId = trimmedId[0];
            switch (id.Substring(0, 1))
            {
                case "M":
                    ViewData["idName"] = "男士時尚";
                    ViewData["idData"] = "MenWear";
                    break;
                case "W":
                    ViewData["idName"] = "女性優雅";
                    ViewData["idData"] = "WomenWear";
                    break;
                case "B":
                    ViewData["idName"] = "童裝天地";
                    ViewData["idData"] = "BabyWear";
                    break;
                case "A":
                    ViewData["idName"] = "精品配件";
                    ViewData["idData"] = "Accessories";
                    break;
                case "C":
                    ViewData["idName"] = "生鮮蔬果";
                    ViewData["idData"] = "ChilledFoods";
                    break;
                case "F":
                    ViewData["idName"] = "冷凍美食";
                    ViewData["idData"] = "FrozenFoods";
                    break;
                case "P":
                    ViewData["idName"] = "甜點饗宴";
                    ViewData["idData"] = "Pastries";
                    break;
                case "D":
                    ViewData["idName"] = "美味熟食";
                    ViewData["idData"] = "DeliItems";
                    break;
            }
            using var prod = new z_repoProducts();
            var model = prod.GetData(id);
            return View(model);
        }
    }
}