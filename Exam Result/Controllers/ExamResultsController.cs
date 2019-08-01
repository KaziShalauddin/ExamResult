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
    public class ExamResultsController : Controller
    {
        private ResultDbContext db = new ResultDbContext();

        // GET: ExamResults
        public ActionResult Index()
        {
            var examResults = db.ExamResults.Include(e => e.Student).Include(e => e.Subject);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Student_Id");
            ViewBag.SubjectId = db.Subjects.ToList();
            //return View(examResults.ToList());
            //ViewBag.Result = result;
            return View();
        }

        public JsonResult GetResults()
        {
            var examResults = db.ExamResults.ToList();
            var studentSubjects = db.StudentSubjects.ToList();
            var students = db.Students.ToList();

            //var list = (from st in students
            //    join s in studentSubjects on st.Id equals s.StudentId into assignedSubjects
            //    from a in assignedSubjects.DefaultIfEmpty()
            //    join e in examResults on new { a.Student, a.Subject } equals new { e.Student, e.Subject } into se
            //    from sk in se.DefaultIfEmpty()
            //    select new
            //    {
            //        st.Student_Id,
            //        Status = a == null ? "Absent" : a.Subject.SubjectName
            //    }).ToList();

            var list1 = (from st in students
                        join s in studentSubjects on st.Id equals s.StudentId into se
                        from t in se.DefaultIfEmpty()
                        select new
                        {
                           StudentId= st.Id,
                            st.Student_Id,
                            SubjectName = t == null ? "No subject" : t.Subject.SubjectName
                        }).ToList();

            var list2 = (from st in list1
                join e in examResults on new{st.StudentId, st.SubjectName } equals new {e.StudentId,e.Subject.SubjectName} into se
                from t in se.DefaultIfEmpty()
                select new
                {
                    st.Student_Id,
                    st.SubjectName,
                    Status = t == null ? "Absent" : t.Status
                }).ToList();
            //select new { StudentSubjects = s, Results = e == null ? "Absent" : e.Status };
            //var result = list.ToList();
            //return Json(result, JsonRequestBehavior.AllowGet);
            return null;

        }
        // GET: ExamResults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamResult examResult = db.ExamResults.Find(id);
            if (examResult == null)
            {
                return HttpNotFound();
            }
            return View(examResult);
        }
        public JsonResult GetSubjectsForExam(int StudentId)
        {
            //var alreadyAddedResults = db.ExamResults.Where(c => c.StudentSubject.StudentId == StudentId).ToList();
            var subjects = db.StudentSubjects.Where(s => s.StudentId == StudentId).Select(s => s.Subject);
            var allSubjects = db.Subjects.Except(subjects).ToList();
            var list = (from subject in allSubjects
                        select new
                        {
                            subject.Id,
                            subject.SubjectName
                        }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);

        }
        // GET: ExamResults/Create
        public ActionResult Create()
        {
            //ViewBag.StudentSubjectId = new SelectList(db.StudentSubjects, "Id", "Id");
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Student_Id");
            ViewBag.SubjectId = db.Subjects.ToList();
            var studentSubject = db.StudentSubjects.ToList();
            return View();
        }

        // POST: ExamResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExamResultVM examResult)
        {
            if (ModelState.IsValid)
            {
                StudentSubject studentSubject = db.StudentSubjects.FirstOrDefault(s =>
                    s.StudentId == examResult.StudentId && s.SubjectId == examResult.SubjectId);
                if (studentSubject != null)
                {
                    ExamResult result = new ExamResult()
                    {
                        StudentId = examResult.StudentId,
                        SubjectId = examResult.SubjectId,
                        Status = "Pass"
                    };
                    db.ExamResults.Add(result);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {

                    ModelState.AddModelError("", "You have to assign subject first!!");
                    ViewBag.StudentId = new SelectList(db.Students, "Id", "Student_Id");
                    ViewBag.SubjectId = db.Subjects.ToList();
                    return View(examResult);
                }

            }

            //ViewBag.StudentSubjectId = new SelectList(db.StudentSubjects, "Id", "Id", examResult.StudentSubjectId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Student_Id");
            ViewBag.SubjectId = db.Subjects.ToList();
            return View(examResult);
        }

        // GET: ExamResults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamResult examResult = db.ExamResults.Find(id);
            if (examResult == null)
            {
                return HttpNotFound();
            }
            //ViewBag.StudentSubjectId = new SelectList(db.StudentSubjects, "Id", "Id", examResult.StudentSubjectId);
            return View(examResult);
        }

        // POST: ExamResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StudentId,SubjectId,Status")] ExamResult examResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.StudentSubjectId = new SelectList(db.StudentSubjects, "Id", "Id", examResult.StudentSubjectId);
            return View(examResult);
        }

        // GET: ExamResults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamResult examResult = db.ExamResults.Find(id);
            if (examResult == null)
            {
                return HttpNotFound();
            }
            return View(examResult);
        }

        // POST: ExamResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExamResult examResult = db.ExamResults.Find(id);
            db.ExamResults.Remove(examResult);
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
