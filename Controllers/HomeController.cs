using HrsVerification.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HrsVerification.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        TimeVerificationDb _timeVerificationDB = new TimeVerificationDb();

        public ActionResult Index()
        {
            ViewBag.Message = "";

            EmpStatusViewModel empStatus = new EmpStatusViewModel();
            empStatus.EmpStatus = EnumEmpStatus.Permanent;
            ViewBag.EmploymentStatus = empStatus.EmpTypes;   

            return View(empStatus); //.EmpTypes);
        }

        public ActionResult Confirm()
        {
            return View();
        }
 
        //[HttpPost]
        //public ActionResult Index(EmpStatusViewModel empStatus)
        //{
            //if (ModelState.IsValid)
            //{
            //    return RedirectToAction("Index");
            //}
        //    return View(empStatus);
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }


    public enum EmpType
    {
        English,
        French,
        Spanish
    }
    public static class Enum
    {
        public static IEnumerable<T> GetItems<T>()
        {
            return System.Enum.GetValues(typeof(T)).Cast<T>();
        }
    }

}
