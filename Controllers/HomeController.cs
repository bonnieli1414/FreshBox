using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FreshBox.Models;

namespace FreshBox.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        vmContact model = new vmContact();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ContactAsync(vmContact model)
    {
        if (!ModelState.IsValid) return View(model);
        DateTime currentDateTime = DateTime.Now;
        TimeSpan currentTime = DateTime.Now.TimeOfDay;
        using (var messagesRepo = new z_repoMessages())
        {
            var message = new Messages
            {
                // 將 vmContact 的屬性賦值給新的 Messages 物件
                ContactorName = model.ContactorName,
                ContactorEmail = model.ContactorEmail,
                HeaderText = model.HeaderText,
                MessageText = model.MessageText,
                SendDate = DateTime.Today,
                SendTime = currentTime
            };
            await messagesRepo.CreateAsync(message);
        }
        return RedirectToAction("Index", "Home", new { area = "" });
    }

    /// <summary>
    /// 更多常見問題
    /// </summary>
    /// <returns></returns>
    public IActionResult Question()
    {
        using var question = new z_repoQuestions();
        var model = question.GetDataList();
        return View(model);
    }

    /// <summary>
    /// 結帳付款
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Login(RoleList = "Member")]
    public IActionResult Payment()
    {
        var model = new vmOrders();
        return View(model);
    }

    /// <summary>
    /// 結帳付款
    /// </summary>
    /// <param name="model">付款資訊</param>
    /// <returns></returns>
    [HttpPost]
    [Login(RoleList = "Member")]
    public IActionResult Payment(vmOrders model)
    {
        if (string.IsNullOrEmpty(model.ReceiveName)) { ModelState.Remove("ReceiveName"); model.ReceiveName = model.MemberName; }
        if (string.IsNullOrEmpty(model.ReceiveTel)) { ModelState.Remove("ReceiveTel"); model.ReceiveTel = model.MemberTel; }
        if (string.IsNullOrEmpty(model.ReceiveEmail)) { ModelState.Remove("ReceiveEmail"); model.ReceiveEmail = model.MemberEmail; }
        if (string.IsNullOrEmpty(model.ReceiveAddress)) { ModelState.Remove("ReceiveAddress"); model.ReceiveAddress = model.MemberAddress; }
        if (!ModelState.IsValid) return View(model);
        string str_order_no = CartService.CartPayment(model);
        // SessionService.ReturnPage = "ShopIndex";
        SessionService.MessageText = $"訂單已建立，您的訂單編號為：{str_order_no}!!";
        return RedirectToAction("Index", "Message", new { area = "" });
    }

}
