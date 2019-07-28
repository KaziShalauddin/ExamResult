using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exam_Result.Models
{
    public class Subject
    {
        public int Id { get; set; }
        [Display(Name = "Subject")]
        public string SubjectName { get; set; }
    }
}