using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_Result.Models
{
    public class ExamResult
    {
        public int Id { get; set; }

        public int StudentSubjectId { get; set; }
        public virtual StudentSubject StudentSubject { get; set; }
        public string Status { get; set; }
    }
}