using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exam_Result.Models.ViewModels
{
    public class SubjectAssignVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Student Id")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

        [Required]
        [Display(Name = "Subject")]
        //[Remote("IsAlreadyAssigned", "SubjectAssign", AdditionalFields = "StudentId", HttpMethod = "POST", ErrorMessage = "Subject Already Exist, Try Another")]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public List<Subject> Subjects { get; set; }
    }
}