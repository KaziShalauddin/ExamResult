using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exam_Result.Models
{
    public class ExamResult
    {
        public int Id { get; set; }

        public int StudentSubjectId { get; set; }
        public virtual StudentSubject StudentSubject { get; set; }
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
        public string Status { get; set; }
    }
}