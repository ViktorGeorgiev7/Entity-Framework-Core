using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace _1._Student_System
{
    public class Courses
    {
        public Courses()
        {
            Student = new HashSet<Student>();
            Resource = new HashSet<Resource>();
            Homeworks = new HashSet<Homeworks>();
        }
        [Key]
        public int CourseId { get; set; }
        [MaxLength(80)]
        [Unicode]
        public string Name { get; set; }
        [Unicode]
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Precision(2)]
        public double Price { get; set; }
        public ICollection<Student> Student { get; set; }
        public ICollection<Resource> Resource { get; set; }
        public ICollection<Homeworks> Homeworks { get; set; }

    }
}