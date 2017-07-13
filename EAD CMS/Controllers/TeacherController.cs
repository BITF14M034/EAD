using EAD_CMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EAD_CMS.Controllers
{
    public class TeacherController : Controller
    {
        //
        // GET: /Teacher/
        public ActionResult Teacher()
        {
            Teacher teacher = new Teacher();

            string username = (string)Session["username"];

            using (CMSEntities db = new CMSEntities())
            {
                var userDetails = db.Teachers.Where(x => x.username == username).FirstOrDefault();

                teacher.name = userDetails.name.ToUpper();
                teacher.username = userDetails.username;

            }

            return View(teacher);
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
        public ActionResult AddAttendence(string course, string degree, string batch)
        {
            using (CMSEntities db = new CMSEntities())
            {
                assigned_course assCourse = db.assigned_course.Where(x => x.course == course && x.degree1.title == degree && x.batch1.name == batch).FirstOrDefault();

                if (assCourse != null)
                {
                    List<student> studentList = db.students.Where(x => x.degree1.title == degree && x.batch1.name == batch).ToList();

                    List<attendence> model = new List<attendence>();
                    for (int i = 0; i < studentList.Count; i++)
                    {
                        attendence atten = new attendence();
                        atten.ass_course_id = assCourse.Id;
                        atten.rollno = studentList[i].rollno;
                        atten.status = false;
                        atten.assigned_course = assCourse;
                        atten.student = studentList[i];
                        atten.date = DateTime.Now;

                        model.Add(atten);
                    }
                    return View(model);
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddAttendence(List<attendence> model)
        {
            CMSEntities db = new CMSEntities();
            int id = model[0].ass_course_id;
            System.DateTime date=model[0].date;
            assigned_course assCourse = db.assigned_course.Where(x => x.Id == id).FirstOrDefault();

            List<attendence> atten=db.attendences.Where(x=>x.ass_course_id==id && x.date==date).ToList();

            if (atten.Count == 0)
            {
                foreach (attendence item in model)
                {
                    student s = db.students.Where(x => x.rollno == item.rollno).FirstOrDefault();
                    item.assigned_course = assCourse;
                    item.student = s;
                    db.attendences.Add(item);
                }
                db.SaveChanges();

                return JavaScript("Attendence Submitted!");
            }
            return JavaScript("Error: Attendence for this date already submitted!");
            
        }
        public ActionResult About()
        {
            return View();
        }
    }
}