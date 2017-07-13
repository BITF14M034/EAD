using EAD_CMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EAD_CMS.Controllers
{
    public class StudentController : Controller
    {
        //
        // GET: /Student/
        public ActionResult Student()
        {
            student stu = new student();

            string username = (string)Session["username"];

            using (CMSEntities db=new CMSEntities())
            {
                var userDetails = db.students.Where(x => x.rollno == username).FirstOrDefault();

                if (userDetails != null)
                {
                    stu.name = userDetails.name.ToUpper();
                    stu.rollno = userDetails.rollno;
                    stu.degree = userDetails.degree;
                    stu.batch = userDetails.batch;
                }
                
            }

            return View(stu);
        }

        public ActionResult AllAttendence(int ass_id, string rollno)
        {
            using (CMSEntities db = new CMSEntities())
            {
                List<attendence> model = db.attendences.Where(x => x.ass_course_id == ass_id && x.rollno == rollno).ToList();

                return View(model);   
            }
        }

        public ActionResult ChangePassword(CMSEntities db)
        {
            if (Session["username"] != null)
            {
                string uname = Session["username"].ToString();
                login model = new login();
                model = db.logins.Where(x => x.username == uname).FirstOrDefault();


                return View(model);
            }
            else
                return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(login lg)
        {
            CMSEntities db = new CMSEntities();
            if (ModelState.IsValid)
            {
                db.Entry(lg).State = EntityState.Modified;
                db.SaveChanges();
                return JavaScript("Password Changed successfully!");
            }
            return View(lg);
        }
        public ActionResult About()
        {
            return View();
        }
	}
}