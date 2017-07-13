using EAD_CMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EAD_CMS.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult validate(login users)
        {
            using (CMSEntities db = new CMSEntities())
            {
                var userDetails = db.logins.Where(x => x.username == users.username && x.password == users.password).FirstOrDefault();
                
                if (userDetails == null)
                {
                    users.LoginErrorMessage = "Wrong username or password.";
                    return View("Login", users);
                }
                else
                {
                    Session["username"] = userDetails.username;
                    if (userDetails.type == "t")
                        return RedirectToAction("Teacher", "Teacher");
                    else if (userDetails.type == "a")
                        return RedirectToAction("Admin", "Admin");
                    else if (userDetails.type=="s")
                        return RedirectToAction("Student", "Student");
                    else
                        return View("Login", users);
                             
                }
            }

            return View();
        }
	}
}