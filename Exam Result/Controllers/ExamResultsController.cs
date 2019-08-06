using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
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
            var examResults = db.ExamResults.OrderBy(c=>c.Student.Student_Id).Include(e => e.Student).Include(e => e.Subject);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Student_Id");
            ViewBag.SubjectId = db.Subjects.ToList();
            var vm=new ExamResultVM();
            vm.ExamResults = examResults.ToList();
            return View(vm);
            //ViewBag.Result = result;
            //return View();
        }

        public ActionResult GetResults()
        {
            //List<ExamResult> examResults = db.ExamResults.ToList();
            //var studentSubjects = db.StudentSubjects.ToList();
            //var students = db.Students.ToList();

            //var notAppeared = studentSubjects.Select(c => c.Id).Except(examResults.Select(c => c.StudentSubjectId)).ToList();

            ////var failedStudents = students.Where(c => notAppeared.Any(c2 => c2==c.Id)).Select(c => new { c.Student_Id, Status = "Fail" }).ToList();
            //var failedStudents = studentSubjects.Where(c => notAppeared.Any(c2 => c2 == c.Id)).Select(c => c.Student).GroupBy(c=>c.Student_Id).Select(c => new { Student_Id= c.Key, Status = "Fail" }).ToList();


            //var passedStudents = studentSubjects.Where(c => failedStudents.All(c2 => c2.Student_Id != c.Student.Student_Id)).GroupBy(c => c.Student.Student_Id).Select(c => new { Student_Id= c.Key, Status = "Pass" }).ToList();

            //var joinPassFail = passedStudents.Concat(failedStudents).ToList();
            //var finalResult = (from st in students
            //                   join f in joinPassFail on st.Student_Id equals f.Student_Id into se
            //                   from t in se.DefaultIfEmpty()
            //                   select new
            //                   {
            //                       st.Student_Id,
            //                       Status = t == null ? "Absent" : t.Status
            //                   }).ToList();

            //return Json(finalResult, JsonRequestBehavior.AllowGet);

            //return null;

            // To remove duplicate entry
            List<ExamResult> examResults = db.ExamResults.ToList();
            var studentSubjects = db.StudentSubjects.ToList();
            var students = db.Students.ToList();

            var notAppeared = studentSubjects.Where(c => !examResults.Any(c2 => c2.StudentId == c.StudentId&& c2.SubjectId==c.SubjectId)).ToList();

            //var failedStudents = students.Where(c => notAppeared.Any(c2 => c2==c.Id)).Select(c => new { c.Student_Id, Status = "Fail" }).ToList();
            var failedStudents = studentSubjects.Where(c => notAppeared.Any(c2 => c2 == c)).Select(c => c.Student).GroupBy(c => c.Student_Id).Select(c => new { Student_Id = c.Key, Status = "Fail" }).ToList();


            var passedStudents = studentSubjects.Where(c => failedStudents.All(c2 => c2.Student_Id != c.Student.Student_Id)).GroupBy(c => c.Student.Student_Id).Select(c => new { Student_Id = c.Key, Status = "Pass" }).ToList();

            var joinPassFail = passedStudents.Concat(failedStudents).ToList();
            var finalResult = (from st in students
                join f in joinPassFail on st.Student_Id equals f.Student_Id into se
                from t in se.DefaultIfEmpty()
                select new
                {
                    st.Student_Id,
                    Status = t == null ? "Absent" : t.Status
                }).ToList();

            return Json(finalResult, JsonRequestBehavior.AllowGet);
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
                        //Status = "Pass"
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
