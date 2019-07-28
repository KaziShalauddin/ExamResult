using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Exam_Result.DatabaseContext;
using Exam_Result.Models;
using Exam_Result.Models.ViewModels;

namespace Exam_Result.Controllers
{
    public class SubjectAssignController : Controller
    {
        private ResultDbContext db = new ResultDbContext();

        // GET: SubjectAssign
        public ActionResult Index()
        {
            var subjectAssignVMs = db.StudentSubjects.Include(s => s.Student).Include(s => s.Subject);
            return View(subjectAssignVMs.ToList());
        }

        // GET: SubjectAssign/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentSubject subjectAssignVM = db.StudentSubjects.Find(id);
            if (subjectAssignVM == null)
            {
                return HttpNotFound();
            }
            return View(subjectAssignVM);
        }

        public JsonResult GetSubjects(int StudentId)
        {
            var subjects = db.StudentSubjects.Where(s => s.StudentId == StudentId).Select(s=>s.Subject);
            //bool result = false;
            //if (subject == null)
            //{
            //    result = true;
            //    return Json(result, JsonRequestBehavior.AllowGet);
            //}
            var allSubjects = db.Subjects.Except(subjects).ToList();
            var list = (from subject in allSubjects
                select new
                {
                    subject.Id,
                    subject.SubjectName
                }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);

        }

        // GET: SubjectAssign/Create
        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.Students, "Id", "StudentId");
            //ViewBag.SubjectId = db.Subjects.ToList();
            return View();
        }

        // POST: SubjectAssign/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       // [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,StudentId,SubjectId")] SubjectAssignVM subjectAssignVM)
        public ActionResult Create(int StudentId,int SubjectId)
        {
            
                StudentSubject subject = new StudentSubject()
                {
                    Subject = db.Subjects.Where(s=>s.Id==SubjectId).FirstOrDefault(),
                    Student = db.Students.Where(s=>s.Id==StudentId).FirstOrDefault()
                };
                db.StudentSubjects.Add(subject);
                //db.StudentSubjects.Add(subjectAssignVM);
                db.SaveChanges();
                return RedirectToAction("Index");
            

            //ViewBag.StudentId = new SelectList(db.Students, "Id", "StudentId", StudentId);
            //ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "SubjectName",SubjectId);
            //return View();
        }

        // GET: SubjectAssign/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // db.StudentSubjects.Find(id);
            SubjectAssignVM subjectAssignVM = new SubjectAssignVM();

            if (subjectAssignVM == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.Students, "Id", "StudentId", subjectAssignVM.StudentId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "SubjectName", subjectAssignVM.SubjectId);
            return View(subjectAssignVM);
        }

        // POST: SubjectAssign/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StudentId,SubjectId")] SubjectAssignVM subjectAssignVM)
        {
            if (ModelState.IsValid)
            {
                StudentSubject studentSubject = db.StudentSubjects
                    .FirstOrDefault(s => s.Id== subjectAssignVM.Id);
                studentSubject.StudentId = subjectAssignVM.StudentId;
                studentSubject.SubjectId = subjectAssignVM.SubjectId;
                db.Entry(studentSubject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.Students, "Id", "StudentId", subjectAssignVM.StudentId);
            ViewBag.SubjectId = new SelectList(db.Subjects, "Id", "SubjectName", subjectAssignVM.SubjectId);
            return View(subjectAssignVM);
        }

        // GET: SubjectAssign/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
                StudentSubject subject=db.StudentSubjects.Include(s => s.Student).Include(s=>s.Subject).FirstOrDefault(c => c.Id==(int)id);
            if (subject == null)
            {
                return HttpNotFound();
            }
           
            return View(subject);
            //return null;
        }

        // POST: SubjectAssign/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           
            StudentSubject subject = db.StudentSubjects.Find(id);
            db.StudentSubjects.Remove(subject);
            db.SaveChanges();
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
