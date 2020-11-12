using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SupervisorAllotment.Models;

namespace SupervisorAllotment.Controllers
{
    public class HomeController : Controller
    {
        private SupervisorAllotmentDBEntities db = new SupervisorAllotmentDBEntities();
        public ActionResult StudentHome()
        {
            return View();
        }

        public ActionResult AdminHome()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Proceed(string CollegeId)
        {
            var st = db.Students.Find(CollegeId);
            if (st == null)
            {
                return RedirectToAction("Create", "Student");
            }
            else
            {
                return RedirectToAction("Details", "Student", new { id = CollegeId });
            }
        }
    }
}