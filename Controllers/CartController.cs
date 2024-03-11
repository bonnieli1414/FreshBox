using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FreshBox.Controllers
{
    public class CartController : Controller
    {
        /// <summary>
        /// 購物車列表
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            using var cart = new z_repoCarts();
            var model = cart.GetDataList();
            return View(model);
        }

        /// <summary>
        /// 加入購物車
        /// </summary>
        /// <param name="id">商品編號</param>
        /// <returns></returns>
        public IActionResult AddCart(string id)
        {
            using var cart = new z_repoCarts();
            cart.AddCart(id, "", 1);
            return RedirectToAction("Index", "Cart", new { area = "" });
        }

        public IActionResult AddCarts(IFormCollection collection)
        {
            string str_prod_no = collection["ProdNo"].ToString();
            string str_qty = collection["qtybutton"].ToString();
            int int_qty = int.Parse(str_qty);
            string input = collection["submit"].ToString();

            using var cart = new z_repoCarts();
            cart.AddCart(str_prod_no, "", int_qty);
            if (input == "立即結帳")
            {
                return RedirectToAction("Payment", "Home", new { area = "" });
            }
            return RedirectToAction("Index", "Cart", new { area = "" });
        }

        /// <summary>
        /// 修改購物車數量
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="qty">數量</param>
        /// <returns></returns>
        public IActionResult UpdateCart(int id, int qty)
        {
            using var cart = new z_repoCarts();
            cart.UpdateCart(id, qty);
            return RedirectToAction("Index", "Cart", new { area = "" });
        }

        /// <summary>
        /// 修改購物車數量
        /// </summary>
        /// <param name="ProdNo">Id</param>
        /// <param name="Qty">數量</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult UpdateQty(string ProdNo, int Qty)
        {
            using var repoCart = new z_repoCarts();
            string str_message = repoCart.UpdateQty(ProdNo, Qty);
            // 如果回傳訊息不是空值，就是失敗
            if (!string.IsNullOrEmpty(str_message))
            {
                // 這裡會回傳失敗的錯誤訊息
                return Json(new { success = false, responseText = str_message });
            }
            // 成功則回覆成功OK
            return Json(new { success = true, responseText = "OK" });
        }

        /// <summary>
        /// 刪除購物車
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        public IActionResult DeleteCart(int id)
        {
            using var cart = new z_repoCarts();
            cart.DeleteCart(id);
            return RedirectToAction("Index", "Cart", new { area = "" });
        }


    }
}