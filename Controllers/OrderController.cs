using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FreshBox.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 查看訂單
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Login(RoleList = "Member")]
        public IActionResult Index(int id = 1)
        {
            using var order = new z_repoOrders();
            var model = order.GetOrderList()
                .ToPagedList(id, 10);
            SessionService.SetPageInfo(id, model.PageCount, true);
            return View(model);
        }

        /// <summary>
        /// 查看訂單明細
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Login(RoleList = "Member")]
        public IActionResult Detail(string id = "")
        {
            using var detail = new z_repoOrderDetails();
            var model = detail.GeOrderDetailList(id);
            return View(model);
        }

        /// <summary>
        /// 取消訂單
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Login(RoleList = "Member")]
        public IActionResult Cancel(string id = "")
        {
            using var order = new z_repoOrders();
            order.CancelOrder(id);
            return RedirectToAction("Index", "Order", new { area = "" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}