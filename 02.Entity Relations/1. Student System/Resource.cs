using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1._Student_System
{
    public class Resource
    {
        [Key]
        public int ResourceId { get; set; }
        [MaxLength(50)]
        [Unicode]
        public string Name { get; set; }
        public string Url { get; set; }
        public Type ResourceType { get; set; }
        public int CoursesId { get; set; }
    }
    public enum Type 
    {
        Video,
        Presentation,
        Document,
        Other
    }
}