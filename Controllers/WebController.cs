using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreshBox.Controllers
{
    public class WebController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        /// <summary>
        /// 登出系統
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Login()]
        public IActionResult Logout()
        {
            SessionService.Logout();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        /// <summary>
        /// 登入系統
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous()]
        public IActionResult Login()
        {
            SessionService.IsLogin = false;
            ActionService.SetActionData("登入", "", "使用者");
            vmLogin model = new vmLogin();
            return View(model);
        }

        /// <summary>
        /// 登入系統
        /// </summary>
        /// <param name="model">使用者輸入的資料模型</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous()]
        public IActionResult Login(vmLogin model)
        {
            if (!ModelState.IsValid) return View(model);
            using var user = new z_repoUsers();
            if (!user.CheckLogin(model))
            {
                ModelState.AddModelError("UserNo", "登入帳號或密碼輸入不正確!!");
                model.UserNo = "";
                model.Password = "";
                return View(model);
            }

            //判斷使用者角色，進入不同的首頁
            var data = user.GetData(model.UserNo);
            if (data.RoleNo == "Admin") return RedirectToAction("Index", "Home", new { area = "Admin" });
            if (data.RoleNo == "Mis") return RedirectToAction("Index", "Home", new { area = "Mis" });
            if (data.RoleNo == "User") return RedirectToAction("Index", "Home", new { area = "User" });
            if (data.RoleNo == "Member")
            {
                //會員登入，合併遊客的購物車資訊
                using var cart = new z_repoCarts();
                cart.MergeCart();
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            //角色不正確,引發自定義錯誤,並重新輸入
            ModelState.AddModelError("UserNo", "登入帳號角色設定不正確!!");
            model.UserNo = "";
            model.Password = "";
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous()]
        public IActionResult Register()
        {
            ActionService.SetActionData("註冊", "", "使用者");
            vmRegister model = new vmRegister();
            model.GenderCode = "M";
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous()]
        public IActionResult Register(vmRegister model)
        {
            if (!ModelState.IsValid) return View(model);

            //檢查登入帳號及電子信箱是否有重覆
            using var user = new z_repoUsers();
            if (!user.CheckRegisterUserNo(model.UserNo))
            {
                ModelState.AddModelError("UserNo", "登入帳號重覆註冊!!");
                return View(model);
            }
            if (!user.CheckRegisterEmail(model.Email))
            {
                ModelState.AddModelError("Email", "電子信箱重覆註冊!!");
                return View(model);
            }
            //新增未審核的使用者記錄
            string str_code = user.RegisterNewUser(model);

            //寄出驗證信
            using var sendEmail = new SendMailService();
            string str_message = user.CheckMailValidateCode(str_code);
            if (string.IsNullOrEmpty(str_message))
            {
                var userData = user.GetValidateUser(str_code);
                var mailObject = new MailObject();
                mailObject.MailTime = DateTime.Now;
                mailObject.ValidateCode = str_code;
                mailObject.UserNo = userData.UserNo;
                mailObject.UserName = userData.UserName;
                mailObject.ToName = userData.UserName;
                mailObject.ToEmail = userData.ContactEmail;
                mailObject.ValidateCode = str_code;
                mailObject.ReturnUrl = $"{ActionService.HttpHost}/Web/RegisterValidate/{str_code}";

                str_message = sendEmail.UserRegister(mailObject);
                if (string.IsNullOrEmpty(str_message))
                {
                    str_message = "您的註冊資訊已建立，請記得收信完成驗證流程!!";
                }
            }
            //顯示註冊訊息
            SessionService.ReturnPage = "WebLogin";
            SessionService.MessageText = str_message;
            return RedirectToAction("Index", "Message", new { area = "" });
        }

        [HttpGet]
        [AllowAnonymous()]
        public IActionResult RegisterValidate(string id)
        {
            using var user = new z_repoUsers();
            SessionService.ReturnPage = "WebLogin";
            SessionService.MessageText = user.RegisterConfirm(id); ;
            return RedirectToAction("Index", "Message", new { area = "" });
        }

        [HttpGet]
        [AllowAnonymous()]
        public IActionResult Forget()
        {
            ActionService.SetActionData("忘記密碼", "", "使用者");
            vmForget model = new vmForget();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous()]
        public IActionResult Forget(vmForget model)
        {
            //1.檢查輸入資料是否合格
            if (!ModelState.IsValid) return View(model);
            using var user = new z_repoUsers();

            //2.檢查帳號是否存在,存在則設定新的密碼也設定狀態為未審核
            string str_code = user.Forget(model.UserNo);
            if (string.IsNullOrEmpty(str_code))
            {
                ModelState.AddModelError("UserNo", "查無帳號或電子信箱資訊!!");
                return View(model);
            }

            //3.寄出忘記密碼驗證信
            using var sendEmail = new SendMailService();
            string str_message = user.CheckMailValidateCode(str_code);
            if (string.IsNullOrEmpty(str_message))
            {
                var userData = user.GetValidateUser(str_code);
                var mailObject = new MailObject();
                mailObject.MailTime = DateTime.Now;
                mailObject.ValidateCode = str_code;
                mailObject.UserNo = userData.UserNo;
                mailObject.UserName = userData.UserName;
                mailObject.ToName = userData.UserName;
                mailObject.ToEmail = userData.ContactEmail;
                mailObject.ValidateCode = str_code;
                mailObject.Password = userData.Password;
                mailObject.ReturnUrl = $"{ActionService.HttpHost}/Web/ForgetValidate/{str_code}";

                str_message = sendEmail.UserForget(mailObject);
                if (string.IsNullOrEmpty(str_message))
                {
                    str_message = "您重設密碼的要求已受理，請記得收信完成重設密碼的流程!!!";
                }
            }

            //顯示註冊訊息
            SessionService.ReturnPage = "WebLogin";
            SessionService.MessageText = str_message;
            return RedirectToAction("Index", "Message", new { area = "" });
        }

        [HttpGet]
        [AllowAnonymous()]
        public IActionResult ForgetValidate(string id)
        {
            using var user = new z_repoUsers();
            //更新使用者狀態為已審核
            string str_message = user.ForgetConfirm(id);
            //顯示重設密碼訊息
            SessionService.ReturnPage = "WebLogin";
            SessionService.MessageText = str_message;
            return RedirectToAction("Index", "Message", new { area = "" });
        }

        [HttpGet]
        [Login()]
        public IActionResult ResetPassword()
        {
            ActionService.SetActionData("重設密碼", "", "使用者");
            vmResetPassword model = new vmResetPassword();
            return View(model);
        }

        [HttpPost]
        [Login()]
        public IActionResult ResetPassword(vmResetPassword model)
        {
            //1.檢查輸入資料是否合格
            if (!ModelState.IsValid) return View(model);
            using var user = new z_repoUsers();

            //2.檢查帳號是否存在,存在則設定新的密碼也設定狀態為未審核
            string str_code = user.ResetPassword(model);
            if (string.IsNullOrEmpty(str_code))
            {
                ModelState.AddModelError("UserNo", "目前密碼不正確!!");
                return View(model);
            }

            //3.寄出忘記密碼驗證信
            using var sendEmail = new SendMailService();

            string str_message = user.CheckMailValidateCode(str_code);
            if (string.IsNullOrEmpty(str_message))
            {
                var userData = user.GetValidateUser(str_code);
                var mailObject = new MailObject();
                mailObject.MailTime = DateTime.Now;
                mailObject.ValidateCode = str_code;
                mailObject.UserNo = userData.UserNo;
                mailObject.UserName = userData.UserName;
                mailObject.ToName = userData.UserName;
                mailObject.ToEmail = userData.ContactEmail;
                mailObject.ValidateCode = str_code;
                mailObject.Password = userData.Password;
                mailObject.ReturnUrl = $"{ActionService.HttpHost}/Web/ResetPasswordValidate/{str_code}";

                str_message = sendEmail.UserResetPassword(mailObject);
                if (string.IsNullOrEmpty(str_message))
                {
                    str_message = "您重設密碼的要求已受理，請記得收信完成重設密碼的流程!!!";
                }
            }

            //3.登出使用者
            SessionService.IsLogin = false;
            SessionService.UserNo = "";
            SessionService.UserName = "";

            //顯示註冊訊息
            SessionService.ReturnPage = "WebLogin";
            SessionService.MessageText = str_message;
            return RedirectToAction("Index", "Message", new { area = "" });
        }

        [HttpGet]
        [AllowAnonymous()]
        public IActionResult ResetPasswordValidate(string id)
        {
            using var user = new z_repoUsers();
            //更新使用者狀態為已審核
            string str_message = user.ResetPasswordConfirm(id);
            //顯示重設密碼訊息
            SessionService.ReturnPage = "WebLogin";
            SessionService.MessageText = str_message;
            return RedirectToAction("Index", "Message", new { area = "" });
        }

        [HttpGet]
        [Login()]
        public IActionResult Profile()
        {
            ActionService.SetActionData("我的帳號", "", "使用者");
            using var user = new z_repoUsers();
            var model = user
                .GetDataList()
                .Where(m => m.UserNo == SessionService.UserNo)
                .FirstOrDefault();
            return View(model);
        }

        [HttpGet]
        [Login()]
        public IActionResult PhotoUpload()
        {
            ActionService.SetActionData("上傳照片", "", "我的帳號");
            return View();
        }

        [HttpPost]
        [Login()]
        public IActionResult PhotoUpload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // 取得目前專案資料夾目錄路徑
                string FileNameOnServer = Directory.GetCurrentDirectory();
                // 專案路徑加入要存入的資料夾路徑
                FileNameOnServer += "\\wwwroot\\images\\users\\";
                // 以使用者名稱.jpg 存入
                FileNameOnServer += $"{SessionService.UserNo}.jpg";
                // 刪除已存在檔案
                if (System.IO.File.Exists(FileNameOnServer))
                    System.IO.File.Delete(FileNameOnServer);
                // 建立一個串流物件
                using var stream = System.IO.File.Create(FileNameOnServer);
                // 將檔案寫入到此串流物件中
                file.CopyTo(stream);
            }
            return RedirectToAction("Profile", "Web", new { area = "" });
        }

        [HttpGet]
        [Login()]
        public IActionResult EditProfile()
        {
            ActionService.SetActionData("修改個人資料", "", "我的帳號");
            using var user = new z_repoUsers();
            var model = user
                .GetDataList()
                .Where(m => m.UserNo == SessionService.UserNo)
                .FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        [Login()]
        public IActionResult EditProfile(Users model)
        {
            if (!ModelState.IsValid) return View(model);
            using var user = new z_repoUsers();
            user.UpdateUserProfile(model);
            return RedirectToAction("Profile", "Web", new { area = "" });
        }

        [HttpGet]
        [AllowAnonymous()]
        public IActionResult MessageResult()
        {
            ViewBag.MessageText = (TempData["MessageText"] == null) ? "" : TempData["MessageText"].ToString();
            return View();
        }

        [HttpGet]
        [Login()]
        public IActionResult Open(string id)
        {
            using var prg = new z_repoPrograms();
            var data = prg.GetData(id);
            if (data == null)
            {
                return RedirectToAction("Index", "Home", new { area = SessionService.RoleNo });
            }
            SessionService.SetProgramInfo(data);
            return RedirectToAction(data.ActionName, data.ControllerName, new { area = data.AreaName });
        }
    }
}