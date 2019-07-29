using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Exam_Result.Models;

namespace Exam_Result.DatabaseContext
{
    public class ResultDbContext : DbContext
    {
        public ResultDbContext() : base("defaultContext")
        {
            //
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }

        //public System.Data.Entity.DbSet<Exam_Result.Models.ViewModels.SubjectAssignVM> SubjectAssignVMs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure domain classes using modelBuilder here..

        }

    }
}

    