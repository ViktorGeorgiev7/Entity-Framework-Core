using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _1._Student_System
{
    public class Homeworks
    {
        [Key]
        public int HomeworkId { get; set; }
        public string Content { get; set; }
        public TypeContent ContentType { get; set; }
        public DateTime SubmissionTime { get; set; }
        public int StudentId { get; set; }
        public int CoursesId { get; set; }
        
        public Student Student { get; set; }
        public Courses Course { get; set; }
    }
    public enum TypeContent 
    {
        Application,
        Pdf,
        Zip
    }
}
