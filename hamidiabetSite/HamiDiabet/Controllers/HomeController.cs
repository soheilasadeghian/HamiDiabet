using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
//using System.Web.Http;
using System.Net;
using System.Net.Http;




namespace HamiDiabet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.IsHome = true;

            return View();
        }
        public ActionResult LoginPage()
        {
            return View();
        }
        
        public ActionResult ErrorPage(string message)
        {
            ViewBag.IsHome = false;
            var vm = new Models.ErrorPage();
            vm.message = message;
            return View(vm as object);
        }

        [HttpGet]
        public ActionResult SignIn()
        {
            ViewBag.IsHome = false;
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(Models.SignIn user)
        {
            ViewBag.IsHome = false;
            var result = ClassCollection.User.SignIn(user.mobile, user.password);

            if (result.code == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (result.code == 1000)
            {
                ModelState.AddModelError("err", "خطا در اتصال به سرور");
                return View(user);
            }
            else
            {
                ModelState.AddModelError("err", "شماره موبایل یا گذرواژه اشتباه است");
                return View(user);
            }
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            ViewBag.IsHome = false;
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(Models.SignUp user)
        {
            ViewBag.IsHome = false;
            user.cityId = 0;
            var result = HamiDiabet.ClassCollection.User.SignUp(user.name, user.family, user.cityId.ToString(), user.mobile, user.password, user.subscribeNewsletter);

            if (result.code == 0)
            {
                return RedirectToAction("SignIn", "Home");
            }
            else if (result.code == 1000)
            {
                ModelState.AddModelError("err", "خطا در اتصال به سرور");
                return View(user);
            }
            else
            {
                switch (result.message)
                {
                    case "INVALID_NAME": result.message = "نام نامعتبر است"; break;
                    case "INVALID_FAMILY": result.message = "نام خانوادگی نامعتبر است"; break;
                    case "INVALID_CITY": result.message = "شهر نامعتبر است"; break;
                    case "INVALID_MOBILE": result.message = "شماره موبایل نامعتبر است"; break;
                    case "INVALID_PASSWORD": result.message = "گذرواژه نامعتبر است"; break;
                    case "INVALID_SUBSCRIBNEWSLETTER": result.message = "خبرنامه نامعتبر است"; break;
                    case "MOBILE_ALREADY_EXIST": result.message = "شماره موبایل نامعتبر است"; break;
                    default: break;
                }
                ModelState.AddModelError("err", result.message);
                return View(user);
            }
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            ViewBag.IsHome = false;
            var result = ClassCollection.User.SignOut();
            if (result.code == 1000)
            {
                System.Web.HttpContext.Current.Session["access_token"] = null;
            }
            return RedirectToAction("Index", "Home");
        }
        
    }
}