using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exam_Result.Models.ViewModels
{
    public class ExamResultVM
    {
        public int Id { get; set; }
        [Required]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [Required]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public string Status { get; set; }

        public List<ExamResult> ExamResults { get; set; }
    }
}