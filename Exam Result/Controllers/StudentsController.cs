using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Exam_Result.DatabaseContext;
using Exam_Result.Models;

namespace Exam_Result.Controllers
{
    public class StudentsController : Controller
    {
        private ResultDbContext db = new ResultDbContext();

        // GET: Students
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }


        public JsonResult GetStudentIdNumber()
        {

            //var totalStudent = db.Students.Count() + 1;
            //int length = 4;
            // var result = totalStudent.ToString().PadLeft(length, '0');


            var lastStudentId = (from n in db.Students
                                 orderby n.Id descending
                                 select n.Student_Id).FirstOrDefault();

            string input = "0";
            if (lastStudentId != null)
            {
                input = Regex.Replace(lastStudentId, "[^0-9]+", string.Empty);
            }

            string startKeyWord = "STU-";
            string studentId = startKeyWord + (Convert.ToInt32(input)+1);
            return Json(studentId, JsonRequestBehavior.AllowGet);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            //ViewBag.StudentId = GetStudentIdNumber();
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Student_Id,Name,Roll,Address")] Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.InnerException.InnerException.Message);
                    return View();

                    //ModelState.AddModelError("", ex.InnerException.InnerException.Message);
                    //return View("Error",  ex.InnerException.InnerException.Message);
                }

                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Student_Id,Name,Roll,Address")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);

            if (student != null)
            {
                StudentSubject subject = db.StudentSubjects.FirstOrDefault(c => c.StudentId == student.Id);
                //student.isDeleted = true;
                if (subject == null)
                {
                    db.Students.Remove(student);
                    //db.Entry(student).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    
                    ModelState.AddModelError("","Can not delete student while subject is already assigned!!");
                    return View(student);
                }
               
            }

            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
