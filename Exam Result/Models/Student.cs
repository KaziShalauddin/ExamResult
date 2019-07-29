using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exam_Result.Models
{
    public class Student
    {
        public int Id { get; set; }

        //[Index("StudentId_Index", IsUnique = true)]
        [Display(Name = "Student Id")]
        [StringLength(10)]
        [Index(IsUnique = true)]
        public string StudentId { get; set; }
        public string Name { get; set; }
        public int Roll { get; set; }
        public string Address { get; set; }
        //public bool isDeleted { get; set; }
    }
}