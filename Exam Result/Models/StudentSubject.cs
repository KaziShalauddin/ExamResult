using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exam_Result.Models
{
    public class StudentSubject
    {
        public int Id { get; set; }
        [Display(Name = "Student Id")]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        [Display(Name = "Subject")]
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
    }
}