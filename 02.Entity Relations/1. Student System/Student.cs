using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace _1._Student_System
{
    public class Student
    {
        public Student()
        {
            Courses = new HashSet<Courses>();
            Homeworks = new HashSet<Homeworks>();
        }
        [Key]
        public int StudentId { get; set; }
        [MaxLength(100)]
        [Unicode]
        public string? Name { get; set; }
        [MaxLength(10)] 
        public string? PhoneNumber { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime? Birthday { get; set; }
        public ICollection<Courses> Courses { get; set; }
        public ICollection<Homeworks> Homeworks { get; set; }
    }
}
