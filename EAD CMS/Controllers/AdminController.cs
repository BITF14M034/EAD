using EAD_CMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EAD_CMS.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        public ActionResult Admin()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        public ActionResult RegisterStudent()
        {
            return View();
        }
        public ActionResult RegisterTeacher()
        {
            return View();
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
        public ActionResult AllStudents(CMSEntities db)
        {
            List<student> allStudents = db.students.ToList();

            return View(allStudents);
        }
        [HttpPost]
        public ActionResult AllStudents(string key)
        {
            CMSEntities db = new CMSEntities();
            return View(db.students.Where(model => model.name.Contains(key) || key == null).ToList());
        }
        public ActionResult AllTeachers(CMSEntities db)
        {
            List<Teacher> allTeachers = db.Teachers.ToList();

            return View(allTeachers);
        }
        public ActionResult AllDegrees(CMSEntities db)
        {
            List<degree> model = db.degrees.ToList();
            return View(model);
        }
        public ActionResult AllBatches(CMSEntities db)
        {
            List<batch> model = db.batches.ToList();
            return View(model);
        }
        public ActionResult AllCourses(CMSEntities db)
        {
            List<course> model = db.courses.ToList();

            return View(model);
        }
        [HttpPost]
        public ActionResult AllTeachers(string key)
        {
            CMSEntities db = new CMSEntities();
            return View(db.Teachers.Where(model => model.name.Contains(key) || key == null).ToList());
        }
        public ActionResult AssignedCourses(CMSEntities db)
        {
            List<assigned_course> model = db.assigned_course.ToList();

            return View(model);
        }
        public ActionResult AddStudent(CMSEntities db)
        {

            student model = new student();
            IEnumerable<SelectListItem> degList = db.degrees.Select(c => new SelectListItem
            {
                Value = c.title,
                Text = c.title.ToUpper()

            });

            model.degreeList = degList;

            IEnumerable<SelectListItem> batList = db.batches.Select(c => new SelectListItem
            {
                Value = c.name,
                Text = c.name.ToUpper()

            });
            model.batchList = batList;


            return View(model);
        }


        public ActionResult AssignCourse(CMSEntities db)
        {
            assigned_course model = new assigned_course();
            IEnumerable<SelectListItem> degList = db.degrees.Select(c => new SelectListItem
            {
                Value = c.title,
                Text = c.title.ToUpper()

            });

            model.degreeList = degList;

            IEnumerable<SelectListItem> batList = db.batches.Select(c => new SelectListItem
            {
                Value = c.name,
                Text = c.name.ToUpper()

            });
            model.batchList = batList;

            IEnumerable<SelectListItem> courseList = db.courses.Select(c => new SelectListItem
            {
                Value = c.name,
                Text = c.name.ToUpper()
            });
            model.courseList = courseList;

            IEnumerable<SelectListItem> tUnameList = db.Teachers.Select(c => new SelectListItem
            {
                Value = c.username,
                Text = c.username
            });
            model.tUnameList = tUnameList;

            return View(model);
        }
        [HttpPost]
        public ActionResult AddStudent(student s)
        {
            if (ModelState.IsValid)
            {
                CMSEntities db = new CMSEntities();

                login l = new login();
                l.username = s.rollno;
                l.password = "123";
                l.type = "s";

                db.students.Add(s);
                db.logins.Add(l);
                db.SaveChanges();


                return JavaScript("Student Added Successfully!");
            }
            else
                return View();
        }
        [HttpPost]
        public ActionResult AssignCourse(assigned_course model)
        {
            if (ModelState.IsValid)
            {
                CMSEntities db = new CMSEntities();

                var assCourse = db.assigned_course.Where(x => x.course == model.course && x.batch == model.batch && x.degree==model.degree).FirstOrDefault();

                if (assCourse != null)
                {
                    return JavaScript("Error: Such course is already assigned to '" + assCourse.t_username + "'");
                }
                else
                {
                    db.assigned_course.Add(model);
                    db.SaveChanges();
                }
                

                return JavaScript("Course Assigned successfully!");
            }
            return View(model);
        }
        public ActionResult AddTeacher(CMSEntities db)
        {
            Teacher model = new Teacher();

            return View(model);
        }
        [HttpPost]
        public ActionResult AddTeacher(Teacher model)
        {
            if (ModelState.IsValid)
            {
                CMSEntities db = new CMSEntities();

                login lg = new login();
                lg.username = model.username;
                lg.password = "pucit";
                lg.type = "t";

                db.Teachers.Add(model);
                db.logins.Add(lg);
                db.SaveChanges();

                return JavaScript("Teacher Added Successfully!");
            }
            else
                return View(model);
        }
        public ActionResult AddDegree()
        {
            degree deg = new degree();

            return View(deg);
        }
        [HttpPost]
        public ActionResult AddDegree(degree model)
        {
            if (ModelState.IsValid)
            {
                CMSEntities db = new CMSEntities();

                db.degrees.Add(model);
                db.SaveChanges();

                return JavaScript("New Degree Added Successfully!");
            }
            else
                return View(model);
        }
        public ActionResult AddBatch()
        {
            batch model = new batch();

            return View(model);
        }
        [HttpPost]
        public ActionResult AddBatch(batch model)
        {
            if (ModelState.IsValid)
            {
                CMSEntities db = new CMSEntities();

                db.batches.Add(model);
                db.SaveChanges();

                return JavaScript("New Batch Added Successfully!");
            }
            else
                return View(model);
        }
        public ActionResult AddCourse()
        {
            course model = new course();

            return View(model);
        }
        [HttpPost]
        public ActionResult AddCourse(course model)
        {
            if (ModelState.IsValid)
            {
                CMSEntities db = new CMSEntities();

                db.courses.Add(model);
                db.SaveChanges();

                return JavaScript("New Course Added Successfully!");
            }
            else
                return View(model);
        }
        public ActionResult DeleteStudent(string id)
        {
            CMSEntities db = new CMSEntities();
            student s = db.students.Where(x => x.rollno == id).FirstOrDefault();

            s.name = s.name.ToUpper();
            s.rollno = s.rollno.ToUpper();
            s.batch1.name = s.batch1.name.ToUpper();
            s.degree1.title = s.degree1.title.ToUpper();

            return View(s);
        }
        [HttpPost]
        public ActionResult DeleteStudent(string id, student stu)
        {
            CMSEntities db = new CMSEntities();
            student s = new student();
            login lg = new login();
            {
                s = db.students.Find(id);
                lg = db.logins.Find(id);
                if (s != null)
                    db.students.Remove(s);
                if (lg != null)
                    db.logins.Remove(lg);
                db.SaveChanges();

                return JavaScript("Student Deleted Successfully!");
            }

        }
        public ActionResult DeleteTeacher(string id)
        {
            CMSEntities db = new CMSEntities();
            Teacher model = db.Teachers.Where(x => x.username == id).FirstOrDefault();

            model.name = model.name.ToUpper();
            model.username = model.username.ToUpper();

            return View(model);
        }
        [HttpPost]
        public ActionResult DeleteTeacher(string id, Teacher model)
        {
            CMSEntities db = new CMSEntities();
            login lg = new login();
            model = db.Teachers.Find(id);
            lg = db.logins.Find(id);
            if (lg != null)
                db.logins.Remove(lg);
            if (model != null)
                db.Teachers.Remove(model);
            db.SaveChanges();

            return JavaScript("Teacher Deleted Successfully!");

        }
        public ActionResult EditStudent(string id)
        {
            CMSEntities db = new CMSEntities();
            student s = db.students.Find(id);

            IEnumerable<SelectListItem> degList = db.degrees.Select(c => new SelectListItem
            {
                Value = c.title,
                Text = c.title.ToUpper()

            });

            s.degreeList = degList;

            IEnumerable<SelectListItem> batList = db.batches.Select(c => new SelectListItem
            {
                Value = c.name,
                Text = c.name.ToUpper()

            });
            s.batchList = batList;

            return View(s);
        }
        [HttpPost]
        public ActionResult EditStudent(student s)
        {
            CMSEntities db = new CMSEntities();
            if (ModelState.IsValid)
            {
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
                return JavaScript("Student Modified successfully!");
            }
            return View(s);
        }
        public ActionResult EditTeacher(string id)
        {
            CMSEntities db = new CMSEntities();
            Teacher model = db.Teachers.Find(id);

            return View(model);
        }
        [HttpPost]
        public ActionResult EditTeacher(Teacher model)
        {
            CMSEntities db = new CMSEntities();
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return JavaScript("Teacher Modified Successfully!");
            }
            return View(model);
        }
        [HttpGet]
        public JsonResult isExist(string username)
        {
            CMSEntities db = new CMSEntities();
            JavaScript("gotoParent()");
            return Json(!db.logins.Any(x => x.username == username), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult isDegExist(string title)
        {
            CMSEntities db = new CMSEntities();
            return Json(!db.degrees.Any(x => x.title == title), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult isBatchExist(string name)
        {
            CMSEntities db = new CMSEntities();
            return Json(!db.batches.Any(x => x.name == name), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult isCourseExist(string name)
        {
            CMSEntities db = new CMSEntities();
            return Json(!db.courses.Any(x => x.name == name), JsonRequestBehavior.AllowGet);
        }
    }
}